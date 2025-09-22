using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class GameMaster : Game
{
    private GraphicsDeviceManager gdm;
    private SpriteBatch sb;

    public GameMaster()
    {
        gdm = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Screen.Init(gdm);
        Input.Init();
        MathZ.Init();
        
        GameLogic.Init();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        sb = new SpriteBatch(GraphicsDevice);

        Visuals.Init(sb, Content);
    }

    protected override void Update(GameTime gameTime)
    {
        Time.Update(gameTime);

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Input.Update();
        GameLogic.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        Visuals.Update();

        base.Draw(gameTime);
    }
}
