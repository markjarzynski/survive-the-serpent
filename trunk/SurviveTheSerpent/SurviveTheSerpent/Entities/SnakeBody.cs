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
	public partial class SnakeBody
	{
        public float previousX;
        public float previousY;
        public float previousRotationZ;

		private void CustomInitialize()
		{
            this.EntireSceneCurrentChainName = "Straight";
            previousX = X;
            previousY = Y;
            previousRotationZ = RotationZ;
            Body.Visible = false;
		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void Straight()
        {
            this.EntireSceneCurrentChainName = "Straight";
            Body.Visible = false;
        }

        public void Elbow()
        {
            this.EntireSceneCurrentChainName = "Elbow";
            Body.Visible = false;
        }

        public void setX( float newX )
        {
            previousX = X;
            X = newX;
        }

        public void setY( float newY )
        {
            previousY = Y;
            Y = newY;
        }

        public void setRotationZ(float newRotationZ)
        {
            previousRotationZ = Z;
            Z = newRotationZ;

        }

	}
}
