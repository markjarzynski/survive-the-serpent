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

        private int GridSizeX = 2;
        private int GridSizeY = 2;

        private double snakeUpdateDelay = 1.0; // in seconds
        private double snakeTimeSinceLastUpdate;
        
		void CustomInitialize()
		{
            GuiManager.IsUIEnabled = true;

            cursor = GuiManager.Cursor;

            Entities.Food newFood = new Entities.Food(ContentManagerName);
            FoodList.Add(newFood);

            Entities.Obstacle newObstacle = new Entities.Obstacle(ContentManagerName);
            ObstacleList.Add(newObstacle);

            // init snake stuff
            snakeTimeSinceLastUpdate = TimeManager.CurrentTime;
            Entities.SnakeBody newSnakeBody = new Entities.SnakeBody(ContentManagerName);
            SnakeBodyList.Add(newSnakeBody);
		}

		void CustomActivity(bool firstTimeCalled)
		{
            UpdatePlayer();
            UpdateSnake();
		}

        void UpdatePlayer()
        {
            if (cursor.PrimaryClick && upButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Up);
                //Player.X += 1;
                System.Console.WriteLine("up button code here");
            }
            else if (cursor.PrimaryClick && downButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Down);
                //Player.X -= 1;
                System.Console.WriteLine("down button code here");
            }
            else if (cursor.PrimaryClick && leftButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Left);
                //Player.Y += 1;
                System.Console.WriteLine("left button code here");
            }
            else if (cursor.PrimaryClick && rightButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Right);
                //Player.Y -= 1;
                System.Console.WriteLine("right button code here");
            }

            if (Player.Body.CollideAgainstMove(CollisionFile, 0, 1))
            {
                Player.SetDirection(Entities.Player.Direction.Still);
            }

            foreach (Entities.Obstacle obstacle in ObstacleList)
            {
                if (Player.Body.CollideAgainstMove(obstacle.Body, 0, 1))
                {
                    Player.SetDirection(Entities.Player.Direction.Still);
                }
            }

        }

        void UpdateSnake()
        {
            // Don't start moving the snake until Player first starts moving
            if (!SnakeHead.canMove && (Player.direction != Entities.Player.Direction.Still))
            {
                SnakeHead.canMove = true;
            }

            if (SnakeHead.canMove && TimeManager.SecondsSince(snakeTimeSinceLastUpdate) > snakeUpdateDelay)
            {
                // Figure out the direction of the snake head
                double angle = Math.Atan2(Player.Y - SnakeHead.Y, Player.X - SnakeHead.X);
                SnakeHead.ChangeDirectionByAngle(angle);

                float previousX = SnakeHead.X;
                float previousY = SnakeHead.Y;

                SnakeHead.Move();

                // Move the snake body after the snake head
                SnakeBodyList[0].setX(SnakeHead.previousX);
                SnakeBodyList[0].setY(SnakeHead.previousY);
                SnakeBodyList[0].RotationZ = (float)Math.Atan2(SnakeHead.Y - SnakeBodyList[0].previousY, SnakeHead.X - SnakeBodyList[0].previousX);
                

                for( int i = 1; i < SnakeBodyList.Count; i++ )
                //foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
                {
                    SnakeBodyList[i].setX(SnakeBodyList[i - 1].previousX);
                    SnakeBodyList[i].setY(SnakeBodyList[i - 1].previousY);
                    SnakeBodyList[i].RotationZ = (float)Math.Atan2(SnakeBodyList[i - 1].Y - SnakeBodyList[i].previousY, SnakeBodyList[i - 1].X - SnakeBodyList[i].previousX);
                   
                    /*
                    float tempX = snakeBody.X;
                    float tempY = snakeBody.Y;

                    snakeBody.RotationZ = (float)Math.Atan2(snakeBody.Y - snakeBody.previousY, snakeBody.X - snakeBody.previousX);

                    snakeBody.setX( previousX );
                    snakeBody.setY( previousY ) ;

                    previousX = tempX;
                    previousY = tempY;
                    */
                }

                SnakeTail.RotationZ = (float)Math.Atan2(SnakeBodyList.Last.Y - SnakeBodyList.Last.previousY, SnakeBodyList.Last.X - SnakeBodyList.Last.previousX);

                if (SnakeHead.didEatFood == false)
                {
                    SnakeTail.X = SnakeBodyList.Last.previousX;
                    SnakeTail.Y = SnakeBodyList.Last.previousY;
                }
                else
                {
                    SnakeHead.didEatFood = false;
                }

                // TODO: Snake head consumes food it collides with
                foreach (Entities.Food food in FoodList)
                {
                    if (SnakeHead.Body.CollideAgainst(food.Body))
                    {
                        Entities.SnakeBody newSnakeBody = new Entities.SnakeBody(ContentManagerName);
                        newSnakeBody.X = SnakeBodyList.Last.X;
                        newSnakeBody.Y = SnakeBodyList.Last.Y;
                        newSnakeBody.RotationZ = SnakeBodyList.Last.RotationZ;

                        SnakeBodyList.Add(newSnakeBody);
                        FoodList.Remove(food);
                        food.Destroy();

                        SnakeHead.didEatFood = true;
                    }
                }

                // TODO: Randomly generate more food

                snakeTimeSinceLastUpdate = TimeManager.CurrentTime;
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
