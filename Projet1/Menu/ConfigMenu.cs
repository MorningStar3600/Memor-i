using System;

namespace Projet1.Menu
{
    public static class ConfigMenu
    {
        public static void Start(int width, int height)
        {
            string[] cards = {"Menu/return","default","default","default","default","default","default","Menu/speed","default","default","default","default","default","default","default"};
            string[] back = {"default","default","default","default","default","default","default","default","default","default","default","default","default","default","default"};
            Program.cm = new CardManager(cards, back, 5, EventHandler, width, height, 0, true);
            Program.cm.Draw();
        }

        private static void EventHandler(CardManager cm, int cardId, int eventId, char key, int keyCode)
        {
            if (eventId == 1)
            {
                if (cardId == 7)
                {
                    cm.GetCards()[7].NextAnimation();
                }

                if (cardId == 0)
                {
                    Draws.toDraw.Clear();
                    Draws.Clear();
                    Program.isInMenu = true;
                    EscMenu.Start(Console.WindowWidth, Console.WindowHeight);
                }
            }
        }
    }
}