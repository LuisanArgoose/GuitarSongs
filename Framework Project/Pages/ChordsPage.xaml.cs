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
        public ChordsPage()
        {

            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Chord Em = new Chord();
            VisualChord temp = new VisualChord(Em);
            DataContext = temp;
        }
    }
    public class VisualChord
    {
        public VisualChord(Chord Em)
        {
            int temp = Em.FirstFret;
             
            Position1 = new Visibility[4];
            Position2 = new Visibility[4];
            Position3 = new Visibility[4];
            Position4 = new Visibility[4];
            Position5 = new Visibility[4];
            Position6 = new Visibility[4];
            FreatCount = new int[5];
            for (int i = 0; i < 5; i++)
            {
                FreatCount[i] = i + temp;

            }
            for (int i = 0; i < 4; i++)
            {
                Position1[i] = Visibility.Collapsed;
                Position2[i] = Visibility.Collapsed;
                Position3[i] = Visibility.Collapsed;
                Position4[i] = Visibility.Collapsed;
                Position5[i] = Visibility.Collapsed;
                Position6[i] = Visibility.Collapsed;

            }
        }
        public int[] FreatCount { get; set; }
        public Visibility[] Position1 { get; set; }
        public Visibility[] Position2 { get; set; }
        public Visibility[] Position3 { get; set; }
        public Visibility[] Position4 { get; set; }
        public Visibility[] Position5 { get; set; }
        public Visibility[] Position6 { get; set; }

        


    }
}
