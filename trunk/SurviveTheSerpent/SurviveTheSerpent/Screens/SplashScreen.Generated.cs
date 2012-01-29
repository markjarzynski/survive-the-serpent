using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Input;
using FlatRedBall.IO;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4
using Color = Microsoft.Xna.Framework.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using FlatRedBall.Broadcasting;
using SurviveTheSerpent.Entities;
using FlatRedBall;

namespace SurviveTheSerpent.Screens
{
	public partial class SplashScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		private Scene SceneFile;

		private SurviveTheSerpent.Entities.StartButton startButton;
		private SurviveTheSerpent.Entities.CreditButton creditButton;
		private SurviveTheSerpent.Entities.InstructionButton instructionButton;
		private SurviveTheSerpent.Entities.OptionsButton optionsButton;

		public SplashScreen()
			: base("SplashScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			SceneFile = FlatRedBallServices.Load<Scene>("content/screens/splashscreen/scenefile.scnx", ContentManagerName);
			startButton = new SurviveTheSerpent.Entities.StartButton(ContentManagerName, false);
			startButton.Name = "startButton";
			creditButton = new SurviveTheSerpent.Entities.CreditButton(ContentManagerName, false);
			creditButton.Name = "creditButton";
			instructionButton = new SurviveTheSerpent.Entities.InstructionButton(ContentManagerName, false);
			instructionButton.Name = "instructionButton";
			optionsButton = new SurviveTheSerpent.Entities.OptionsButton(ContentManagerName, false);
			optionsButton.Name = "optionsButton";



			PostInitialize();
			if(addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers

        public override void AddToManagers()
        {
			AddToManagersBottomUp();
			CustomInitialize();

        }


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if(!IsPaused)
			{

				startButton.Activity();
				creditButton.Activity();
				instructionButton.Activity();
				optionsButton.Activity();
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity
			SceneFile.ManageAll();
		
		
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			if(startButton != null)
			{
				startButton.Destroy();
			}
			if(creditButton != null)
			{
				creditButton.Destroy();
			}
			if(instructionButton != null)
			{
				instructionButton.Destroy();
			}
			if(optionsButton != null)
			{
				optionsButton.Destroy();
			}
			SceneFile.RemoveFromManagers(ContentManagerName != "Global");



			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize()
		{
			startButton.X = 2f;
			startButton.Y = -1.5f;
			creditButton.X = -7f;
			creditButton.Y = -0.5f;
			instructionButton.X = -2.5f;
			instructionButton.Y = -0.3f;
			optionsButton.X = 6.5f;
			optionsButton.Y = 0f;
			optionsButton.EntireSceneCurrentChainName = "default";
		}
		public virtual void AddToManagersBottomUp()
		{


            // We move the main Camera back to the origin and unrotate it so that anything attached to it can just use its absolute position
            float oldCameraRotationX = SpriteManager.Camera.RotationX;
            float oldCameraRotationY = SpriteManager.Camera.RotationY;
            float oldCameraRotationZ = SpriteManager.Camera.RotationZ;

            float oldCameraX = SpriteManager.Camera.X;
            float oldCameraY = SpriteManager.Camera.Y;
            float oldCameraZ = SpriteManager.Camera.Z;

            SpriteManager.Camera.X = 0;
            SpriteManager.Camera.Y = 0;
            SpriteManager.Camera.Z = 40; // Move it to 40 so that things attach in front of the camera.
            SpriteManager.Camera.RotationX = 0;
            SpriteManager.Camera.RotationY = 0;
            SpriteManager.Camera.RotationZ = 0;
			SceneFile.AddToManagers(mLayer);

			startButton.AddToManagers(mLayer);
			startButton.X = 2f;
			startButton.Y = -1.5f;
			if(startButton.Parent == null)
			{
				startButton.AttachTo(SpriteManager.Camera, true);
			}
			creditButton.AddToManagers(mLayer);
			creditButton.X = -7f;
			creditButton.Y = -0.5f;
			if(creditButton.Parent == null)
			{
				creditButton.AttachTo(SpriteManager.Camera, true);
			}
			instructionButton.AddToManagers(mLayer);
			instructionButton.X = -2.5f;
			instructionButton.Y = -0.3f;
			if(instructionButton.Parent == null)
			{
				instructionButton.AttachTo(SpriteManager.Camera, true);
			}
			optionsButton.AddToManagers(mLayer);
			optionsButton.X = 6.5f;
			optionsButton.Y = 0f;
			optionsButton.EntireSceneCurrentChainName = "default";

            SpriteManager.Camera.X = oldCameraX;
            SpriteManager.Camera.Y = oldCameraY;
            SpriteManager.Camera.Z = oldCameraZ;
            SpriteManager.Camera.RotationX = oldCameraRotationX;
            SpriteManager.Camera.RotationY = oldCameraRotationY;
            SpriteManager.Camera.RotationZ = oldCameraRotationZ;
                		}
		public virtual void ConvertToManuallyUpdated()
		{
			SceneFile.ConvertToManuallyUpdated();
			startButton.ConvertToManuallyUpdated();
			creditButton.ConvertToManuallyUpdated();
			instructionButton.ConvertToManuallyUpdated();
			optionsButton.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent(string contentManagerName)
		{
			#if DEBUG
			if(contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if(HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			SurviveTheSerpent.Entities.StartButton.LoadStaticContent(contentManagerName);
			SurviveTheSerpent.Entities.CreditButton.LoadStaticContent(contentManagerName);
			SurviveTheSerpent.Entities.InstructionButton.LoadStaticContent(contentManagerName);
			SurviveTheSerpent.Entities.OptionsButton.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		object GetMember(string memberName)
		{
			switch(memberName)
			{
				case "SceneFile":
					return SceneFile;
			}
			return null;
		}


	}
}
