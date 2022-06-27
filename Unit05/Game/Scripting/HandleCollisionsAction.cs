using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool isGameOver = false;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (isGameOver == false)
            {
                HandleFoodCollisions(cast);
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleFoodCollisions(Cast cast)
        {
            Snake snake1 = (Snake)cast.GetFirstActor("snake");
            Snake snake2 = (Snake)cast.GetSecondActor("snake");

            Score score1 = (Score)cast.GetFirstActor("score");
            Score score2 = (Score)cast.GetSecondActor("score");

            Food food = (Food)cast.GetFirstActor("food");
            
            if (snake1.GetHead().GetPosition().Equals(food.GetPosition()))
            {
                int points = food.GetPoints();
                snake1.GrowTail(points);
                score1.AddPoints(points);
                food.Reset();
            }
            else if (snake2.GetHead().GetPosition().Equals(food.GetPosition()))
            {
                int points = food.GetPoints();
                snake2.GrowTail(points);
                score2.AddPoints(points);
                food.Reset();
            }
        }

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Snake snake1 = (Snake)cast.GetFirstActor("snake");
            Snake snake2 = (Snake)cast.GetSecondActor("snake");
            
            Actor head1 = snake1.GetHead();
            Actor head2 = snake2.GetHead();

            List<Actor> body1 = snake1.GetBody();
            List<Actor> body2 = snake2.GetBody();

            foreach (Actor segment1 in body1)
            {
                foreach (Actor segment2 in body2)
                    if (segment1.GetPosition().Equals(segment2.GetPosition()))
                    {
                        isGameOver = true;
                    }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (isGameOver == true)
            {
                Snake snake1 = (Snake)cast.GetFirstActor("snake");
                Snake snake2 = (Snake)cast.GetSecondActor("snake");

                List<Actor> segments1 = snake1.GetSegments();
                List<Actor> segments2 = snake2.GetSegments();
                Food food = (Food)cast.GetFirstActor("food");

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over!");
                message.SetPosition(position);
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segments1)
                {
                    segment.SetColor(Constants.WHITE);
                }
                foreach (Actor segment in segments2)
                {
                    segment.SetColor(Constants.WHITE);
                }
                food.SetColor(Constants.WHITE);
            }
        }

    }
}