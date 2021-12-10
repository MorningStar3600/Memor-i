using System;
using System.Collections.Generic;

namespace Projet1
{
    class AnimCard
    {
        private int _width;
        private int _height;
        char[][,] _screenBuffer;
        short[][,] _screenBufferColor;
        short[][,]_screenBufferBColor;
        FastConsole.CharInfo[][] buffer;
        double animationIndex = 0;


        public AnimCard(List<List<ColoredChar[]>> values, int width, int height, ProgressBar pb = null)
        {
            _screenBuffer = new char[values.Count][,];
            _screenBufferColor = new short[values.Count][,];
            _screenBufferBColor = new short[values.Count][,];
            buffer = new FastConsole.CharInfo[values.Count][];
            _width = width;
            _height = height;
            for (int i = 0; i < values.Count; i++)
            {
                Convert(i, Card.Smooth(Card.Resize(values[i], _width, _height)));
                Preload(i);
                if (pb != null && i % 2 == 0)
                {
                    pb.Update();
                }
            }
        }
        
        private void Convert(int index, List<ColoredChar[]> value)
        {
            int length0 = _height;
            int length1 = _width;
            _screenBuffer[index] = new char[length0, length1];
            _screenBufferColor[index] = new short[length0, length1];
            _screenBufferBColor[index] = new short[length0, length1];
            for (int i = 0; i < length0; i++)
            {
                for (int j = 0; j < length1; j++)
                {
                    _screenBuffer[index][i, j] = value[i][j].c;
                    _screenBufferColor[index][i, j] = (short)value[i][j].color;
                    _screenBufferBColor[index][i, j] = (short)value[i][j].bColor;
                }
            }
        }

        private void Preload(int cardIndex)
        {
            int index = 0;
            buffer[cardIndex] = new FastConsole.CharInfo[_height * _width];

            
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    buffer[cardIndex][i*_width + j].Char.UnicodeChar = _screenBuffer[cardIndex][i, j];
                    buffer[cardIndex][i*_width + j].Attributes = (short) (_screenBufferColor[cardIndex][i,j] | (_screenBufferBColor[cardIndex][i,j]<<4));
                }
            }
        }

        public (char[,], short[,], short[,]) GetValue()
        {
            animationIndex += 1;
            if (animationIndex >= _screenBuffer.Length) animationIndex = 0;
            return (_screenBuffer[(int)animationIndex], _screenBufferColor[(int)animationIndex], _screenBufferBColor[(int)animationIndex]);
        }
        
        public FastConsole.CharInfo[] GetBuffer()
        {
            animationIndex += 1;
            if (animationIndex >= buffer.Length) animationIndex = 0;
            return buffer[(int)animationIndex];
        }

    }
}