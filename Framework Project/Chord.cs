using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A
{
    class Chord
    {
        private string name;
        private Dictionary<int, int> chordsPosition;
        private int bare;
        private int firstFret;
        private int length;

        // Constructor
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

            this.length = maxFret - this.firstFret;
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
            get { return this.Length; }
        }

        //Functions

        public bool[,] construct()
        {
            bool[,] position = new bool[6, this.length];



            return position;
        }
    }
    
}
