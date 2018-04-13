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

namespace Parejas_de_Cartas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Star(object sender, RoutedEventArgs e)
        {
            Juego j = new Juego(18, Juego.Tematica.Star);
            j.Show();
            this.Hide();
        }

        private void Button_Click_MHA(object sender, RoutedEventArgs e)
        {
            Juego j = new Juego(18, Juego.Tematica.MyHeroAcademia);
            j.Show();
            this.Hide();
        }

        private void Button_Click_SU(object sender, RoutedEventArgs e)
        {
            Juego j = new Juego(18, Juego.Tematica.StevenUniverse);
            j.Show();
            this.Hide();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Credits(object sender, RoutedEventArgs e)
        {
            AcercaDe acerca = new AcercaDe();
            acerca.ShowDialog();
        }
    }
}
