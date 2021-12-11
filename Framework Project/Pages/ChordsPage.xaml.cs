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
