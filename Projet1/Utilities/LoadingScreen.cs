using System.Collections.Generic;

namespace Projet1
{
    public static class LoadingScreen
    {
        private static AnimCard _animCard;
        
        public static void PreLoad(ProgressBar pb, int width, int height)
        {
            //ConsoleManager.SetCurrentFont("Consolas",5);
            List<List<ColoredChar[]>> anim = new List<List<ColoredChar[]>>();
            CardLoader.Load("0", anim, pb);
            _animCard = new AnimCard(anim, width, height, pb);
        }

        public static void Start()
        {
            ConsoleManager.SetCurrentFont("Consolas",5);
            Draws.toDraw.Add(_animCard);
        }
        
        public static void Stop()
        {
            Draws.toDraw.Clear();
            Draws.Clear();
            ConsoleManager.SetCurrentFont("Consolas", 20);
        }
    }
}