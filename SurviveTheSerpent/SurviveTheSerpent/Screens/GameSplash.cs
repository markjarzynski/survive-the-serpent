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
    //TODO: Bug with player dying
	public partial class GameSplash
	{
        private Cursor cursor;

        private int GridSizeX = 2;
        private int GridSizeY = 2;

        private Random rand = new Random();
        private const int MAX_ATTEMPTS = 10;

        private double snakeUpdateDelay = 0.8; // in seconds
        private double snakeVelocity = 0.8;
        private double snakeAcceleration = 0.2;
        private double snakeTimeSinceLastUpdate;

        private const double FOOD_SPAWN_CHANCE = .2;

        private int gameOverDelay;
        private bool isGameOver;
        private bool ghostTrip;

        void NewGame()
        {
            CustomDestroy();
            isGameOver = false;
            ghostTrip = false;

            Player = new Entities.Player(ContentManagerName);

            // init snake stuff
            CreateSnake();

            SpawnObstacle();
            SpawnObstacle();
            SpawnFood();

            upButton.EntireSceneCurrentChainName = "NoGlow";
            downButton.EntireSceneCurrentChainName = "NoGlow";
            rightButton.EntireSceneCurrentChainName = "NoGlow";
            leftButton.EntireSceneCurrentChainName = "NoGlow";
        }

        void CreateSnake()
        {
            switch (Game1.diff)
            {
                case Game1.Difficulty.easy:
                    snakeUpdateDelay = 0.9; // in seconds
                    snakeVelocity = 0.9;
                    snakeAcceleration = 0.1;
                    break;
                    
                case Game1.Difficulty.medium:
                    snakeUpdateDelay = 0.75; // in seconds
                    snakeVelocity = 0.75;
                    snakeAcceleration = 0.2;
                    break;

                case Game1.Difficulty.hard:
                    snakeUpdateDelay = 0.5; // in seconds
                    snakeVelocity = 0.5;
                    snakeAcceleration = 0.3;
                    break;
            }

            snakeVelocity = snakeUpdateDelay;

            snakeTimeSinceLastUpdate = TimeManager.CurrentTime;

            SnakeTail = new Entities.SnakeTail(ContentManagerName);
            SnakeTail.X = -2;
            SnakeTail.Y = 8;

            SnakeHead = new Entities.SnakeHead(ContentManagerName);
            SnakeHead.X = 2;
            SnakeHead.Y = 8;

            Entities.SnakeBody newSnakeBody = new Entities.SnakeBody(ContentManagerName);
            newSnakeBody.X = 0;
            newSnakeBody.Y = 8;
            SnakeBodyList.Add(newSnakeBody);
            CollisionFile.Visible = false;

            //DEBUG

            /*newSnakeBody = new Entities.SnakeBody(ContentManagerName);
            newSnakeBody.X = 0;
            newSnakeBody.Y = 6;
            newSnakeBody.Visible = true;
            newSnakeBody.Body.Visible = false;
            SnakeBodyList.Add(newSnakeBody);

            newSnakeBody = new Entities.SnakeBody(ContentManagerName);
            newSnakeBody.X = -2;
            newSnakeBody.Y = 6;
            newSnakeBody.Visible = true;
            newSnakeBody.Body.Visible = false;
            SnakeBodyList.Add(newSnakeBody);*/

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
            NewGame();
		}

		void CustomActivity(bool firstTimeCalled)
		{
            if (mPopups.Count == 0 && IsPaused)
            {
                UnpauseThisScreen();
                NewGame();
            }
            else if (!IsPaused)
            {
                UpdatePlayer();
                UpdateSnake();
                UpdateFood();
            }
		}

        void UpdateFood()
        {
            if (rand.Next(1) < FOOD_SPAWN_CHANCE && FoodList.Count < 1)
            {
                SpawnFood();
            }
        }

        void SpawnObstacle()
        {
            int attempts = 0;
            Entities.Obstacle newObstacle = new Entities.Obstacle(ContentManagerName);
            Boolean invalidLocation = true;

            while (invalidLocation)
            {
                attempts++;
                newObstacle.X = (float)rand.Next(4) * 2;
                newObstacle.Y = (float)rand.Next(7) * 2;
                if (rand.NextDouble() < .5)
                {
                    newObstacle.X *= -1;
                }
                if (rand.NextDouble() < .5)
                {
                    newObstacle.Y *= -1;
                }
                if ((!newObstacle.Body.CollideAgainstMove(Player.Body, 0, 1)) &&
                    (!newObstacle.Body.CollideAgainstMove(SnakeHead.Body, 0, 1)) &&
                    (!newObstacle.Body.CollideAgainstMove(SnakeTail.Body, 0, 1)) &&
                    (!newObstacle.Body.CollideAgainstMove(CollisionFile, 0, 1)))
                {
                    invalidLocation = false;
                    foreach (Entities.Obstacle obstacle in ObstacleList)
                    {
                        if (newObstacle.Body.CollideAgainstMove(obstacle.Body,0,1))
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.Food food in FoodList)
                    {
                        if (newObstacle.Body.CollideAgainst(food.Body) && attempts < MAX_ATTEMPTS)
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
                    {
                        if (newObstacle.Body.CollideAgainst(snakeBody.Body) && attempts < MAX_ATTEMPTS)
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.StoneSnakeBody stoneSnakeBody in StoneSnakeBodyList)
                    {
                        if (newObstacle.Body.CollideAgainst(stoneSnakeBody.Body) && attempts < MAX_ATTEMPTS)
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.StoneSnakeHead stoneSnakeHead in StoneSnakeHeadList)
                    {
                        if (newObstacle.Body.CollideAgainst(stoneSnakeHead.Body) && attempts < MAX_ATTEMPTS)
                        {
                            invalidLocation = true;
                            break;
                        }
                    }
                }
            }
            ObstacleList.Add(newObstacle);
        }

        void SpawnFood()
        {
            int attempts = 0;
            Entities.Food newFood = new Entities.Food(ContentManagerName);
            Boolean invalidLocation = true;

            while (invalidLocation)
            {
                attempts++;
                newFood.X = (float)rand.NextDouble() * 7;
                newFood.Y = (float)rand.NextDouble() * 12;
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
                        if (newFood.Body.CollideAgainst(obstacle.Body) && attempts < MAX_ATTEMPTS)
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.Food food in FoodList)
                    {
                        if (newFood.Body.CollideAgainst(food.Body))
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.SnakeBody snakeBody in SnakeBodyList)
                    {
                        if (newFood.Body.CollideAgainst(snakeBody.Body) && attempts < MAX_ATTEMPTS)
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.StoneSnakeBody stoneSnakeBody in StoneSnakeBodyList)
                    {
                        if (newFood.Body.CollideAgainst(stoneSnakeBody.Body) && attempts < MAX_ATTEMPTS)
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.StoneSnakeHead stoneSnakeHead in StoneSnakeHeadList)
                    {
                        if (newFood.Body.CollideAgainst(stoneSnakeHead.Body) && attempts < MAX_ATTEMPTS)
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.StoneSnakeTail stoneSnakeTail in StoneSnakeTailList)
                    {
                        if (newFood.Body.CollideAgainst(stoneSnakeTail.Body) && attempts < MAX_ATTEMPTS)
                        {
                            invalidLocation = true;
                            break;
                        }
                    }

                    foreach (Entities.Player ghostPlayer in GhostPlayerList)
                    {
                        if (newFood.Body.CollideAgainstMove(ghostPlayer.Body, 0, 1))
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
            if (!cursor.PrimaryDown)
            {
                upButton.EntireSceneCurrentChainName = "NoGlow";
                downButton.EntireSceneCurrentChainName = "NoGlow";
                rightButton.EntireSceneCurrentChainName = "NoGlow";
                leftButton.EntireSceneCurrentChainName = "NoGlow";
                Player.SetDirection(Entities.Player.Direction.Still);
            }
            else if (cursor.PrimaryDown && upButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Up);
                Player.EntireSceneCurrentChainName = "up";
                Player.RotationY = 0;
                Player.EntireSceneAnimate = true;
                upButton.EntireSceneCurrentChainName = "Glow";
                downButton.EntireSceneCurrentChainName = "NoGlow";
                rightButton.EntireSceneCurrentChainName = "NoGlow";
                leftButton.EntireSceneCurrentChainName = "NoGlow";
            }
            else if (cursor.PrimaryDown && downButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Down);
                Player.EntireSceneCurrentChainName = "down";
                Player.RotationY = 0;
                Player.EntireSceneAnimate = true;
                upButton.EntireSceneCurrentChainName = "NoGlow";
                downButton.EntireSceneCurrentChainName = "Glow";
                rightButton.EntireSceneCurrentChainName = "NoGlow";
                leftButton.EntireSceneCurrentChainName = "NoGlow";
            }
            else if (cursor.PrimaryDown && leftButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Left);
                Player.EntireSceneCurrentChainName = "side";
                Player.RotationY = (float)Math.PI;
                Player.EntireSceneAnimate = true;
                upButton.EntireSceneCurrentChainName = "NoGlow";
                downButton.EntireSceneCurrentChainName = "NoGlow";
                rightButton.EntireSceneCurrentChainName = "NoGlow";
                leftButton.EntireSceneCurrentChainName = "Glow";
            }
            else if (cursor.PrimaryDown && rightButton.HasCursorOver(cursor))
            {
                Player.SetDirection(Entities.Player.Direction.Right);
                Player.EntireSceneCurrentChainName = "side";
                Player.RotationY = 0;
                Player.EntireSceneAnimate = true;
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
                if (ghostTrip == false)
                {
                    Game1.hiss.Play();
                    Entities.Player newGhostPlayer = new Entities.Player(ContentManagerName);
                    newGhostPlayer.X = Player.X;
                    newGhostPlayer.Y = Player.Y;
                    newGhostPlayer.EntireSceneCurrentChainName = "dead";
                    GhostPlayerList.Add(newGhostPlayer);
                    ghostTrip = true;
                }
                Player.EntireSceneAnimate = false;
                Player.SetDirection(Entities.Player.Direction.Still);
            }

        }

        void KillSnake()
        {

            Entities.StoneSnakeHead newStoneSnakeHead = new Entities.StoneSnakeHead(ContentManagerName);
            //newStoneSnakeHead.Body.ScaleY = (float).7;
            newStoneSnakeHead.Body.RotationZ = SnakeHead.RotationZ;
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
                newStoneSnakeBody.Body.RotationZ = newStoneSnakeBody.RotationZ;
                if (newStoneSnakeBody.EntireSceneCurrentChainName == "Elbow")
                {
                    newStoneSnakeBody.Body.ScaleY = (float).6;
                    newStoneSnakeBody.Body.ScaleX = (float).6;
                }
                else if ( newStoneSnakeBody.RotationZ % Math.PI/2 == 0 )
                {
                    newStoneSnakeBody.Body.ScaleY = (float).5;
                } 
                else
                {
                    newStoneSnakeBody.Body.ScaleX = (float).5;
                }

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

            LoadPopup(typeof(WinScreen).FullName, true);
            //Put game win screen here ^
            PauseThisScreen();
            return;
        }

        void UpdateSnake()
        {
            // Don't start moving the snake until Player first starts moving
            if (!SnakeHead.canMove && (Player.direction != Entities.Player.Direction.Still))
            {
                SnakeHead.canMove = true;
            }

            if (SnakeHead.canMove && TimeManager.SecondsSince(snakeTimeSinceLastUpdate) > snakeVelocity)
            {
                // Figure out the direction of the snake head
                SnakeHead.ChangeDirectionByAngle(Player, SnakeBodyList, GhostPlayerList);

                if (SnakeHead.isDead)
                {
                    //Kill the snake
                    KillSnake();
                    return;
                }
                // If snake collides with tail, game over.
                if (SnakeHead.Body.CollideAgainst(SnakeTail.Body))
                {
                    KillSnake();
                    return;
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
                    SnakeBodyList.Last.Body.Visible = false;
                }

                
                // Snake head consumes food it collides with
                foreach (Entities.Food food in FoodList)
                {
                    if (SnakeHead.Body.CollideAgainstMove(food.Body, 1 , 0))
                    {
                        // Eat food, get faster
                        snakeVelocity = snakeVelocity - snakeVelocity * snakeAcceleration;

                        Game1.eat.Play();
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
                    LoadPopup(typeof(LoseScreen).FullName, true);
                    PauseThisScreen();
                }
                else if (gameOverDelay < 2)
                {
                    gameOverDelay += 1;
                }
            }
        }

		void CustomDestroy()
		{

            // Destroy the Snake
            SnakeHead.Destroy();
            for (int i = SnakeBodyList.Count - 1; i >= 0; i--)
            {
                Entities.SnakeBody snakeBody = SnakeBodyList[i];
                SnakeBodyList.RemoveAt(i);
                snakeBody.Destroy();
            }
            SnakeTail.Destroy();

            // Destroy the food
            for (int i = FoodList.Count - 1; i >= 0; i--)
            {
                Entities.Food food = FoodList[i];
                FoodList.RemoveAt(i);
                food.Destroy();
            }

            // Destroy the obstacles
            for (int i = ObstacleList.Count - 1; i >= 0; i--)
            {
                Entities.Obstacle obs = ObstacleList[i];
                ObstacleList.RemoveAt(i);
                obs.Destroy();
            }

            // Destroy Old Ghosts
            /*foreach (Entities.Player ghostPlayer in GhostPlayerList)
            {
                if (newFood.Body.CollideAgainstMove(ghostPlayer.Body, 0, 1))
                {
                    invalidLocation = true;
                    break;
                }
            }*/
            if ( GhostPlayerList.Count > 3 )
            {
                Entities.Player ghost = GhostPlayerList[0];
                GhostPlayerList.RemoveAt(0);
                ghost.Destroy();
            }

            // Destroy the player
            Player.Destroy();
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {

        }

	}
}
