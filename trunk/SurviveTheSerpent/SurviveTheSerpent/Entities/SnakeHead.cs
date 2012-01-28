using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;


#endif

namespace SurviveTheSerpent.Entities
{
	public partial class SnakeHead
	{
		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{
            if (this.Direction == "North")
            {
                this.RotationZ = (float)Math.PI;
            }
            else if (this.Direction == "East")
            {
                this.RotationZ = (float)Math.PI/2;
            }
            else if (this.Direction == "South")
            {
                this.RotationZ = 0.0f;
            }
            else if (this.Direction == "West")
            {
                this.RotationZ = (float)Math.PI * 3 / 2;
            }

		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void ChangeDirectionByAngle(double radians)
        {
            if (0.0 > radians && radians < Math.PI / 2)
            {
                this.Direction = "North";
            }
            else if (Math.PI/2 > radians && radians < Math.PI)
            {
                this.Direction = "East";
            }
            else if (0.0 < radians && radians > -Math.PI/2)
            {
                this.Direction = "South";
            }
            else if (-Math.PI/2 < radians && radians > -Math.PI)
            {
                this.Direction = "West";
            }
            else
            {

            }
        }
	}
}
