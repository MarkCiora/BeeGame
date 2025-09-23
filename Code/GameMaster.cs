using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;

namespace BeeGame;

public class GameMaster : Game
{
    private GraphicsDeviceManager gdm;
    private SpriteBatch sb;
    private Desktop _desktop;

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

        GlobalLogic.Init();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        sb = new SpriteBatch(GraphicsDevice);

        MyraEnvironment.Game = this;

        var grid = new Grid
        {
        RowSpacing = 8,
        ColumnSpacing = 8
        };

        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        var helloWorld = new Label
        {
        Id = "label",
        Text = "Hello, World!"
        };
        grid.Widgets.Add(helloWorld);

        // ComboBox
        var combo = new ComboView();
        Grid.SetColumn(combo, 1);
        Grid.SetRow(combo, 0);

        combo.Widgets.Add(new Label{Text = "Red", TextColor = Color.Red});
        combo.Widgets.Add(new Label{Text = "Green", TextColor = Color.Green});
        combo.Widgets.Add(new Label{Text = "Blue", TextColor = Color.Blue});

        grid.Widgets.Add(combo);

        // Button
        var button = new Button
        {
        Content = new Label
        {
            Text = "Show"
        }
        };
        Grid.SetColumn(button, 0);
        Grid.SetRow(button, 1);

        button.Click += (s, a) =>
        {
        var messageBox = Dialog.CreateMessageBox("Message", "Some message!");
        messageBox.ShowModal(_desktop);
        };

        grid.Widgets.Add(button);

        // Spin button
        var spinButton = new SpinButton
        {
        Width = 100,
        Nullable = true
        };
        Grid.SetColumn(spinButton, 1);
        Grid.SetRow(spinButton, 1);

        grid.Widgets.Add(spinButton);

        // Add it to the desktop
        _desktop = new Desktop();
        _desktop.Root = grid;

        Visuals.Init(sb, Content);
    }

    protected override void Update(GameTime gameTime)
    {
        Time.Update(gameTime);

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Input.Update();

        // _desktop.Update(gameTime);

        GlobalLogic.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        Visuals.Update();
        
        _desktop.Render();

        base.Draw(gameTime);
    }
}
