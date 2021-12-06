﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A
{
    public class Chord
    {
        private string name;
        private Dictionary<int, int?> chordsPosition;
        private int? bare;
        private int firstFret;
        private int length;

        // Constructor
        public Chord()
        {
            this.name = "Em";
            this.chordsPosition = new Dictionary<int, int?>
            {
                [1] = 0,
                [2] = 0,
                [3] = 0,
                [4] = 2,
                [5] = 2,
                [6] = 0
            };
            bare = null;
            firstFret = 1;
            length = 2;
        }
        public Chord(string name, Dictionary<int, int?> chordsPosition) : this(name, chordsPosition, null)
        {
            if(firstFret == 0)
            {
                firstFret = 1;
                length -= 1;
            }
        }
        public Chord(string name, Dictionary<int, int?> chordsPosition, int? bare) 
        {
            this.name = name;
            this.chordsPosition = chordsPosition;
            this.bare = bare;

            firstFret = (int)chordsPosition[1];
            foreach (int? fret in this.chordsPosition.Values)
            {
                if (fret < firstFret && fret != null)
                {
                    firstFret = (int)fret;
                }
            }

            int maxFret = (int)chordsPosition[1];
            foreach (int? fret in this.chordsPosition.Values)
            {
                if (fret > maxFret && fret != null)
                {
                    maxFret = (int)fret;
                }
            }

            length = maxFret - firstFret + 1;
        }

        // Getters and setters

        public string Name
        {
            get { return name; }
        }

        public int FirstFret
        {
            get { return firstFret; }
        }

        public int Length
        {
            get { return length; }
        }

        //Functions

        public bool[,] construct()
        {
            bool[,] position = new bool[6, length];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    position[i, j] = false;
                }
            }

            for (int i = 1; i <= 6; i++)
            {

                if (chordsPosition[i] != 0 && chordsPosition[i] != null)
                {
                    position[i - 1, (int)chordsPosition[i] - firstFret] = true;
                }
            }

            if (bare != null)
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
