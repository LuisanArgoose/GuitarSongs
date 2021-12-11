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
        WWDB db = new WWDB();
        List<Dictionary<string, string>> songslist;
        public SongsPage()
        {
            InitializeComponent();
            for(int i = 0; i < 5; i++)
            {
                Chord temp = new Chord();
                Chords.Items.Add(new VisualChord(temp));
            }

            songslist = db.GetSongsList();
            for (int i = 0; i < songslist.Count; i++)
            {
                string resstring = string.Format("Название песни: {0}{1}{0}\nИсполнитель: {0}{2}{0}", '"', songslist[i]["name"], songslist[i]["artist"]);
                Listing.Items.Add(resstring);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Listing.Items.Clear();
            Tool = new ParsSong(Search.Text);
            for (int i = 0; i < Tool.Songs.Count; i++)
            {
                Listing.Items.Add(Tool.Songs[i]);
            }
        }
        private void Open_Song(object sender, RoutedEventArgs e)
        {
            string check = (sender as Button).Content as string;
            for (int i = 0; i < Tool.Songs.Count; i++)
            {
                if (check == Tool.Songs[i].Full_info)
                {
                    SongInfo.DataContext = Tool.Songs[i];
                    SongText.DataContext = Tool.Songs[i];
                    break;
                }
            }
        }
    }
}
