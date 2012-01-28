using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math;
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

        public bool canMove;
        public Direction direction;

        public float gridSizeX = 2.0f;
        public float gridSizeY = 2.0f;

        public float previousX;
        public float previousY;

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
            direction = Direction.Right;

            previousX = X;
            previousY = Y;
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

        public void Move(PositionedObjectList<SnakeBody> SnakeBodyList)
        {
            previousX = X;
            previousY = Y;

            if (direction == Direction.Up)
            {
                if (canMoveUp(SnakeBodyList))
                {
                    this.Y += gridSizeY;
                    this.RotationZ = (float)Math.PI / 2;
                }
                else
                {
                    this.Y -= gridSizeY;
                    this.RotationZ = (float)Math.PI * 3 / 2;
                }
            }
            else if (direction == Direction.Down)
            {
                if (canMoveDown(SnakeBodyList))
                {
                    this.Y -= gridSizeY;
                    this.RotationZ = (float)Math.PI * 3 / 2;
                }
                else
                {
                    this.Y += gridSizeY;
                    this.RotationZ = (float)Math.PI / 2;
                }
            }
            else if (direction == Direction.Left)
            {
                if (canMoveLeft(SnakeBodyList))
                {
                    this.X -= gridSizeX;
                    this.RotationZ = (float)Math.PI;
                }
                else
                {
                    this.X += gridSizeX;
                    this.RotationZ = 0.0f;
                }
            }
            else if (direction == Direction.Right)
            {
                if (canMoveRight(SnakeBodyList))
                {
                    this.X += gridSizeX;
                    this.RotationZ = 0.0f;
                }
                else
                {
                    this.X -= gridSizeX;
                    this.RotationZ = (float)Math.PI;
                }
            }
        }

        private bool canMoveUp(PositionedObjectList<SnakeBody> SnakeBodyList)
        {
            bool ableToMove = true;

            float newY = this.Y + gridSizeY;
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody.Y == newY && snakeBody.X == this.X)
                {
                    ableToMove = false;
                }
                else
                {
                    ableToMove = true;
                }
            }
            return ableToMove;
        }

        private bool canMoveDown(PositionedObjectList<SnakeBody> SnakeBodyList)
        {
            bool ableToMove = true;

            float newY = this.Y - gridSizeY;
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody.Y == newY && snakeBody.X == this.X)
                {
                    ableToMove = false;
                }
                else
                {
                    ableToMove = true;
                }
            }
            return ableToMove;
        }

        private bool canMoveLeft(PositionedObjectList<SnakeBody> SnakeBodyList)
        {
            bool ableToMove = true;

            float newX = this.X - gridSizeX;
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody.X == newX && snakeBody.Y == this.Y)
                {
                    ableToMove = false;
                }
                else
                {
                    ableToMove = true;
                }
            }
            return ableToMove;
        }

        private bool canMoveRight(PositionedObjectList<SnakeBody> SnakeBodyList)
        {
            bool ableToMove = true;

            float newX = this.X + gridSizeX;
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody.X == newX && snakeBody.Y == this.Y)
                {
                    ableToMove = false;
                }
                else
                {
                    ableToMove = true;
                }
            }
            return ableToMove;
        }
	}
}
