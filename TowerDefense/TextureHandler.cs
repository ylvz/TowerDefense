using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

internal static class TextureHandler
{
    public static Texture2D texture_red;
    public static Texture2D antlerTex;
    public static Texture2D weakEnemyTex, strongEnemyTex;
    public static Texture2D texture_road, mudTex;
    public static Texture2D mooseTex, priceTex1, priceTex2, priceTex3;
    public static Texture2D background, wolfTex, hogTex;
    public static Texture2D spikesTex, biteTex;
    public static Texture2D mainMenu, wolfButton, mooseButton, hogButton;
    public static Texture2D placingMooseTex, placingWolfTex, placingHogTex;
    public static Texture2D startButton, quitButton, storyButton, healthTex, forestTex;
    public static SpriteFont spriteFont;

    public static void LoadTextures(ContentManager content, GraphicsDevice gd)
    {
        weakEnemyTex = content.Load<Texture2D>("woo");
        strongEnemyTex = content.Load<Texture2D>("strongwoo");
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
        placingMooseTex = content.Load<Texture2D>("MoosePlacer");
        placingWolfTex = content.Load<Texture2D>("WolfPlacer");
        placingHogTex = content.Load<Texture2D>("HogPlacer");
        priceTex1 = content.Load<Texture2D>("1");
        priceTex2 = content.Load<Texture2D>("2");
        priceTex3 = content.Load<Texture2D>("3");
        spriteFont = content.Load<SpriteFont>("File");
        mudTex = content.Load<Texture2D>("mud1");
        texture_red = new Texture2D(gd, 1, 1, false, SurfaceFormat.Color);
        texture_red.SetData<Microsoft.Xna.Framework.Color>(new Color[] { Color.Red });

        antlerTex = content.Load<Texture2D>("antlers");
        spikesTex = content.Load<Texture2D>("spikes");
        biteTex = content.Load<Texture2D>("bitey");
    }
}