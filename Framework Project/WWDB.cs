using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Project_A
{
    public class WWDB
    {
        string pg_sign_data = "Host=localhost;Username=postgres;Password=admin123;Database=GuitarLessons";
        private NpgsqlConnection con;

        public WWDB()
        {
            this.con = new NpgsqlConnection(pg_sign_data);
            this.con.Open();
        }
        public string TestConnection()
        {
            string com = "SELECT version()";
            var cmd = new NpgsqlCommand(com, this.con);

            var version = cmd.ExecuteScalar().ToString();
            return $"PostgreSQL version: {version}";
        }
        public Chord GetChord(string ChordName)
        {
            Dictionary<int, int?> ChordPosition = new Dictionary<int, int?> { [1] = null, [2] = null, [3] = null, [4] = null, [5] = null, [6] = null };
            int? bare = null;

            string com = "select * from chords where chordname = '" + ChordName + "'";
            var cmd = new NpgsqlCommand(com, this.con);
            NpgsqlDataReader Reader = cmd.ExecuteReader();
            while (Reader.Read())
            {
                for (int i = 0; i < 6; i++)
                {
                    try
                    {
                        ChordPosition[i + 1] = Reader.GetInt32(i);
                    }
                    catch
                    {
                        ChordPosition[i + 1] = null;
                    }
                }
                try
                {
                    bare = Reader.GetInt32(6);
                    return new Chord(ChordName, ChordPosition, (int)bare);
                }
                catch
                {
                    return new Chord(ChordName, ChordPosition);
                }                
            }
            return new Chord();
        }
    }
}
