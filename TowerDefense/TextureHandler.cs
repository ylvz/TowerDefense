using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

internal static class TextureHandler
{
    public static Texture2D texture_red;
    public static Texture2D texture_yellow;
    public static Texture2D texture_car;
    public static Texture2D texture_road;
    public static Texture2D fireDragonTex;
    public static Texture2D background, iceDragonTex, windDragonTex, elecDragonTex;
    public static Texture2D mainMenu;
    public static Texture2D startButton, quitButton, storyButton;

    public static void LoadTextures(ContentManager content, GraphicsDevice gd)
    {
        texture_car = content.Load<Texture2D>("woo");
        texture_road = content.Load<Texture2D>("path2");
        fireDragonTex = content.Load<Texture2D>("moose");
        background = content.Load<Texture2D>("bg");
        mainMenu = content.Load<Texture2D>("Mainmenu");
        startButton = content.Load<Texture2D>("button1");
        quitButton = content.Load<Texture2D>("button2");
        storyButton = content.Load<Texture2D>("button3");
        texture_red = new Texture2D(gd, 1, 1, false, SurfaceFormat.Color);
        texture_red.SetData<Microsoft.Xna.Framework.Color>(new Color[] { Color.Red });

        texture_yellow = content.Load<Texture2D>("antlers");
    }
}

