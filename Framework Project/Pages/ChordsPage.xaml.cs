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
    /// Логика взаимодействия для ChordsPage.xaml
    /// </summary>
    public partial class ChordsPage : Page
    {
        List<Chord> ActiveChords;
        public ChordsPage()
        {
            InitializeComponent();
            PullChords();
        }
        private void PullChords()
        {
            WWDB db = new WWDB();
            ActiveChords = db.GetChordsList();
            for(int i = 0; i < ActiveChords.Count; i++)
            {
                Listing.Items.Add(ActiveChords[i]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string check = (sender as Button).Content as string;
            for (int i = 0; i < ActiveChords.Count; i++)
            {
                if(check == ActiveChords[i].Name)
                {
                    DataContext = new VisualChord(ActiveChords[i]);
                    break;
                }
            }
        }
    }
    
}
