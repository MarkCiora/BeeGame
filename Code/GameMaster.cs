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
    private Desktop desktop;

    public GameMaster()
    {
        gdm = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // initialize UI
        MyraEnvironment.Game = this;
        desktop = new Desktop();
        GS.desktop = desktop;
        
        // initialize helpers
        Screen.Init(gdm);
        Input.Init();
        MathZ.Init();

        // initialize game state
        GlobalLogic.Init();

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

        GlobalLogic.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        Visuals.Update();

        desktop.Render();

        base.Draw(gameTime);
    }
}
