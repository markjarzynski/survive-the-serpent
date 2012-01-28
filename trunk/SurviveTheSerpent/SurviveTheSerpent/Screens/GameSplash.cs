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

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

namespace SurviveTheSerpent.Screens
{
	public partial class GameSplash
	{
        private Cursor cursor;

		void CustomInitialize()
		{
            GuiManager.IsUIEnabled = true;

            cursor = GuiManager.Cursor;

		}

		void CustomActivity(bool firstTimeCalled)
		{
            if (cursor.PrimaryClick && upButton.HasCursorOver(cursor))
            {
                Player.X += 1;
                System.Console.WriteLine("up button code here");
            }
            else if (cursor.PrimaryClick && downButton.HasCursorOver(cursor))
            {
                Player.X -= 1;
                System.Console.WriteLine("down button code here");
            }
            else if (cursor.PrimaryClick && leftButton.HasCursorOver(cursor))
            {
                Player.Y += 1;
                System.Console.WriteLine("left button code here");
            }
            else if (cursor.PrimaryClick && rightButton.HasCursorOver(cursor))
            {
                Player.Y -= 1;
                System.Console.WriteLine("right button code here");
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
