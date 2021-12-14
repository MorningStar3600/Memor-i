using System;

namespace Projet1.Menu
{
    public static class EscMenu
    {
        public static void Start(int width, int height)
        {
            string[] cards = {"default","default","default","default", "default","default","Menu/settings","default","default","default","default","default","default","default","default","default","Menu/return","default","Menu/main","default","default","default",};
            string[] back = {"default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default"};
            Program.cm = new CardManager(cards, back, 7, EventHandler, width, height, 0, true);
            Program.cm.Draw();
        }

        private static void EventHandler(CardManager cm, int cardId, int eventId, char key, int keyCode)
        {
            if (eventId == 1)
            {
                if (cardId == 16)
                {
                    Draws.toDraw.Clear();
                    Draws.Clear();
                    Program.cm = Program.onHoldCm;
                    Program.isInMenu = false;
                    Program.cm.Draw();
                }

                if (cardId == 18)
                {
                    Program.isInMenu = false;
                    MainMenu.Start(Console.WindowWidth, Console.WindowHeight);
                }

                if (cardId == 6)
                {
                    Draws.toDraw.Clear();
                    Draws.Clear();
                    Program.isInMenu = false;
                    ConfigMenu.Start(Console.WindowWidth, Console.WindowHeight);
                }
            }
        }
    }
}