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
        private double updateDelay = 1.0; // in seconds
        private double timeSinceLastUpdate;
        public bool canMove;
        public Direction direction;

        public float gridSizeX = 2.0f;
        public float gridSizeY = 2.0f;

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        };

		private void CustomInitialize()
		{
            canMove = false;
            timeSinceLastUpdate = TimeManager.CurrentTime;
            direction = Direction.Right;
		}

		private void CustomActivity()
		{
            if (canMove && TimeManager.SecondsSince(timeSinceLastUpdate) > updateDelay)
            {
                if (direction == Direction.Up)
                {
                    this.Y += gridSizeY;
                    this.RotationZ = (float)Math.PI / 2;
                }
                else if (direction == Direction.Down)
                {
                    this.Y -= gridSizeY;
                    this.RotationZ = (float)Math.PI * 3 / 2;
                }
                else if (direction == Direction.Left)
                {
                    this.X -= gridSizeX;
                    this.RotationZ = (float)Math.PI;
                }
                else if (direction == Direction.Right)
                {
                    this.X += gridSizeX;
                    this.RotationZ = 0.0f;
                }
                timeSinceLastUpdate = TimeManager.CurrentTime;
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
            if ( radians > -Math.PI / 4 && radians < Math.PI / 4)
            {
                direction = Direction.Right;
                //this.RotationZ = 0.0f;
            }
            else if (radians > Math.PI / 4 && radians < Math.PI * 3 / 4)
            {
                direction = Direction.Up;
                //this.RotationZ = (float)Math.PI / 2;
            }
            else if (radians > Math.PI * 3 / 4 || radians < -Math.PI * 3 / 4)
            {
                direction = Direction.Left;
                //this.RotationZ = (float)Math.PI;
            }
            else if (radians < -Math.PI / 4 && radians > -Math.PI * 3 / 4)
            {
                direction = Direction.Down;
                //this.RotationZ = (float)Math.PI * 3 / 2;
            }
        }
	}
}
