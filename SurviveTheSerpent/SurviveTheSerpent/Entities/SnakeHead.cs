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
            direction = Direction.Right;

            previousX = X;
            previousY = Y;
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

        private void ChangeDirection(Entities.Player player, PositionedObjectList<SnakeBody> snakeBodyList, PositionedObjectList<Player> ghostPlayerList)
        {
            bool canRight = canMoveRight(snakeBodyList, ghostPlayerList);
            bool canLeft = canMoveLeft(snakeBodyList, ghostPlayerList);
            bool canUp = canMoveUp(snakeBodyList, ghostPlayerList);
            bool canDown = canMoveDown(snakeBodyList, ghostPlayerList);

            double rightDistance = Math.Sqrt((player.Y - Y) * (player.Y - Y) + (player.X - (X + gridSizeX)) * (player.X - (X + gridSizeX)));
            double leftDistance = Math.Sqrt((player.Y - Y) * (player.Y - Y) + (player.X - (X - gridSizeX)) * (player.X - (X - gridSizeX)));
            double upDistance = Math.Sqrt((player.Y - (Y + gridSizeY)) * (player.Y - (Y + gridSizeY)) + (player.X - X) * (player.X - X));
            double downDistance = Math.Sqrt((player.Y - (Y - gridSizeY)) * (player.Y - (Y - gridSizeY)) + (player.X - X) * (player.X - X));

            if (!canRight && !canLeft && !canUp && !canDown)
            {
                isDead = true;
            }
            else if (!canRight && !canLeft && !canUp && canDown)
            {
                direction = Direction.Down;
            }
            else if (!canRight && !canLeft && canUp && !canDown)
            {
                direction = Direction.Up;
            }
            else if (!canRight && canLeft && !canUp && !canDown)
            {
                direction = Direction.Left;
            }
            else if (canRight && !canLeft && !canUp && !canDown)
            {
                direction = Direction.Right;
            }
            else if (!canRight && !canLeft && canUp && canDown)
            {
                if (upDistance < downDistance)
                {
                    direction = Direction.Up;
                }
                else
                {
                    direction = Direction.Down;
                }
            }
            else if (!canRight && canLeft && !canUp && canDown)
            {
                if (leftDistance < downDistance)
                {
                    direction = Direction.Left;
                }
                else
                {
                    direction = Direction.Down;
                }
            }
            else if (canRight && !canLeft && !canUp && canDown)
            {
                if (rightDistance < downDistance)
                {
                    direction = Direction.Right;
                }
                else
                {
                    direction = Direction.Down;
                }
            }
            else if (!canRight && canLeft && canUp && !canDown)
            {
                if (leftDistance < upDistance)
                {
                    direction = Direction.Left;
                }
                else
                {
                    direction = Direction.Up;
                }
            }

            else if (canRight && !canLeft && canUp && !canDown)
            {
                if (rightDistance < upDistance)
                {
                    direction = Direction.Right;
                }
                else
                {
                    direction = Direction.Up;
                }
            }
            else if (canRight && canLeft && !canUp && !canDown)
            {
                if (rightDistance < leftDistance)
                {
                    direction = Direction.Right;
                }
                else
                {
                    direction = Direction.Left;
                }
            }
            else if (!canRight && canLeft && canUp && canDown)
            {
                if (leftDistance < upDistance && leftDistance < downDistance)
                {
                    direction = Direction.Left;
                }
                else
                {
                    if (upDistance < downDistance)
                    {
                        direction = Direction.Up;
                    }
                    else
                    {
                        direction = Direction.Down;
                    }
                }
            }
            else if (canRight && !canLeft && canUp && canDown)
            {
                if (rightDistance < upDistance && rightDistance < downDistance)
                {
                    direction = Direction.Right;
                }
                else
                {
                    if (upDistance < downDistance)
                    {
                        direction = Direction.Up;
                    }
                    else
                    {
                        direction = Direction.Down;
                    }
                }
            }
            else if (canRight && canLeft && !canUp && canDown)
            {
                if (rightDistance < leftDistance && rightDistance < downDistance)
                {
                    direction = Direction.Right;
                }
                else
                {
                    if (leftDistance < downDistance)
                    {
                        direction = Direction.Left;
                    }
                    else
                    {
                        direction = Direction.Down;
                    }
                }
            }
            else if (canRight && canLeft && canUp && !canDown)
            {
                if (rightDistance < leftDistance && rightDistance < upDistance)
                {
                    direction = Direction.Right;
                }
                else
                {
                    if (leftDistance < upDistance)
                    {
                        direction = Direction.Left;
                    }
                    else
                    {
                        direction = Direction.Up;
                    }
                }
            }
            else
            {
                //we should never ever reach this point
            }
        }

        public void ChangeDirectionByAngle(Entities.Player player, PositionedObjectList<SnakeBody> snakeBodyList, 
            PositionedObjectList<Player> ghostPlayerList)
        {
            double radians = Math.Atan2(player.Y - Y, player.X - X);

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

            this.ChangeDirection(player, snakeBodyList, ghostPlayerList);
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

        private bool canMoveUp(PositionedObjectList<SnakeBody> SnakeBodyList, PositionedObjectList<Player> GhostPlayerList)
        {
            if (direction == Direction.Down)
            {
                return false;
            }
            float newY = this.Y + gridSizeY;
            if (newY > 12)
            {
                return false;
            }
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody == SnakeBodyList.Last) continue;
                if (snakeBody.Y == newY && snakeBody.X == this.X)
                {
                    return false;
                }
            }
            foreach (Entities.Player ghostPlayer in GhostPlayerList)
            {
                if (Math.Abs(ghostPlayer.Y-newY) < 1 && Math.Abs(ghostPlayer.X-this.X) < 1.5)
                {
                    return false;
                }
            }

            return true;
        }

        private bool canMoveDown(PositionedObjectList<SnakeBody> SnakeBodyList, PositionedObjectList<Player> GhostPlayerList)
        {
            if (direction == Direction.Up)
            {
                return false;
            }
            float newY = this.Y - gridSizeY;
            if (newY < -12)
            {
                return false;
            }
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody == SnakeBodyList.Last) continue;
                if (snakeBody.Y == newY && snakeBody.X == this.X)
                {
                    return false;
                }
            }
            foreach (Entities.Player ghostPlayer in GhostPlayerList)
            {
                if (Math.Abs(ghostPlayer.Y - newY) < 1 && Math.Abs(ghostPlayer.X - this.X) < 1.5)
                {
                    return false;
                }
            }
            return true;
        }

        private bool canMoveLeft(PositionedObjectList<SnakeBody> SnakeBodyList, PositionedObjectList<Player> GhostPlayerList)
        {
            if (direction == Direction.Right)
            {
                return false;
            }
            float newX = this.X - gridSizeX;
            if (newX < -7)
            {
                return false;
            }
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody == SnakeBodyList.Last) continue;
                if (snakeBody.X == newX && snakeBody.Y == this.Y)
                {
                    return false;

                }
            }
            foreach (Entities.Player ghostPlayer in GhostPlayerList)
            {
                if (Math.Abs(ghostPlayer.X - newX) < 1.5 && Math.Abs(ghostPlayer.Y - this.Y) < 1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool canMoveRight(PositionedObjectList<SnakeBody> SnakeBodyList, PositionedObjectList<Player> GhostPlayerList)
        {
            if (direction == Direction.Left )
            {
                return false;
            }
            float newX = this.X + gridSizeX;
            if (newX > 7)
            {
                return false;
            }
            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (snakeBody == SnakeBodyList.Last) continue;
                if (snakeBody.X == newX && snakeBody.Y == this.Y)
                {
                    return false;
                }
            }
            foreach (Entities.Player ghostPlayer in GhostPlayerList)
            {
                if (Math.Abs(ghostPlayer.X - newX) < 1.5 && Math.Abs(ghostPlayer.Y - this.Y) < 1)
                {
                    return false;
                }
            }
            return true;
        }
	}
}
