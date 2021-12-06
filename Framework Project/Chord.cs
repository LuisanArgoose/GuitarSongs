using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A
{
    public class Chord
    {
        private string name;
        private Dictionary<int, int> chordsPosition;
        private int? bare;
        private int firstFret;
        private int length;

        // Constructor
        public Chord(string name, Dictionary<int, int> chordsPosition)
        {
            this.name = name;
            this.chordsPosition = chordsPosition;

            this.firstFret = chordsPosition[1];
            foreach (int fret in this.chordsPosition.Values)
            {
                if ((fret < this.firstFret) && (fret != 0))
                {
                    this.firstFret = fret;
                }
            }
            int maxFret = this.chordsPosition[1];
            foreach (int fret in this.chordsPosition.Values)
            {
                if (fret > maxFret)
                {
                    maxFret = fret;
                }
            }

            if(this.firstFret == 0)
            {
                this.firstFret = 1;
            }

            this.length = maxFret - this.firstFret + 1;
        }
        public Chord(string name, Dictionary<int, int> chordsPosition, int bare)
        {
            this.name = name;
            this.chordsPosition = chordsPosition;
            this.bare = bare;

            this.firstFret = this.chordsPosition[1];
            foreach (int fret in this.chordsPosition.Values)
            {
                if (fret < this.firstFret)
                {
                    this.firstFret = fret;
                }
            }
            int maxFret = this.chordsPosition[1];
            foreach (int fret in this.chordsPosition.Values)
            {
                if (fret > maxFret)
                {
                    maxFret = fret;
                }
            }

            this.length = maxFret - this.firstFret + 1;
        }

        // Getters and setters

        public string Name
        {
            get { return this.name; }
        }

        public int FirstFret
        {
            get { return this.firstFret; }
        }

        public int Length
        {
            get { return this.length; }
        }

        //Functions

        public bool[,] construct()
        {
            bool[,] position = new bool[6, this.length];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < this.length; j++)
                {
                    position[i, j] = false;
                }
            }

            for (int i = 1; i <= 6; i++)
            {

                if (this.chordsPosition[i] != 0)
                {
                    position[i - 1, (this.chordsPosition[i] - this.firstFret)] = true;
                }
            }

            if (this.bare != null)
            {
                for (int i = 0; i < bare; i++)
                {
                    position[i, 0] = true;
                }
            }

            return position;
        }
    }
    
}
