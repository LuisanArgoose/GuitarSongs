using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_A.Pages
{
    /// <summary>
    /// Логика взаимодействия для SongsPage.xaml
    /// </summary>
    public partial class SongsPage : Page
    {
        ParsSong Tool;
        bool is_scrolling;
        WWDB db = new WWDB();

        private void output_db_songs()
        {
            Listing.Items.Clear();
            Tool = new ParsSong(Search.Text);
            for (int i = 0; i < Tool.Songs.Count; i++)
            {
                Listing.Items.Add(Tool.Songs[i]);
            }
        }

        public SongsPage()
        {
            InitializeComponent();
            output_db_songs();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Search.Text == "")
                {
                    output_db_songs();
                }
                else
                {
                    Listing.Items.Clear();
                    Tool = new ParsSong(Search.Text);
                    for (int i = 0; i < Tool.Songs.Count; i++)
                    {
                        Listing.Items.Add(Tool.Songs[i]);
                    }
                }
            }
            catch
            {
                output_db_songs();
            }
            
        }
        private async Task ScrollDown()
        {
            for (var i = SongText.ContentVerticalOffset; i < SongText.ScrollableHeight; i++)
            {
                if (is_scrolling)
                {
                    SongText.ScrollToVerticalOffset(i);
                    await Task.Delay(500-(int.Parse(SpeedBox.Text)-1)*50);
                }
                else
                {
                    break;
                }
            }
        }

        private async void Scroll_Click(object sender, RoutedEventArgs e)
        {
            if (!is_scrolling)
            {
                is_scrolling = true;
                await ScrollDown();
            }
        }

        private void Unscroll_Click(object sender, RoutedEventArgs e)
        {
            is_scrolling = false;
        }

        private void Open_Song(object sender, RoutedEventArgs e)
        {
            is_scrolling = false;
            Chords.Items.Clear();
            string check = (sender as Button).Content as string;
            for (int i = 0; i < Tool.Songs.Count; i++)
            {
                if (check == Tool.Songs[i].Full_info)
                {
                    Tool.Songs[i].Get_Song();
                    if (Tool.Songs[i].Chords != null)
                    {
                        List<Chord> ActiveChords = db.GetChordsList();
                        List<string> NameChords = new List<string>();
                        for (int j = 0; j < ActiveChords.Count; j++)
                        {
                            NameChords.Add(ActiveChords[j].Name);
                        }
                        for (int j = 0; j < Tool.Songs[i].Chords.Count; j++)
                        {
                            if (NameChords.Contains(Tool.Songs[i].Chords[j]))
                            {
                                for (int f = 0; f < ActiveChords.Count; f++)
                                {
                                    if (ActiveChords[f].Name == Tool.Songs[i].Chords[j])
                                    {
                                        Chords.Items.Add(new VisualChord(ActiveChords[f]));
                                    }
                                }
                            }
                        }
                    }
                    SongInfo.DataContext = Tool.Songs[i];
                    SongText.DataContext = Tool.Songs[i];
                    break;
                }
            }
        }

        private void Download(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)e.Source;
            Song song = (Song)menuItem.DataContext;
            if (song.Chords == null)
            {
                song.Get_Song();
            }
            db.PushSongToDB(song);
        }

        private void Plus_click(object sender, RoutedEventArgs e)
        {
            SpeedBox.Text = (int.Parse(SpeedBox.Text) + 1).ToString();
        }
        private void Min_click(object sender, RoutedEventArgs e)
        {
            SpeedBox.Text = (int.Parse(SpeedBox.Text) - 1).ToString();
        }

        private void SpeedBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach(char Char in SpeedBox.Text)
            {
                if (!char.IsDigit(Char) && Char != '-')
                {
                    SpeedBox.Text = "5";
                    break;
                }
            }

            if (SpeedBox.Text != null){
                if (int.Parse(SpeedBox.Text) > 10)
                {
                    SpeedBox.Text = "10";
                }
                else if (int.Parse(SpeedBox.Text) < 0)
                {
                    SpeedBox.Text = "0";
                }
            }
        }
    }
    public class VisualChord
    {
        public VisualChord(Chord Em)
        {
            ChordName = Em.Name;
            int temp = Em.FirstFret;
            bool[,] head = Em.construct();
            Position1 = new Visibility[4];
            Position2 = new Visibility[4];
            Position3 = new Visibility[4];
            Position4 = new Visibility[4];
            Position5 = new Visibility[4];
            Position6 = new Visibility[4];
            FreatCount = new int[4];
            for (int i = 0; i < 4; i++)
            {
                FreatCount[i] = i + temp;
                Position1[i] = Visibility.Collapsed;
                Position2[i] = Visibility.Collapsed;
                Position3[i] = Visibility.Collapsed;
                Position4[i] = Visibility.Collapsed;
                Position5[i] = Visibility.Collapsed;
                Position6[i] = Visibility.Collapsed;

                if (head[0, i])
                {
                    Position1[i] = Visibility.Visible;
                }
                if (head[1, i])
                {
                    Position2[i] = Visibility.Visible;
                }
                if (head[2, i])
                {
                    Position3[i] = Visibility.Visible;
                }
                if (head[3, i])
                {
                    Position4[i] = Visibility.Visible;
                }
                if (head[4, i])
                {
                    Position5[i] = Visibility.Visible;
                }
                if (head[5, i])
                {
                    Position6[i] = Visibility.Visible;
                }
            }
        }
        public string ChordName { get; set; }
        public int[] FreatCount { get; set; }
        public Visibility[] Position1 { get; set; }
        public Visibility[] Position2 { get; set; }
        public Visibility[] Position3 { get; set; }
        public Visibility[] Position4 { get; set; }
        public Visibility[] Position5 { get; set; }
        public Visibility[] Position6 { get; set; }
    }
}
