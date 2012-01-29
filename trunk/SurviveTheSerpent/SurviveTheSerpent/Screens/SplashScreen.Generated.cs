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

		private SurviveTheSerpent.Entities.StartButton mstartButton;
		public SurviveTheSerpent.Entities.StartButton startButton
		{
			get{ return mstartButton;}
		}
		private SurviveTheSerpent.Entities.CreditButton mcreditButton;
		public SurviveTheSerpent.Entities.CreditButton creditButton
		{
			get{ return mcreditButton;}
		}
		private SurviveTheSerpent.Entities.InstructionButton minstructionButton;
		public SurviveTheSerpent.Entities.InstructionButton instructionButton
		{
			get{ return minstructionButton;}
		}

		public SplashScreen()
			: base("SplashScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			SceneFile = FlatRedBallServices.Load<Scene>("content/screens/splashscreen/scenefile.scnx", ContentManagerName);
			mstartButton = new SurviveTheSerpent.Entities.StartButton(ContentManagerName, false);
			mstartButton.Name = "mstartButton";
			mcreditButton = new SurviveTheSerpent.Entities.CreditButton(ContentManagerName, false);
			mcreditButton.Name = "mcreditButton";
			minstructionButton = new SurviveTheSerpent.Entities.InstructionButton(ContentManagerName, false);
			minstructionButton.Name = "minstructionButton";



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
			SceneFile.RemoveFromManagers(ContentManagerName != "Global");



			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize()
		{
			startButton.X = 0f;
			startButton.Y = 0f;
			creditButton.X = -6f;
			creditButton.Y = 0f;
			instructionButton.X = -2f;
			instructionButton.Y = 0f;
		}
		public virtual void AddToManagersBottomUp()
		{
			SceneFile.AddToManagers(mLayer);

			mstartButton.AddToManagers(mLayer);
			mstartButton.X = 0f;
			mstartButton.Y = 0f;
			mcreditButton.AddToManagers(mLayer);
			mcreditButton.X = -6f;
			mcreditButton.Y = 0f;
			minstructionButton.AddToManagers(mLayer);
			minstructionButton.X = -2f;
			minstructionButton.Y = 0f;
		}
		public virtual void ConvertToManuallyUpdated()
		{
			SceneFile.ConvertToManuallyUpdated();
			startButton.ConvertToManuallyUpdated();
			creditButton.ConvertToManuallyUpdated();
			instructionButton.ConvertToManuallyUpdated();
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
