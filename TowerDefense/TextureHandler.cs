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

    public static void LoadTextures(ContentManager content, GraphicsDevice gd)
    {
        texture_car = content.Load<Texture2D>("woody");
        texture_road = content.Load<Texture2D>("path");
        fireDragonTex = content.Load<Texture2D>("charlieTheCapybaraAnimationSheet");
        background = content.Load<Texture2D>("background");

        texture_red = new Texture2D(gd, 1, 1, false, SurfaceFormat.Color);
        texture_red.SetData<Microsoft.Xna.Framework.Color>(new Color[] { Color.Red });

        texture_yellow = content.Load<Texture2D>("fire");
    }
}

