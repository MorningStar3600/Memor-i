using System;
using System.Collections;

namespace Projet1.Menu
{
    public static class WinMenu
    {
        public static void Start(int width, int height, Game game)
        {
            
            string[] cards = {"default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","Menu/main"};
            string[] back = {"default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default","default"};
            Program.LoadCardManager(cards, back, 5, EventHandler, width, height, 0, true);
            
            game.SavePlayersToFile();
            
            ArrayList list = new ArrayList();
            for (int i = 0; i < game.GetNumbPlayers(); i++)
            {
                string value = game._players[i].GetName() + ":" + game._players[i].GetScore();
                list.Add(new UpdatedText(value, 10, i*3+3));
            }
            
            for (int i = 0; i < list.Count; i++)
            {
                Draws.toDraw.Add(list[i] as UpdatedText);
            }
            
            game.EndGame();

        }

        private static void EventHandler(CardManager cm, int cardId, int eventId, char key, int keyCode)
        {
            
        }
    }
}