using System.Collections.Generic;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An output action that draws all the actors.</para>
    /// <para>The responsibility of DrawActorsAction is to draw each of the actors.</para>
    /// </summary>
    public class DrawActorsAction : Action
    {
        private VideoService videoService;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public DrawActorsAction(VideoService videoService)
        {
            this.videoService = videoService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Snake snake1 = (Snake)cast.GetFirstActor("snake");
            Snake snake2 = (Snake)cast.GetSecondActor("snake");
            Actor score1 = cast.GetFirstActor("score");
            Actor score2 = cast.GetSecondActor("score");

            Point position2 = new Point(825, 0);
            score2.SetPosition(position2);

            List<Actor> segments1 = snake1.GetSegments();
            List<Actor> segments2 = snake2.GetSegments();

            Actor food = cast.GetFirstActor("food");
            List<Actor> messages = cast.GetActors("messages");
            
            videoService.ClearBuffer();
            videoService.DrawActors(segments1);
            videoService.DrawActors(segments2);
            videoService.DrawActor(score1);
            videoService.DrawActor(score2);
            videoService.DrawActor(food);
            videoService.DrawActors(messages);
            videoService.FlushBuffer();
        }
    }
}