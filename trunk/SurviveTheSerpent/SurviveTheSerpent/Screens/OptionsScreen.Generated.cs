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

		private SurviveTheSerpent.Entities.DifficultyButton Easy;
		private SurviveTheSerpent.Entities.DifficultyButton Medium;
		private SurviveTheSerpent.Entities.DifficultyButton Hard;

		public OptionsScreen()
			: base("OptionsScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			SceneFile = FlatRedBallServices.Load<Scene>("content/screens/optionsscreen/scenefile.scnx", ContentManagerName);
			Easy = new SurviveTheSerpent.Entities.DifficultyButton(ContentManagerName, false);
			Easy.Name = "Easy";
			Medium = new SurviveTheSerpent.Entities.DifficultyButton(ContentManagerName, false);
			Medium.Name = "Medium";
			Hard = new SurviveTheSerpent.Entities.DifficultyButton(ContentManagerName, false);
			Hard.Name = "Hard";



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

				Easy.Activity();
				Medium.Activity();
				Hard.Activity();
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
			if(Easy != null)
			{
				Easy.Destroy();
			}
			if(Medium != null)
			{
				Medium.Destroy();
			}
			if(Hard != null)
			{
				Hard.Destroy();
			}
			SceneFile.RemoveFromManagers(ContentManagerName != "Global");



			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize()
		{
		}
		public virtual void AddToManagersBottomUp()
		{
			SceneFile.AddToManagers(mLayer);

			Easy.AddToManagers(mLayer);
			Medium.AddToManagers(mLayer);
			Hard.AddToManagers(mLayer);
		}
		public virtual void ConvertToManuallyUpdated()
		{
			SceneFile.ConvertToManuallyUpdated();
			Easy.ConvertToManuallyUpdated();
			Medium.ConvertToManuallyUpdated();
			Hard.ConvertToManuallyUpdated();
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
