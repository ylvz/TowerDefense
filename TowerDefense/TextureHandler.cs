using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

internal static class TextureHandler
{
    public static Texture2D texture_red;
    public static Texture2D antlerTex;
    public static Texture2D texture_car;
    public static Texture2D texture_road;
    public static Texture2D mooseTex;
    public static Texture2D background, wolfTex, hogTex;
    public static Texture2D spikesTex, biteTex;
    public static Texture2D mainMenu, wolfButton, mooseButton, hogButton;
    public static Texture2D startButton, quitButton, storyButton, healthTex, forestTex;

    public static void LoadTextures(ContentManager content, GraphicsDevice gd)
    {
        texture_car = content.Load<Texture2D>("woo");
        texture_road = content.Load<Texture2D>("path2");
        mooseTex = content.Load<Texture2D>("moose");
        background = content.Load<Texture2D>("bg");
        mainMenu = content.Load<Texture2D>("Mainmenu");
        startButton = content.Load<Texture2D>("button1");
        quitButton = content.Load<Texture2D>("button2");
        storyButton = content.Load<Texture2D>("button3");
        healthTex = content.Load<Texture2D>("health");
        wolfButton = content.Load<Texture2D>("wolfbutton");
        mooseButton = content.Load<Texture2D>("moosebutton");
        hogButton = content.Load<Texture2D>("hogbutton");
        forestTex = content.Load<Texture2D>("forest");
        hogTex = content.Load<Texture2D>("hog");
        wolfTex =content.Load<Texture2D>("wolfye");
        texture_red = new Texture2D(gd, 1, 1, false, SurfaceFormat.Color);
        texture_red.SetData<Microsoft.Xna.Framework.Color>(new Color[] { Color.Red });

        antlerTex = content.Load<Texture2D>("antlers");
        spikesTex = content.Load<Texture2D>("spikes");
        biteTex = content.Load<Texture2D>("bitey");
    }
}