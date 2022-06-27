using System;
using Unit05.Game.Casting;
using Unit05.Game.Directing;
using Unit05.Game.Scripting;
using Unit05.Game.Services;
using Unit05.Game;


namespace Unit05
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();
            cast.AddActor("food", new Food());

            //add the scores to the cast
            Score score1 = new Score();
            Score score2 = new Score();
            cast.AddActor("score", score1);
            cast.AddActor("score", score2);

            //create two snakes
            Snake snake1 = new Snake();
            Snake snake2 = new Snake();
            cast.AddActor("snake", snake1);
            cast.AddActor("snake", snake2);

            //set the snakes' colors, points, and velocity
            Point velocity = new Point(0,-1);
            snake1.SetColor(Constants.RED);
            Point position1 = new Point(Constants.MAX_X/4, Constants.MAX_Y/2);
            snake1.SetPosition(position1);
            snake1.SetVelocity(velocity);

            snake2.SetColor(Constants.GREEN);
            Point position2 = new Point(Constants.MAX_X/2, Constants.MAX_Y/2);
            snake2.SetPosition(position2);
            snake2.SetVelocity(velocity);

            // create the services
            KeyboardService keyboardService = new KeyboardService();
            VideoService videoService = new VideoService(false);
           
            // create the script
            Script script = new Script();
            script.AddAction("input", new ControlActorsAction(keyboardService));
            script.AddAction("update", new MoveActorsAction());
            script.AddAction("update", new HandleCollisionsAction());
            script.AddAction("output", new DrawActorsAction(videoService));

            // start the game
            Director director = new Director(videoService);
            director.StartGame(cast, script);
        }
    }
}