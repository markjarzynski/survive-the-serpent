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

        private const double FOOD_SPAWN_CHANCE = .2;

        private int gameOverDelay;
        private bool isGameOver;

        void CreateSnake()
        {
            SnakeTail = new Entities.SnakeTail(ContentManagerName);
            SnakeTail.X = -2;
            SnakeTail.Y = 8;
            SnakeHead = new Entities.SnakeHead(ContentManagerName);
            SnakeHead.X = 2;
            SnakeHead.Y = 8;

            snakeTimeSinceLastUpdate = TimeManager.CurrentTime;
            Entities.SnakeBody newSnakeBody = new Entities.SnakeBody(ContentManagerName);
            newSnakeBody.X = 0;
            newSnakeBody.Y = 8;
            newSnakeBody.Visible = true;
            SnakeBodyList.Add(newSnakeBody);

            //DEBUG

            newSnakeBody = new Entities.SnakeBody(ContentManagerName);
            newSnakeBody.X = 0;
            newSnakeBody.Y = 6;
            newSnakeBody.Visible = true;
            SnakeBodyList.Add(newSnakeBody);

            newSnakeBody = new Entities.SnakeBody(ContentManagerName);
            newSnakeBody.X = -2;
            newSnakeBody.Y = 6;
            newSnakeBody.Visible = true;
            SnakeBodyList.Add(newSnakeBody);

            //DEBUG END

        }

		void CustomInitialize()
		{
            gameOverDelay = 0;
            isGameOver = false;
            GuiManager.IsUIEnabled = true;

            cursor = GuiManager.Cursor;

            //Entities.Food newFood = new Entities.Food(ContentManagerName);
            //FoodList.Add(newFood);

            Entities.Obstacle newObstacle = new Entities.Obstacle(ContentManagerName);
            ObstacleList.Add(newObstacle);

            // init snake stuff
            SnakeHead.Destroy();
            SnakeTail.Destroy();
            CreateSnake();


            SpawnFood();
		}

		void CustomActivity(bool firstTimeCalled)
		{
            UpdatePlayer();
            UpdateSnake();
            UpdateFood();
		}

        void UpdateFood()
        {
            Random rand = new Random();
            if (rand.Next(1) < FOOD_SPAWN_CHANCE && FoodList.Count < 1)
            {
                SpawnFood();
            }
        }

        void SpawnFood()
        {
            Entities.Food newFood = new Entities.Food(ContentManagerName);
            Boolean invalidLocation = true;

            Random rand = new Random();

            while (invalidLocation)
            {
                newFood.X = rand.Next(7);
                newFood.Y = rand.Next(12);
                if (rand.NextDouble() < .5)
                {
                    newFood.X *= -1;
                }
                if (rand.NextDouble() < .5)
                {
                    newFood.Y *= -1;
                }
                if ((!newFood.Body.CollideAgainstMove(Player.Body, 0, 1)) &&
                    (!newFood.Body.CollideAgainstMove(SnakeHead.Body, 0, 1)) &&
                    (!newFood.Body.CollideAgainstMove(SnakeTail.Body, 0, 1)) &&
                    (!newFood.Body.CollideAgainstMove(CollisionFile, 0, 1)))
                {
                    invalidLocation = false;
                    foreach (Entities.Obstacle obstacle in ObstacleList)
                    {
                        if (newFood.Body.CollideAgainstMove(obstacle.Body, 0, 1))
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.Food food in FoodList)
                    {
                        if (newFood.Body.CollideAgainstMove(food.Body, 0, 1))
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
                    {
                        if (newFood.Body.CollideAgainstMove(snakeBody.Body, 0, 1))
                        {
                            invalidLocation = true;
                            break;
                        }
                    }
                }
            }
            FoodList.Add(newFood);
        }

        void UpdatePlayer()
        {
            if (cursor.PrimaryClick && upButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Up);
                Player.EntireSceneCurrentChainName = "up";
                Player.RotationY = 0;
                Player.EntireSceneAnimate = true;
                //Player.X += 1;
                upButton.EntireSceneCurrentChainName = "Glow";
                downButton.EntireSceneCurrentChainName = "NoGlow";
                rightButton.EntireSceneCurrentChainName = "NoGlow";
                leftButton.EntireSceneCurrentChainName = "NoGlow";
            }
            else if (cursor.PrimaryClick && downButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Down);
                Player.EntireSceneCurrentChainName = "down";
                Player.RotationY = 0;
                Player.EntireSceneAnimate = true;
                //Player.X -= 1;
                upButton.EntireSceneCurrentChainName = "NoGlow";
                downButton.EntireSceneCurrentChainName = "Glow";
                rightButton.EntireSceneCurrentChainName = "NoGlow";
                leftButton.EntireSceneCurrentChainName = "NoGlow";
            }
            else if (cursor.PrimaryClick && leftButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Left);
                Player.EntireSceneCurrentChainName = "side";
                Player.RotationY = (float)Math.PI;
                Player.EntireSceneAnimate = true;
                //Player.Y += 1;
                upButton.EntireSceneCurrentChainName = "NoGlow";
                downButton.EntireSceneCurrentChainName = "NoGlow";
                rightButton.EntireSceneCurrentChainName = "NoGlow";
                leftButton.EntireSceneCurrentChainName = "Glow";
            }
            else if (cursor.PrimaryClick && rightButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Right);
                Player.EntireSceneCurrentChainName = "side";
                Player.RotationY = 0;
                Player.EntireSceneAnimate = true;
                //Player.Y -= 1;
                upButton.EntireSceneCurrentChainName = "NoGlow";
                downButton.EntireSceneCurrentChainName = "NoGlow";
                rightButton.EntireSceneCurrentChainName = "Glow";
                leftButton.EntireSceneCurrentChainName = "NoGlow";
            }

            if (Player.Body.CollideAgainst(SnakeHead.Body))
            {
                Player.EntireSceneAnimate = false;
                isGameOver = true;
                Player.SetDirection(Entities.Player.Direction.Still);
                //this.MoveToScreen(typeof(GameOverScreen).FullName);
            }

            if (Player.Body.CollideAgainstMove(CollisionFile, 0, 1))
            {
                //Player.SetDirection(Entities.Player.Direction.Still);
            }

            foreach (Entities.Obstacle obstacle in ObstacleList)
            {
                if (Player.Body.CollideAgainstMove(obstacle.Body, 0, 1))
                {
                    //Player.SetDirection(Entities.Player.Direction.Still);
                }
            }

            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                if (Player.Body.CollideAgainstMove(snakeBody.Body, 0, 1))
                {
                    //Player.SetDirection(Entities.Player.Direction.Still);
                }
            }

            foreach (Entities.StoneSnakeHead stoneSnakeHead in StoneSnakeHeadList)
            {
                if (Player.Body.CollideAgainstMove(stoneSnakeHead.Body, 0, 1))
                {
                    //Player.SetDirection(Entities.Player.Direction.Still);
                }
            }

            foreach (Entities.StoneSnakeBody stoneSnakeBody in StoneSnakeBodyList)
            {
                if (Player.Body.CollideAgainstMove(stoneSnakeBody.Body, 0, 1))
                {
                    //Player.SetDirection(Entities.Player.Direction.Still);
                }
            }

            foreach (Entities.StoneSnakeTail stoneSnakeTail in StoneSnakeTailList)
            {
                if (Player.Body.CollideAgainstMove(stoneSnakeTail.Body, 0, 1))
                {
                    //Player.SetDirection(Entities.Player.Direction.Still);
                }
            }

            if (Player.Body.CollideAgainstMove(SnakeTail.Body, 0, 1))
            {
                //Player.SetDirection(Entities.Player.Direction.Still);
            }
            if (isGameOver == true)
            {
                Player.EntireSceneAnimate = false;
                Player.SetDirection(Entities.Player.Direction.Still);
            }

        }

        void KillSnake()
        {

            Entities.StoneSnakeHead newStoneSnakeHead = new Entities.StoneSnakeHead(ContentManagerName);
            newStoneSnakeHead.RotationZ = SnakeHead.RotationZ;
            newStoneSnakeHead.X = SnakeHead.X;
            newStoneSnakeHead.Y = SnakeHead.Y;
            StoneSnakeHeadList.Add(newStoneSnakeHead);
            SnakeHead.Destroy();

            while (SnakeBodyList.Count > 0)
            {
                Entities.SnakeBody snakeBody = SnakeBodyList.Last;
                Entities.StoneSnakeBody newStoneSnakeBody = new Entities.StoneSnakeBody(ContentManagerName);
                newStoneSnakeBody.RotationZ = snakeBody.RotationZ;
                newStoneSnakeBody.X = snakeBody.X;
                newStoneSnakeBody.Y = snakeBody.Y;
                newStoneSnakeBody.EntireSceneCurrentChainName = snakeBody.EntireSceneCurrentChainName;

                StoneSnakeBodyList.Add(newStoneSnakeBody);

                SnakeBodyList.Remove(snakeBody);
                snakeBody.Destroy();
            }

            Entities.StoneSnakeTail newStoneSnakeTail = new Entities.StoneSnakeTail(ContentManagerName);
            newStoneSnakeTail.RotationZ = SnakeTail.RotationZ;
            newStoneSnakeTail.X = SnakeTail.X;
            newStoneSnakeTail.Y = SnakeTail.Y;

            StoneSnakeTailList.Add(newStoneSnakeTail);

            SnakeTail.Destroy();

            CreateSnake();
            return;
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
                SnakeHead.ChangeDirectionByAngle(angle, SnakeBodyList);

                if (SnakeHead.isDead)
                {
                    //Kill the snake
                    KillSnake();


                }
                // If snake collides with tail, game over.
                if (SnakeHead.Body.CollideAgainst(SnakeTail.Body))
                {
                    KillSnake();
                }

                float previousX = SnakeHead.X;
                float previousY = SnakeHead.Y;

                SnakeHead.Move(SnakeBodyList);

                SnakeBodyList[0].setX(SnakeHead.previousX);
                SnakeBodyList[0].setY(SnakeHead.previousY);
                SnakeBodyList[0].RotationZ = (float)Math.Atan2(SnakeHead.Y - SnakeBodyList[0].previousY, SnakeHead.X - SnakeBodyList[0].previousX);

                if (Math.Abs(SnakeBodyList[0].RotationZ - Math.PI / 4) < 0.01)
                {
                    SnakeBodyList[0].Elbow();
                    SnakeBodyList[0].RotationZ = 0.0f;
                }
                else if (Math.Abs(SnakeBodyList[0].RotationZ - Math.PI * 3 / 4) < 0.01)
                {
                    SnakeBodyList[0].Elbow();
                    SnakeBodyList[0].RotationZ = (float)Math.PI / 2;
                }
                else if (Math.Abs(SnakeBodyList[0].RotationZ - Math.PI * 5 / 4) < 0.01)
                {
                    SnakeBodyList[0].Elbow();
                    SnakeBodyList[0].RotationZ = (float)Math.PI;
                }
                else if (Math.Abs(SnakeBodyList[0].RotationZ - Math.PI * 7 / 4) < 0.01)
                {
                    SnakeBodyList[0].Elbow();
                    SnakeBodyList[0].RotationZ = (float)Math.PI * 3 / 2;
                }
                else
                {
                    SnakeBodyList[0].Straight();
                }

                float px = SnakeBodyList[0].X - SnakeBodyList[0].previousX;
                float py = SnakeBodyList[0].Y - SnakeBodyList[0].previousY;
                float hx = SnakeBodyList[0].X - SnakeHead.X;
                float hy = SnakeBodyList[0].Y - SnakeHead.Y;

                if( py * hx > 0  ) 
                {
                    SnakeBodyList[0].RotationZ += (float)Math.PI;
                }
                if (px * hy < 0)
                {
                    SnakeBodyList[0].RotationZ += (float)Math.PI;
                }

                // Move the snake body after the snake head
                for( int i = 1; i < SnakeBodyList.Count; i++ )
                {
                    SnakeBodyList[i].setX(SnakeBodyList[i - 1].previousX);
                    SnakeBodyList[i].setY(SnakeBodyList[i - 1].previousY);
                    SnakeBodyList[i].RotationZ = (float)Math.Atan2(SnakeBodyList[i - 1].Y - SnakeBodyList[i].previousY, SnakeBodyList[i - 1].X - SnakeBodyList[i].previousX);

                    if (Math.Abs(SnakeBodyList[i].RotationZ - Math.PI / 4) < 0.01)
                    {
                        SnakeBodyList[i].Elbow();
                        SnakeBodyList[i].RotationZ = 0.0f;
                    }
                    else if (Math.Abs(SnakeBodyList[i].RotationZ - Math.PI * 3 / 4) < 0.01)
                    {
                        SnakeBodyList[i].Elbow();
                        SnakeBodyList[i].RotationZ = (float)Math.PI / 2;
                    }
                    else if (Math.Abs(SnakeBodyList[i].RotationZ - Math.PI * 5 / 4) < 0.01)
                    {
                        SnakeBodyList[i].Elbow();
                        SnakeBodyList[i].RotationZ = (float)Math.PI;
                    }
                    else if (Math.Abs(SnakeBodyList[i].RotationZ - Math.PI * 7 / 4) < 0.01)
                    {
                        SnakeBodyList[i].Elbow();
                        SnakeBodyList[i].RotationZ = (float)Math.PI * 3 / 2;
                    }
                    else
                    {
                        SnakeBodyList[i].Straight();
                    }

                    px = SnakeBodyList[i].X - SnakeBodyList[i].previousX;
                    py = SnakeBodyList[i].Y - SnakeBodyList[i].previousY;
                    hx = SnakeBodyList[i].X - SnakeBodyList[i-1].X;
                    hy = SnakeBodyList[i].Y - SnakeBodyList[i-1].Y;

                    if (py * hx > 0)
                    {
                        SnakeBodyList[i].RotationZ += (float)Math.PI;
                    }
                    if (px * hy < 0)
                    {
                        SnakeBodyList[i].RotationZ += (float)Math.PI;
                    }


                }

                if (SnakeHead.didEatFood == false)
                {
                    SnakeTail.RotationZ = (float)Math.Atan2(SnakeBodyList.Last.Y - SnakeBodyList.Last.previousY, SnakeBodyList.Last.X - SnakeBodyList.Last.previousX);
                    SnakeTail.X = SnakeBodyList.Last.previousX;
                    SnakeTail.Y = SnakeBodyList.Last.previousY;
                }
                else
                {
                    SnakeHead.didEatFood = false;
                    SnakeBodyList.Last.Visible = true;
                }

                
                // Snake head consumes food it collides with
                foreach (Entities.Food food in FoodList)
                {
                    if (SnakeHead.Body.CollideAgainstMove(food.Body, 1 , 0))
                    {
                        //Game1.eat.Play();
                        Entities.SnakeBody newSnakeBody = new Entities.SnakeBody(ContentManagerName);
                        newSnakeBody.X = SnakeTail.X;
                        newSnakeBody.Y = SnakeTail.Y;
                        newSnakeBody.previousX = SnakeTail.X;
                        newSnakeBody.previousY = SnakeTail.Y;
                        newSnakeBody.RotationZ = SnakeBodyList.Last.RotationZ;
                        newSnakeBody.Visible = false;

                        SnakeBodyList.Add(newSnakeBody);
                        FoodList.Remove(food);
                        food.Destroy();

                        SnakeHead.didEatFood = true;
                        break;
                    }
                }

                // TODO: Randomly generate more food

                snakeTimeSinceLastUpdate = TimeManager.CurrentTime;

                if (isGameOver == true && gameOverDelay == 2)
                {
                    CustomDestroy();
                    this.MoveToScreen(typeof(GameOverScreen).FullName);
                }
                else if (gameOverDelay < 2)
                {
                    gameOverDelay += 1;
                }
            }
        }

		void CustomDestroy()
		{
            SnakeHead.Body.Visible = false;
            SnakeTail.Body.Visible = false;
            SnakeHead.Body.RemoveSelfFromListsBelongingTo();
            SnakeTail.Body.RemoveSelfFromListsBelongingTo();

            foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
            {
                snakeBody.Visible = false;
                snakeBody.RemoveSelfFromListsBelongingTo();
            }
            Player.Body.Visible = false;
            Player.Body.RemoveSelfFromListsBelongingTo();
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
