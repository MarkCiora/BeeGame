using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public enum MouseButton
{
    Left,
    Right,
    Middle
}

public class InputEvent
{
    public Keys? key { get; }
    public MouseButton? mouse_button { get; }
    public Point mouse_pos { get; }
    public bool consumed { get; private set; }

    public InputEvent(Keys key)
    {
        this.key = key;
    }

    public InputEvent(MouseButton button, Point pos)
    {
        mouse_button = button;
        mouse_pos = pos;
    }

    public void Consume()
    {
        consumed = true;
    }
}

public static class Input
{
    private static KeyboardState last_kstate;
    private static KeyboardState current_kstate;
    private static MouseState last_mstate;
    private static MouseState current_mstate;

    public static void Init()
    {
        last_kstate = Keyboard.GetState();
        current_kstate = Keyboard.GetState();
        last_mstate = Mouse.GetState();
        current_mstate = Mouse.GetState();
    }

    public static void Update()
    {
        last_kstate = current_kstate;
        current_kstate = Keyboard.GetState();
        last_mstate = current_mstate;
        current_mstate = Mouse.GetState();
        InputRouter.ProcessInput();
    }

    private static ButtonState GetButtonState(MouseState state, MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => state.LeftButton,
            MouseButton.Right => state.RightButton,
            MouseButton.Middle => state.MiddleButton,
            _ => ButtonState.Released
        };
    }

    public static bool IsPressed(Keys key)
    {
        return current_kstate.IsKeyDown(key) && last_kstate.IsKeyUp(key);
    }

    public static bool IsReleased(Keys key)
    {
        return current_kstate.IsKeyUp(key) && last_kstate.IsKeyDown(key);
    }

    public static bool IsHeld(Keys key)
    {
        return current_kstate.IsKeyDown(key);
    }

    public static bool IsPressed(MouseButton button)
    {
        return GetButtonState(current_mstate, button) == ButtonState.Pressed &&
            GetButtonState(last_mstate, button) == ButtonState.Released;
    }

    public static bool IsReleased(MouseButton button)
    {
        return GetButtonState(current_mstate, button) == ButtonState.Released &&
            GetButtonState(last_mstate, button) == ButtonState.Pressed;
    }

    public static bool IsHeld(MouseButton button)
    {
        return GetButtonState(current_mstate, button) == ButtonState.Pressed;
    }

    public static Point MousePosP()
    {
        return new Point(current_mstate.X, current_mstate.Y);
    }
    public static Vector2 MousePosV()
    {
        return new Vector2((float)current_mstate.X, (float)current_mstate.Y);
    }

    public static bool IsScrollUp()
    {
        int scroll_delta = current_mstate.ScrollWheelValue - last_mstate.ScrollWheelValue;
        return scroll_delta > 0;
    }

    public static bool IsScrollDown()
    {
        int scroll_delta = current_mstate.ScrollWheelValue - last_mstate.ScrollWheelValue;
        return scroll_delta < 0;
    }

    public static IEnumerable<InputEvent> GetFrameEvents()
    {
        // Keys
        foreach (Keys key in Enum.GetValues(typeof(Keys)))
        {
            if (IsPressed(key))
                yield return new InputEvent(key);
        }

        // Mouse buttons
        foreach (MouseButton button in Enum.GetValues(typeof(MouseButton)))
        {
            if (IsPressed(button))
                yield return new InputEvent(button, MousePosP());
        }
    }
}

public static class InputRouter
{
    private static readonly Dictionary<Keys, List<(int Priority, Func<InputEvent, bool> Handler)>> key_handlers = new();
    private static readonly Dictionary<MouseButton, List<(int Priority, Func<InputEvent, bool> Handler)>> mouse_handlers = new();

    // Register a key handler
    public static void RegisterKeyHandler(Keys key, Func<InputEvent, bool> handler, int priority = 0)
    {
        if (!key_handlers.TryGetValue(key, out var list))
        {
            list = new List<(int, Func<InputEvent, bool>)>();
            key_handlers[key] = list;
        }
        list.Add((priority, handler));
        list.Sort((a, b) => b.Priority.CompareTo(a.Priority));
    }

    // Register a mouse button handler
    public static void RegisterMouseHandler(MouseButton button, Func<InputEvent, bool> handler, int priority = 0)
    {
        if (!mouse_handlers.TryGetValue(button, out var list))
        {
            list = new List<(int, Func<InputEvent, bool>)>();
            mouse_handlers[button] = list;
        }
        list.Add((priority, handler));
        list.Sort((a, b) => b.Priority.CompareTo(a.Priority));
    }

    public static void ProcessInput()
    {
        foreach (var e in Input.GetFrameEvents())
        {
            if (e.key.HasValue && key_handlers.TryGetValue(e.key.Value, out var keyList))
            {
                foreach (var (_, handler) in keyList)
                {
                    if (handler(e))
                    {
                        e.Consume();
                        break;
                    }
                }
            }
            else if (e.mouse_button.HasValue && mouse_handlers.TryGetValue(e.mouse_button.Value, out var mouseList))
            {
                foreach (var (_, handler) in mouseList)
                {
                    if (handler(e))
                    {
                        e.Consume();
                        break;
                    }
                }
            }
        }
    }
}
