using System.Collections.Generic;
using System.Threading;

namespace Projet1
{
    public class Draw
    {
        private List<Map> toDraw = new List<Map>();
        private readonly Thread _thread;

        Draw()
        {
            _thread = new Thread(ReDraw);
        }
        public void run()
        {
            _thread.Start();
        }

        public void stop()
        {
            _thread.Abort();
        }

        public void AddToDraw(Map m)
        {
            toDraw.Add(m);
        }

        private void ReDraw()
        {
            while (true)
            {
                if (toDraw.Count > 0)
                {
                    for (int i = 0; i < toDraw.Count; i++)
                    {
                        toDraw[i].Draw();
                    }
                }

                Thread.Sleep(20);
            }
        }
    }
}