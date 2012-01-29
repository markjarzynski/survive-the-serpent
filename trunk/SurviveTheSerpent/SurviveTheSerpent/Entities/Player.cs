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

	public partial class Player
	{
        // Direction Code:
        // U,D,L,R = directions
        // S = standing still
        public Direction direction;
        private const double PLAYER_SPEED = .5;
        private double velocity;

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            Still
        };

		private void CustomInitialize()
		{
            this.X = 0;
            this.Y = 0;
            this.direction = Direction.Still;
            this.velocity = PLAYER_SPEED;


		}

		private void CustomActivity()
		{
            switch (direction)
            {
                case Direction.Up:
                    X += (float)velocity;
                    break;
                case Direction.Down:
                    X -= (float)velocity;
                    break;
                case Direction.Left:
                    Y += (float)velocity;
                    break;
                case Direction.Right:
                    Y -= (float)velocity;
                    break;
                case Direction.Still:
                    break;
                default:
                    break;
}

		}

        public void SetDirection( Direction newDirection )
        {
            direction = newDirection;
        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
