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
// using System.Windows.Shapes;
using System.IO;
using System.Reflection;

namespace Parejas_de_Cartas
{
    /// <summary>
    /// Lógica de interacción para juego.xaml
    /// </summary>
    public partial class Juego : Window
    {
        public Juego(sbyte cuantas)
        {
            InitializeComponent();
            CojeCartas(cuantas);
            string[,] a = new string[5, 4]{ 
                { @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg" },
                { @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg" },
                { @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg" },
                { @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg" },
                { @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja1.jpg" }
                };

            DibujarCartas(a);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.MainWindow.Show();
        }

        private string[] CojeCartas(int cuantas)
        {
            string[] cartas = new string[cuantas];

            throw new NotImplementedException();
            return cartas;
        }

        private string[] DuplicaCartas(string[] cartas)
        {
            string[] cartasConParejas = new string[cartas.Length * 2];

            cartas.CopyTo(cartasConParejas, 0);
            cartas.CopyTo(cartasConParejas, cartas.Length);

            return cartasConParejas;
        }

        private string[] MezclaCartas(string[] cartas)
        {
            int vueltas = 1000;

            int a;
            int b;
            string c;

            Random rnd = new Random();

            for ( int i = 0; i < vueltas; i++ )
            {
                a = rnd.Next(0, cartas.Length);
                b = rnd.Next(0, cartas.Length);

                c = cartas[a];
                cartas[a] = cartas[b];
                cartas[b] = c;
            }
            
            return cartas;
        }

        private string[,] FormaTablero(string[] cartas)
        {
            int lineas = 10;
            int columnas;
            int contador = 0;

            while ( cartas.Length % lineas != 0 ) { lineas--; }

            columnas = cartas.Length / lineas;

            string[,] tablero = new string[lineas, columnas];

            for ( int i = 0; i < lineas; i++ )
                for ( int j = 0; j < columnas; j++ )
                    tablero[i, j] = cartas[contador++];

            return tablero;
        }

        private void DibujarCartas(String[,] cartas)
        {

            for ( int i = 0; i < cartas.GetLength(0); i++ )
            {
                // crear dockpanel
                DockPanel a = new DockPanel();
                a.Height = 100;

                for ( int j = 0; j < cartas.GetLength(1); j++ )
                {
                    // parseamos la ruta
                    Uri imageUri = new Uri(cartas[i,j], UriKind.RelativeOrAbsolute);
                    BitmapImage bitmap = new BitmapImage(imageUri);

                    // crear la imagen
                    Image carta = new Image();
                    carta.Source = bitmap;
                    // carta.Visibility = Visibility.Hidden;

                    // anadimos la carta al dockpanel
                    a.Children.Add(carta);
                }

                // anadir dockpanel al container
                stackContainer.Children.Add(a);
            }
        }
    }
}
