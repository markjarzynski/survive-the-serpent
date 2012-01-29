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
        public Direction bestDirection;

        public float gridSizeX = 2.0f;
        public float gridSizeY = 2.0f;

        public float previousX;
        public float previousY;
        public bool didEatFood;
        public bool isDead;

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            None
        };

		private void CustomInitialize()
		{
            canMove = false;
            didEatFood = false;
            direction = Direction.None;

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

        private void ChangeDirection(PositionedObjectList<SnakeBody> snakeBodyList)
        {
            isDead = false;

            direction = Direction.None;
            if (bestDirection == Direction.Right)
            {
                if (canMoveRight(snakeBodyList))
                {
                    direction = Direction.Right;
                } 
                else if (canMoveDown(snakeBodyList))
                {
                    direction = Direction.Down;
                }
                else if (canMoveUp(snakeBodyList))
                {
                    direction = Direction.Up;
                }
                else if (canMoveLeft(snakeBodyList))
                {
                    direction = Direction.Left;
                }
            }
            else if (bestDirection == Direction.Down)
            {

                if (canMoveDown(snakeBodyList))
                {
                    direction = Direction.Down;
                }
                else if (canMoveLeft(snakeBodyList))
                {
                    direction = Direction.Left;
                }
                else if (canMoveRight(snakeBodyList))
                {
                    direction = Direction.Right;
                }
                else if (canMoveUp(snakeBodyList))
                {
                    direction = Direction.Up;
                }
            }
            else if (bestDirection == Direction.Left)
            {

                if (canMoveLeft(snakeBodyList))
                {
                    direction = Direction.Left;
                }
                else if (canMoveUp(snakeBodyList))
                {
                    direction = Direction.Up;
                }
                else if (canMoveDown(snakeBodyList))
                {
                    direction = Direction.Down;
                }
                else if (canMoveRight(snakeBodyList))
                {
                    direction = Direction.Right;
                }

            } else if (bestDirection == Direction.Up)
            {

                if (canMoveUp(snakeBodyList))
                {
                    direction = Direction.Up;
                }
                else if (canMoveRight(snakeBodyList))
                {
                    direction = Direction.Right;
                }
                else if (canMoveLeft(snakeBodyList))
                {
                    direction = Direction.Left;
                }
                else if (canMoveDown(snakeBodyList))
                {
                    direction = Direction.Down;
                }
            }

            if (direction == Direction.None)
            {
                isDead = true;
            }
        }

        public void ChangeDirectionByAngle(double radians, PositionedObjectList<SnakeBody> snakeBodyList)
        {
            direction = Direction.None;

            if ( radians > -Math.PI / 4 && radians < Math.PI / 4)
            {
                bestDirection = Direction.Right;
                //this.RotationZ = 0.0f;
            }
            else if (radians > Math.PI / 4 && radians < Math.PI * 3 / 4)
            {
                bestDirection = Direction.Up;
                //this.RotationZ = (float)Math.PI / 2;
            }
            else if (radians > Math.PI * 3 / 4 || radians < -Math.PI * 3 / 4)
            {
                bestDirection = Direction.Left;
                //this.RotationZ = (float)Math.PI;
            }
            else if (radians < -Math.PI / 4 && radians > -Math.PI * 3 / 4)
            {
                bestDirection = Direction.Down;
                //this.RotationZ = (float)Math.PI * 3 / 2;
            }
            this.ChangeDirection(snakeBodyList);

        }

        public void Move(PositionedObjectList<SnakeBody> SnakeBodyList)
        {
            previousX = X;
            previousY = Y;

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
        }

        private bool canMoveUp(PositionedObjectList<SnakeBody> SnakeBodyList)
        {
            float newY = this.Y + gridSizeY;
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody.Y == newY && snakeBody.X == this.X)
                {
                    return false;
                }
            }
            return true;
        }

        private bool canMoveDown(PositionedObjectList<SnakeBody> SnakeBodyList)
        {
            float newY = this.Y - gridSizeY;
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody.Y == newY && snakeBody.X == this.X)
                {
                    return false;
                }
            }
            return true;
        }

        private bool canMoveLeft(PositionedObjectList<SnakeBody> SnakeBodyList)
        {
            float newX = this.X - gridSizeX;
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody.X == newX && snakeBody.Y == this.Y)
                {
                    return false;

                }
            }
            return true;
        }

        private bool canMoveRight(PositionedObjectList<SnakeBody> SnakeBodyList)
        {
            float newX = this.X + gridSizeX;
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody.X == newX && snakeBody.Y == this.Y)
                {
                    return false;
                }
            }
            return true;
        }
	}
}
