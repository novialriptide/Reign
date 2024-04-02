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

        private float sidePanelWidth;
        private float sidePanelHeight;
        private float menuBarHeight;

        public Editor()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 4 / 5,
                PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 4 / 5
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            imGuiRenderer = new ImGuiRenderer(this);
            imGuiRenderer.RebuildFontAtlas();

            gameScreen = new RenderTarget2D(
                GraphicsDevice,
                1280,
                720);

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

        protected virtual void ImGuiLayout()
        {
            menuBarHeight = ImGui.GetFrameHeight();
            sidePanelWidth = GraphicsDevice.Viewport.Width / 4;
            sidePanelHeight = GraphicsDevice.Viewport.Height * 5 / 7 - menuBarHeight;

            // Create main menu bar
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    ImGui.MenuItem("New");
                    ImGui.MenuItem("Open");
                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("Edit"))
                {
                    ImGui.MenuItem("Undo");
                    ImGui.MenuItem("Redo");
                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("Run"))
                {
                    ImGui.MenuItem("Start Debugging");
                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("Build"))
                {
                    ImGui.MenuItem("Package a Debug Build");
                    ImGui.MenuItem("Package a Release Build");
                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }

            ImGui.Begin("Side Panel", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar);

            ImGui.SetWindowPos(new Vector2(0, menuBarHeight));
            ImGui.SetWindowSize(new Vector2(sidePanelWidth, sidePanelHeight));

            if (ImGui.BeginTabBar("Tabs"))
            {
                if (ImGui.BeginTabItem("Hierarchy"))
                {
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Scenes"))
                {
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Inspector"))
                {
                    ImGui.EndTabItem();
                }
                ImGui.EndTabBar();
            }

            ImGui.End();

            ImGui.Begin("Bottom Panel", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar);

            ImGui.SetWindowPos(new Vector2(0, sidePanelHeight + menuBarHeight));
            ImGui.SetWindowSize(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height - sidePanelHeight));

            if (ImGui.BeginTabBar("Tabs"))
            {
                if (ImGui.BeginTabItem("Assets"))
                {
                    ImGui.EndTabItem();
                }
                ImGui.EndTabBar();
            }

            ImGui.End();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(gameScreen);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw your game graphics here using spriteBatch

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin();
            spriteBatch.Draw(gameScreen, new Rectangle((int)sidePanelWidth, (int)menuBarHeight, gameScreen.Width, gameScreen.Height), Color.White);
            spriteBatch.End();

            imGuiRenderer.BeforeLayout(gameTime);
            ImGuiLayout();
            imGuiRenderer.AfterLayout();

            base.Draw(gameTime);
        }
    }
}
