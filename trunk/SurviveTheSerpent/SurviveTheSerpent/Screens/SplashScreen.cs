using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using FlatRedBall.Gui;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
//using Mouse = Microsoft.Xna.Framework.Input.MouseState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

namespace SurviveTheSerpent.Screens
{
	public partial class SplashScreen
	{
        private Cursor cursor;

		void CustomInitialize()
		{
            GuiManager.IsUIEnabled = true;

            cursor = GuiManager.Cursor;
		}

		void CustomActivity(bool firstTimeCalled)
		{
            if (firstTimeCalled)
            {
             
            }

            if (cursor.PrimaryClick && startButton.HasCursorOver(cursor))
            {
                this.MoveToScreen(typeof(GameSplash).FullName);
                Game1.hiss.Play();
            }

            if (cursor.PrimaryClick && creditButton.HasCursorOver(cursor))
            {
                this.MoveToScreen(typeof(CreditScreen).FullName);
            }

            if (cursor.PrimaryClick && instructionButton.HasCursorOver(cursor))
            {
                this.MoveToScreen(typeof(InstructionScreen).FullName);
            }

		}

		void CustomDestroy()
		{

		}

        static void CustomLoadStaticContent(string contentManagerName)
        {

        }

	}
}
