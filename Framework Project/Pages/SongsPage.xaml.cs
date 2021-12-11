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
        WWDB db = new WWDB();
        List<Dictionary<string, string>> songslist;
        public SongsPage()
        {
            InitializeComponent();

            songslist = db.GetSongsList();
            for (int i = 0; i < songslist.Count; i++)
            {
                string resstring = string.Format("Название песни: {0}{1}{0}\nИсполнитель: {0}{2}{0}", '"', songslist[i]["name"], songslist[i]["artist"]);
                Listing.Items.Add(resstring);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //(sender as Button).Parent;
            //var thisbut = this;
            //var itemid = (sender as ListBox).SelectedItem;
            //DataContext = itemid;
            
        }
    }
}
