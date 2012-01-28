using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Model;

using FlatRedBall.Input;
using FlatRedBall.Utilities;

using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using SurviveTheSerpent.Screens;
using Matrix = Microsoft.Xna.Framework.Matrix;
using FlatRedBall.Broadcasting;
using SurviveTheSerpent.Entities;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Gui;

#if XNA4
using Color = Microsoft.Xna.Framework.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

#if FRB_XNA
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace SurviveTheSerpent.Entities
{
	public partial class LeftRightButton : PositionedObject, IDestroyable, IClickable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public enum VariableState
		{
			Uninitialized, // This exists so that the first set call actually does something
			Inactive,
			Normal,
			Pressed
		}
		static object mLockObject = new object();
		static bool mHasRegisteredUnload = false;
		static bool IsStaticContentLoaded = false;
		private static AnimationChainList AnimationChainListFile;
		private static Scene SceneFile;

		private Sprite EntireScene;
		public string EntireSceneCurrentChainName
		{
			get
			{
				return EntireScene.CurrentChainName;
			}
			set
			{
				EntireScene.CurrentChainName = value;
			}
		}
		protected bool mIsPaused;
		public override void Pause(InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual bool HasCursorOver(Cursor cursor)
		{
			if(mIsPaused)
			{
				return false;
			}
			if(LayerProvidedByContainer != null && LayerProvidedByContainer.Visible == false)
			{
				return false;
			}
			if(cursor.IsOn3D(EntireScene, LayerProvidedByContainer))
			{
				return true;
			}
			return false;
		}
		public virtual bool WasClickedThisFrame(Cursor cursor)
		{
			return cursor.PrimaryClick && HasCursorOver(cursor);
		}
		protected Layer LayerProvidedByContainer = null;

        public LeftRightButton(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public LeftRightButton(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);

		}

		protected virtual void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			EntireScene = SceneFile.Sprites.FindByName("directionalarrowright1").Clone();


			PostInitialize();
			if(addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers

        public virtual void AddToManagers(Layer layerToAddTo)
        {
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();

        }

		public virtual void Activity()
		{
			// Generated Activity
			mIsPaused = false;

			CustomActivity();
			
			// After Custom Activity


		
}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			if(EntireScene != null)
			{
				SpriteManager.RemoveSprite(EntireScene);
			}





			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize()
		{
			X = 0f;
			Y = 0f;
			RotationZ = 0f;
			EntireSceneCurrentChainName = "NoGlow";
		}
		public virtual void AddToManagersBottomUp(Layer layerToAddTo)
		{


            // We move this back to the origin and unrotate it so that anything attached to it can just use its absolute position
            float oldRotationX = RotationX;
            float oldRotationY = RotationY;
            float oldRotationZ = RotationZ;

            float oldX = X;
            float oldY = Y;
            float oldZ = Z;

            X = 0;
            Y = 0;
            Z = 0;
            RotationX = 0;
            RotationY = 0;
            RotationZ = 0;


			SpriteManager.AddToLayer(EntireScene, layerToAddTo);
			if(EntireScene.Parent == null)
			{
				EntireScene.AttachTo(this, true);
			}

            X = oldX;
            Y = oldY;
            Z = oldZ;
            RotationX = oldRotationX;
            RotationY = oldRotationY;
            RotationZ = oldRotationZ;
                		}
		public virtual void ConvertToManuallyUpdated()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			SpriteManager.ConvertToManuallyUpdated(EntireScene);
		}
		public static void LoadStaticContent(string contentManagerName)
		{
			ContentManagerName = contentManagerName;
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
			if(IsStaticContentLoaded == false)
			{
				IsStaticContentLoaded = true;
				lock(mLockObject)
				{
					if(!mHasRegisteredUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("LeftRightButtonStaticUnload", UnloadStaticContent);
						mHasRegisteredUnload = true;
					}
				}
				bool registerUnload = false;
				if(!FlatRedBallServices.IsLoaded<AnimationChainList>(@"content/entities/leftrightbutton/animationchainlistfile.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				AnimationChainListFile = FlatRedBallServices.Load<AnimationChainList>(@"content/entities/leftrightbutton/animationchainlistfile.achx", ContentManagerName);
				if(!FlatRedBallServices.IsLoaded<Scene>(@"content/entities/leftrightbutton/scenefile.scnx", ContentManagerName))
				{
					registerUnload = true;
				}
				SceneFile = FlatRedBallServices.Load<Scene>(@"content/entities/leftrightbutton/scenefile.scnx", ContentManagerName);
			if(registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock(mLockObject)
				{
					if(!mHasRegisteredUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("LeftRightButtonStaticUnload", UnloadStaticContent);
						mHasRegisteredUnload = true;
					}
				}
			}
				CustomLoadStaticContent(contentManagerName);
			}
		}
		public static void UnloadStaticContent()
		{
			IsStaticContentLoaded = false;
			mHasRegisteredUnload = false;
			if(AnimationChainListFile != null)
			{
				AnimationChainListFile = null;
			}
			if(SceneFile != null)
			{
				SceneFile.RemoveFromManagers(ContentManagerName != "Global");
				SceneFile = null;
			}
		}
		public static object GetStaticMember(string memberName)
		{
			switch(memberName)
			{
				case "AnimationChainListFile":
					return AnimationChainListFile;
				case "SceneFile":
					return SceneFile;
			}
			return null;
		}
		static VariableState mLoadingState = VariableState.Uninitialized;
		public static VariableState LoadingState
		{
			get
			{
				return mLoadingState;
			}
			set
			{
				mLoadingState = value;
			}
		}
		VariableState mCurrentState = VariableState.Uninitialized;
		public VariableState CurrentState
		{
			get
			{
				return mCurrentState;
			}
			set
			{
				mCurrentState = value;
				switch(mCurrentState)
				{
					case VariableState.Inactive:
						break;
					case VariableState.Normal:
						break;
					case VariableState.Pressed:
						break;
				}
			}
		}
		public void InterpolateToState(VariableState stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case VariableState.Inactive:
						break;
				case VariableState.Normal:
						break;
				case VariableState.Pressed:
						break;
			}
			this.Instructions.Add(new MethodInstruction<LeftRightButton>(
				this, "StopStateInterpolation", new object[]{stateToInterpolateTo}, TimeManager.CurrentTime + secondsToTake));
		}

		public void StopStateInterpolation(VariableState stateToStop)
		{
			switch(stateToStop)
			{
				case VariableState.Inactive:
						break;
				case VariableState.Normal:
						break;
				case VariableState.Pressed:
						break;
			}
			CurrentState = stateToStop;
		}

		object GetMember(string memberName)
		{
			switch(memberName)
			{
				case "AnimationChainListFile":
					return AnimationChainListFile;
				case "SceneFile":
					return SceneFile;
			}
			return null;
		}

    }
	
	
	// Extra classes
	public static class LeftRightButtonExtensionMethods
	{
	}
	
}
