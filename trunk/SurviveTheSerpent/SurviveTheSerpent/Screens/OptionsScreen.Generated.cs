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
	public partial class OptionsScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		private Scene SceneFile;

		private SurviveTheSerpent.Entities.DifficultyButton easyButton;
		private SurviveTheSerpent.Entities.DifficultyButton mediumButton;
		private SurviveTheSerpent.Entities.DifficultyButton hardButton;

		public OptionsScreen()
			: base("OptionsScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			SceneFile = FlatRedBallServices.Load<Scene>("content/screens/optionsscreen/scenefile.scnx", ContentManagerName);
			easyButton = new SurviveTheSerpent.Entities.DifficultyButton(ContentManagerName, false);
			easyButton.Name = "easyButton";
			mediumButton = new SurviveTheSerpent.Entities.DifficultyButton(ContentManagerName, false);
			mediumButton.Name = "mediumButton";
			hardButton = new SurviveTheSerpent.Entities.DifficultyButton(ContentManagerName, false);
			hardButton.Name = "hardButton";

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

				easyButton.Activity();
				mediumButton.Activity();
				hardButton.Activity();
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
			if(easyButton != null)
			{
				easyButton.Destroy();
			}
			if(mediumButton != null)
			{
				mediumButton.Destroy();
			}
			if(hardButton != null)
			{
				hardButton.Destroy();
			}
			SceneFile.RemoveFromManagers(ContentManagerName != "Global");



			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize()
		{
			easyButton.X = 5.5f;
			easyButton.Y = 0f;
			mediumButton.X = 1f;
			mediumButton.Y = 0f;
			hardButton.X = -4f;
			hardButton.Y = 0f;
		}
		public virtual void AddToManagersBottomUp()
		{
			SceneFile.AddToManagers(mLayer);

			easyButton.AddToManagers(mLayer);
			easyButton.X = 5.5f;
			easyButton.Y = 0f;
			mediumButton.AddToManagers(mLayer);
			mediumButton.X = 1f;
			mediumButton.Y = 0f;
			hardButton.AddToManagers(mLayer);
			hardButton.X = -4f;
			hardButton.Y = 0f;
		}
		public virtual void ConvertToManuallyUpdated()
		{
			SceneFile.ConvertToManuallyUpdated();
			easyButton.ConvertToManuallyUpdated();
			mediumButton.ConvertToManuallyUpdated();
			hardButton.ConvertToManuallyUpdated();
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
			SurviveTheSerpent.Entities.DifficultyButton.LoadStaticContent(contentManagerName);
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
