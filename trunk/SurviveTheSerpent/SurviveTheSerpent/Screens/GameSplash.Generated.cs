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
using FlatRedBall.Math;

namespace SurviveTheSerpent.Screens
{
	public partial class GameSplash : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		private Scene SceneFile;

		private SurviveTheSerpent.Entities.Player mPlayer;
		public SurviveTheSerpent.Entities.Player Player
		{
			get{ return mPlayer;}
		}
		private SurviveTheSerpent.Entities.SnakeHead SnakeHead;
		private SurviveTheSerpent.Entities.SnakeTail SnakeTail;
		private PositionedObjectList<SnakeBody> SnakeBodyList;
		private SurviveTheSerpent.Entities.DirectionButton upButton;
		private SurviveTheSerpent.Entities.DirectionButton downButton;
		private SurviveTheSerpent.Entities.DirectionButton leftButton;
		private SurviveTheSerpent.Entities.DirectionButton rightButton;
		private PositionedObjectList<Food> mFoodList;
		public PositionedObjectList<Food> FoodList
		{
			get{ return mFoodList;}
		}
		private PositionedObjectList<Obstacle> ObstacleList;

		public GameSplash()
			: base("GameSplash")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			SceneFile = FlatRedBallServices.Load<Scene>("content/screens/gamesplash/scenefile.scnx", ContentManagerName);
			mPlayer = new SurviveTheSerpent.Entities.Player(ContentManagerName, false);
			mPlayer.Name = "mPlayer";
			SnakeHead = new SurviveTheSerpent.Entities.SnakeHead(ContentManagerName, false);
			SnakeHead.Name = "SnakeHead";
			SnakeTail = new SurviveTheSerpent.Entities.SnakeTail(ContentManagerName, false);
			SnakeTail.Name = "SnakeTail";
			SnakeBodyList = new PositionedObjectList<SnakeBody>();
			upButton = new SurviveTheSerpent.Entities.DirectionButton(ContentManagerName, false);
			upButton.Name = "upButton";
			downButton = new SurviveTheSerpent.Entities.DirectionButton(ContentManagerName, false);
			downButton.Name = "downButton";
			leftButton = new SurviveTheSerpent.Entities.DirectionButton(ContentManagerName, false);
			leftButton.Name = "leftButton";
			rightButton = new SurviveTheSerpent.Entities.DirectionButton(ContentManagerName, false);
			rightButton.Name = "rightButton";
			mFoodList = new PositionedObjectList<Food>();
			ObstacleList = new PositionedObjectList<Obstacle>();



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

				Player.Activity();
				SnakeHead.Activity();
				SnakeTail.Activity();
				for(int i = SnakeBodyList.Count - 1; i > -1; i--)
				{
					SnakeBodyList[i].Activity();
				}
				upButton.Activity();
				downButton.Activity();
				leftButton.Activity();
				rightButton.Activity();
				for(int i = FoodList.Count - 1; i > -1; i--)
				{
					FoodList[i].Activity();
				}
				for(int i = ObstacleList.Count - 1; i > -1; i--)
				{
					ObstacleList[i].Activity();
				}
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
			if(Player != null)
			{
				Player.Destroy();
			}
			if(SnakeHead != null)
			{
				SnakeHead.Destroy();
			}
			if(SnakeTail != null)
			{
				SnakeTail.Destroy();
			}
			for(int i = SnakeBodyList.Count - 1; i > -1; i--)
			{
				SnakeBodyList[i].Destroy();
			}
			if(upButton != null)
			{
				upButton.Destroy();
			}
			if(downButton != null)
			{
				downButton.Destroy();
			}
			if(leftButton != null)
			{
				leftButton.Destroy();
			}
			if(rightButton != null)
			{
				rightButton.Destroy();
			}
			for(int i = FoodList.Count - 1; i > -1; i--)
			{
				FoodList[i].Destroy();
			}
			for(int i = ObstacleList.Count - 1; i > -1; i--)
			{
				ObstacleList[i].Destroy();
			}
			SceneFile.RemoveFromManagers(ContentManagerName != "Global");



			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize()
		{
			upButton.X = 8f;
			upButton.Y = 0f;
			downButton.X = -8f;
			downButton.Y = 0f;
			leftButton.X = 0f;
			leftButton.Y = 14f;
			rightButton.X = 0f;
			rightButton.Y = -14f;
		}
		public virtual void AddToManagersBottomUp()
		{
			SceneFile.AddToManagers(mLayer);

			mPlayer.AddToManagers(mLayer);
			SnakeHead.AddToManagers(mLayer);
			SnakeTail.AddToManagers(mLayer);
			upButton.AddToManagers(mLayer);
			upButton.X = 8f;
			upButton.Y = 0f;
			downButton.AddToManagers(mLayer);
			downButton.X = -8f;
			downButton.Y = 0f;
			leftButton.AddToManagers(mLayer);
			leftButton.X = 0f;
			leftButton.Y = 14f;
			rightButton.AddToManagers(mLayer);
			rightButton.X = 0f;
			rightButton.Y = -14f;
		}
		public virtual void ConvertToManuallyUpdated()
		{
			SceneFile.ConvertToManuallyUpdated();
			Player.ConvertToManuallyUpdated();
			SnakeHead.ConvertToManuallyUpdated();
			SnakeTail.ConvertToManuallyUpdated();
			for(int i = 0; i < SnakeBodyList.Count; i++)
			{
				SnakeBodyList[i].ConvertToManuallyUpdated();
			}
			upButton.ConvertToManuallyUpdated();
			downButton.ConvertToManuallyUpdated();
			leftButton.ConvertToManuallyUpdated();
			rightButton.ConvertToManuallyUpdated();
			for(int i = 0; i < FoodList.Count; i++)
			{
				FoodList[i].ConvertToManuallyUpdated();
			}
			for(int i = 0; i < ObstacleList.Count; i++)
			{
				ObstacleList[i].ConvertToManuallyUpdated();
			}
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
			SurviveTheSerpent.Entities.Player.LoadStaticContent(contentManagerName);
			SurviveTheSerpent.Entities.SnakeHead.LoadStaticContent(contentManagerName);
			SurviveTheSerpent.Entities.SnakeTail.LoadStaticContent(contentManagerName);
			SurviveTheSerpent.Entities.DirectionButton.LoadStaticContent(contentManagerName);
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