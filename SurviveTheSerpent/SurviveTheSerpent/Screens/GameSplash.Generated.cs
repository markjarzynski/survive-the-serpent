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
using FlatRedBall.Math.Geometry;

namespace SurviveTheSerpent.Screens
{
	public partial class GameSplash : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		private Scene SceneFile;
		private ShapeCollection CollisionFile;

		private SurviveTheSerpent.Entities.Player Player;
		private SurviveTheSerpent.Entities.SnakeHead SnakeHead;
		private SurviveTheSerpent.Entities.SnakeTail SnakeTail;
		private PositionedObjectList<SnakeBody> SnakeBodyList;
		private SurviveTheSerpent.Entities.UpDownButton upButton;
		private SurviveTheSerpent.Entities.UpDownButton downButton;
		private SurviveTheSerpent.Entities.LeftRightButton leftButton;
		private SurviveTheSerpent.Entities.LeftRightButton rightButton;
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
			CollisionFile = FlatRedBallServices.Load<ShapeCollection>("content/screens/gamesplash/collisionfile.shcx", ContentManagerName);
			Player = new SurviveTheSerpent.Entities.Player(ContentManagerName, false);
			Player.Name = "Player";
			SnakeHead = new SurviveTheSerpent.Entities.SnakeHead(ContentManagerName, false);
			SnakeHead.Name = "SnakeHead";
			SnakeTail = new SurviveTheSerpent.Entities.SnakeTail(ContentManagerName, false);
			SnakeTail.Name = "SnakeTail";
			SnakeBodyList = new PositionedObjectList<SnakeBody>();
			upButton = new SurviveTheSerpent.Entities.UpDownButton(ContentManagerName, false);
			upButton.Name = "upButton";
			downButton = new SurviveTheSerpent.Entities.UpDownButton(ContentManagerName, false);
			downButton.Name = "downButton";
			leftButton = new SurviveTheSerpent.Entities.LeftRightButton(ContentManagerName, false);
			leftButton.Name = "leftButton";
			rightButton = new SurviveTheSerpent.Entities.LeftRightButton(ContentManagerName, false);
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

			CollisionFile.RemoveFromManagers(ContentManagerName != "Global");



			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize()
		{
			upButton.X = 9f;
			upButton.RotationZ = -1.57f;
			downButton.X = -9f;
			downButton.Y = 0f;
			downButton.RotationZ = 1.57f;
			leftButton.X = 0f;
			leftButton.Y = 15.6f;
			leftButton.RotationZ = 1.57f;
			rightButton.X = 0f;
			rightButton.Y = -15.6f;
			rightButton.RotationZ = -1.57f;
		}
		public virtual void AddToManagersBottomUp()
		{
			SceneFile.AddToManagers(mLayer);

			CollisionFile.AddToManagers(mLayer);

			Player.AddToManagers(mLayer);
			SnakeHead.AddToManagers(mLayer);
			SnakeTail.AddToManagers(mLayer);
			upButton.AddToManagers(mLayer);
			upButton.X = 9f;
			upButton.RotationZ = -1.57f;
			downButton.AddToManagers(mLayer);
			downButton.X = -9f;
			downButton.Y = 0f;
			downButton.RotationZ = 1.57f;
			leftButton.AddToManagers(mLayer);
			leftButton.X = 0f;
			leftButton.Y = 15.6f;
			leftButton.RotationZ = 1.57f;
			rightButton.AddToManagers(mLayer);
			rightButton.X = 0f;
			rightButton.Y = -15.6f;
			rightButton.RotationZ = -1.57f;
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
			SurviveTheSerpent.Entities.UpDownButton.LoadStaticContent(contentManagerName);
			SurviveTheSerpent.Entities.LeftRightButton.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		object GetMember(string memberName)
		{
			switch(memberName)
			{
				case "SceneFile":
					return SceneFile;
				case "CollisionFile":
					return CollisionFile;
			}
			return null;
		}


	}
}
