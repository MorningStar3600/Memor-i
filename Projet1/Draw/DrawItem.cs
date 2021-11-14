using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet1
{
    class DrawItem
    {
        List<ScreenCard> value = new List<ScreenCard>();
        List<ScreenCard> drawn { get; } = new List<ScreenCard>();

        DrawItem old;
        public DrawItem(List<ScreenCard> value, DrawItem old = null)
        {
            this.value = value;
            if (old != null)
            {
                this.old = old;
            }
        }

        public void Draw()
        {
            if (this.value.Count > 0)
            {
                if (old != null && this.drawn.Count == 0)
                {
                    old.drawn[old.drawn.Count - 1].Clear();
                    this.value[0].Draw();
                }else if (this.drawn.Count > 0)
                {
                    this.drawn[this.drawn.Count - 1].Clear();
                    this.value[0].Draw();
                }
                else
                {
                    this.value[0].Draw();
                }
                this.drawn.Add(value[0]);
                this.value.RemoveAt(0);
            }

        }
    }
}
