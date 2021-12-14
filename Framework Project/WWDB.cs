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
        private string pg_sign_data = "Host=localhost;Username=postgres;Password=admin123;Database=GuitarLessons";
        private NpgsqlConnection con;

        public WWDB()
        {
            con = new NpgsqlConnection(pg_sign_data);
            con.Open();
        }
        public string TestConnection()
        {
            string com = "SELECT version()";
            var cmd = new NpgsqlCommand(com, con);

            var version = cmd.ExecuteScalar().ToString();
            return $"PostgreSQL version: {version}";
        }
        public Chord GetChord(string ChordName)
        {
            Dictionary<int, int?> ChordPosition = new Dictionary<int, int?> { [1] = null, [2] = null, [3] = null, [4] = null, [5] = null, [6] = null };
            int? bare = null;

            string com = "select * from chords where chordname = '" + ChordName + "'";
            var cmd = new NpgsqlCommand(com, con);
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
                    Reader.Close();
                    return new Chord(ChordName, ChordPosition, (int)bare);
                }
                catch
                {
                    Reader.Close();
                    return new Chord(ChordName, ChordPosition);
                }
            }
            Reader.Close();
            return new Chord();
        }
        public List<Chord> GetChordsList()
        {
            List<string> chordsnames = new List<string>();
            string com = "select chordname from chords";
            var cmd = new NpgsqlCommand(com, con);
            NpgsqlDataReader Reader = cmd.ExecuteReader();
            while (Reader.Read())
            {
                chordsnames.Add(Reader.GetString(0));
            }
            Reader.Close();

            List<Chord> returnList = new List<Chord>();
            foreach (string chordname in chordsnames)
            {
                returnList.Add(GetChord(chordname));
            }

            return returnList;
        }

        public Song GetSongDB(string SongName)
        {
            Song song = new Song();
            int song_id = 0;

            string com1 = $"Select id, artist, songdata, url from songs where name = '{SongName}'";

            var cmd1 = new NpgsqlCommand(com1, con);
            NpgsqlDataReader Reader1 = cmd1.ExecuteReader();
            while (Reader1.Read())
            {
                song_id = Reader1.GetInt16(0);
                song.Name = SongName;
                song.Author = Reader1.GetString(1);
                song.Text = Reader1.GetString(2);
                song.Url = Reader1.GetString(3);
                song.Full_info = song.Name + "\n" + song.Author;
            }
            Reader1.Close();

            string com2 = $"Select chord from songs-chords where song = {song_id}";

            var cmd2 = new NpgsqlCommand(com2, con);
            NpgsqlDataReader Reader2 = cmd2.ExecuteReader();
            while (Reader2.Read())
            {
                song.Chords.Add(Reader2.GetString(0));
            }
            Reader2.Close();

            return song;
        }

        public List<Dictionary<string, string>> GetSongsList()
        {
            List<Dictionary<string, string>> songslist = new List<Dictionary<string, string>>();

            string com = "select name, artist from songs";
            var cmd = new NpgsqlCommand(com, con);
            NpgsqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                songslist.Add( new Dictionary<string, string> { ["name"] = Reader.GetString(0), ["artist"] = Reader.GetString(1) });
            }

            return songslist;
        }

        public void PushSongToDB(Song song)
        {
            string com1 = $"Insert into songs (name, artist, songdata, url) values ('{song.Name}', '{song.Author}', '{song.Text}', '{song.Url}') returning id";
            var cmd1 = new NpgsqlCommand(com1, con);
            int song_id = (int)cmd1.ExecuteScalar();

            string com2 = $"Insert into " + '"' + "songs-chords" + '"' +" values ";
            foreach (string chord in song.Chords)
            {
                com2 += $"({song_id}, '{chord}'), ";
            }
            char[] temp_com2 = com2.ToCharArray();
            temp_com2[temp_com2.Length - 2] = ';';
            com2 = new string(temp_com2);

            var cmd2 = new NpgsqlCommand(com2, con);
            cmd2.ExecuteNonQuery();
        }
    }
}
