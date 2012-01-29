using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Utilities;

using SurviveTheSerpent.Screens;

namespace SurviveTheSerpent
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public static Song theme;
        public static SoundEffect hiss;
        public static SoundEffect eat;
        public static SoundEffect loss;
        public static SoundEffect win;

        public static enum Difficulty
        {
            easy,
            medium,
            hard
        }

        public static Difficulty diff = Difficulty.medium;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			BackStack<string> bs = new BackStack<string>();
			bs.Current = string.Empty;

			
            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;

        }

        protected override void Initialize()
        {
            FlatRedBall.Graphics.Renderer.UseRenderTargets = false;
            FlatRedBallServices.InitializeFlatRedBall(this, graphics);
			GlobalContent.Initialize();

			Screens.ScreenManager.Start(typeof(SurviveTheSerpent.Screens.SplashScreen).FullName);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            theme = Content.Load<Song>("Sound/theme_music");
            hiss = Content.Load<SoundEffect>("Sound/hiss_loud");
            eat = Content.Load<SoundEffect>("Sound/eat");
            loss = Content.Load<SoundEffect>("Sound/loss");
            win = Content.Load<SoundEffect>("Sound/win");
            MediaPlayer.Play(theme);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            FlatRedBallServices.Update(gameTime);
            
			Screens.ScreenManager.Activity();
            
			base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            FlatRedBallServices.Draw();

            base.Draw(gameTime);
        }
    }
}
