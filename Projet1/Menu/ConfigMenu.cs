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
            switch (Config.AnimationSpeed)
            {
                case 0.05:
                    Program.cm.GetCards()[7].animationIndex = 1;
                    break;
                
                case 0.1:
                    Program.cm.GetCards()[7].animationIndex = 2;
                    break;
            }
            
            Draws.toDraw.Add(new UpdatedText("Vitesse d'animation", (width/2) - 9, height/3-2));
        }

        private static void EventHandler(CardManager cm, int cardId, int eventId, char key, int keyCode)
        {
            if (eventId == 1)
            {
                if (cardId == 7)
                {
                    cm.GetCards()[7].NextAnimation();
                    UpdateSpeed(cm);
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

        private static void UpdateSpeed(CardManager cm)
        {
            switch (cm.GetCards()[7].animationIndex)
            {
                case 0:
                    Config.AnimationSpeed = 0.01;
                    break;
                
                case 1:
                    Config.AnimationSpeed = 0.05;
                    break;
                
                case 2: 
                    Config.AnimationSpeed = 0.1;
                    break;
                
            }
            
            for (int i = 0; i < Program.onHoldCm.GetCards().Count; i++){
                Program.onHoldCm.GetCards()[i].animationSpeed = Config.AnimationSpeed;    
            }
        }
    }
}