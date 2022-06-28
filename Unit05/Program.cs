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
            string player1 = "One";
            string player2 = "Two";
            Score score1 = new Score(player1);
            Score score2 = new Score(player2);
            cast.AddActor("score", score1);
            cast.AddActor("score", score2);

            //create two snakes at different positions and colors
            Snake snake1 = new Snake(Constants.MAX_X/4, Constants.MAX_Y/2, Constants.RED);
            Snake snake2 = new Snake(650, Constants.MAX_Y/2, Constants.GREEN);
            cast.AddActor("snake", snake1);
            cast.AddActor("snake", snake2);

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