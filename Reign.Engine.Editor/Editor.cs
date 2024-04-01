using System;
using ImGuiNET;
using ImGuiNET.SampleProgram.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;
using Vector4 = System.Numerics.Vector4;

namespace Reign.Engine.Editor
{
    public class Editor : Game
    {
        private GraphicsDeviceManager graphics;
        private ImGuiRenderer imGuiRenderer;
        private Texture2D imGuiXnaTexture;
        private IntPtr imGuiTexture;

        private SpriteBatch spriteBatch;
        private RenderTarget2D gameScreen;

        public Editor()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            imGuiRenderer = new ImGuiRenderer(this);
            imGuiRenderer.RebuildFontAtlas();

            gameScreen = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // First, load the texture as a Texture2D (can also be done using the XNA/FNA content pipeline)
            imGuiXnaTexture = new Texture2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            // Then, bind it to an ImGui-friendly pointer, that we can use during regular ImGui.** calls (see below)
            imGuiTexture = imGuiRenderer.BindTexture(imGuiXnaTexture);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        // Direct port of the example at https://github.com/ocornut/imgui/blob/master/examples/sdl_opengl2_example/main.cpp
        private float f = 0.0f;
        private Vector3 clear_color = new Vector3(114f / 255f, 144f / 255f, 154f / 255f);
        private byte[] _textBuffer = new byte[100];
        protected virtual void ImGuiLayout()
        {
            // 1. Show a simple window
            // Tip: if we don't call ImGui.Begin()/ImGui.End() the widgets appears in a window automatically called "Debug"
            {
                ImGui.Text("Hello, world!");
                ImGui.SliderFloat("float", ref f, 0.0f, 1.0f, string.Empty);
                ImGui.ColorEdit3("clear color", ref clear_color);
                ImGui.Text(string.Format("Application average {0:F3} ms/frame ({1:F1} FPS)", 1000f / ImGui.GetIO().Framerate, ImGui.GetIO().Framerate));

                ImGui.InputText("Text input", _textBuffer, 100);

                ImGui.Text("Texture sample");
                ImGui.Image(imGuiTexture, new Vector2(300, 150), Vector2.Zero, Vector2.One, Vector4.One, Vector4.One); // Here, the previously loaded texture is used
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(gameScreen);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw your game graphics here using spriteBatch

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin();
            spriteBatch.Draw(gameScreen, new Rectangle(100, 100, gameScreen.Width, gameScreen.Height), Color.White);
            spriteBatch.End();

            imGuiRenderer.BeforeLayout(gameTime);
            ImGuiLayout();
            imGuiRenderer.AfterLayout();

            base.Draw(gameTime);
        }
    }
}
