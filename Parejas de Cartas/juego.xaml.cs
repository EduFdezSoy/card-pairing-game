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
using System.Windows.Threading;

namespace Parejas_de_Cartas
{
    /// <summary>
    /// Lógica de interacción para juego.xaml
    /// </summary>
    public partial class Juego : Window
    {
        string[,] tablero;
        string trasera;
        Image seleccionAnterior;

        public Juego(sbyte cuantas, Tematica tema)
        {
            InitializeComponent();

            string[] barajaCompleta = SeleccionarTematica(tema);
            string[] barajaSeleccionada = CojeCartas(cuantas, barajaCompleta);
            string[] barajaAUsar = DuplicaCartas(barajaSeleccionada);
            string[] barajaMezclada = MezclaCartas(barajaAUsar);
            trasera = EligeTrasera();
            tablero = FormaTablero(barajaMezclada);
            DibujarCartas(tablero);
        }

        // ----- EVENTOS -----

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.MainWindow.Show();
        }

        private void Carta_Click(object sender, EventArgs e)
        {
            int fila = int.Parse(((Image)sender).Name.Split('_')[1]);
            int columna = int.Parse(((Image)sender).Name.Split('_')[2]);

            // para la primera vez hacemos esto
            if (seleccionAnterior == null)
            {
                ((Image)sender).Source = new BitmapImage(new Uri(tablero[fila, columna], UriKind.RelativeOrAbsolute));
                seleccionAnterior = (Image)sender;
            }

            // si seleccionamos la misma la ocultamos
            else if (seleccionAnterior == (Image)sender)
                ((Image)sender).Source = new BitmapImage(new Uri(trasera, UriKind.RelativeOrAbsolute));

            // si es la misma las dejamos descubiertas
            else if (seleccionAnterior.Source == ((Image)sender).Source)
                ((Image)sender).Source = new BitmapImage(new Uri(tablero[fila, columna], UriKind.RelativeOrAbsolute));

            // si no son la misma las mostramos dos segundos y las volteamos de nuevo
            else
            {
                ((Image)sender).Source = new BitmapImage(new Uri(tablero[fila, columna], UriKind.RelativeOrAbsolute));
                seleccionAnterior = (Image)sender;
                DelayAction(2000, new Action(() =>
                {
                    ((Image)sender).Source = new BitmapImage(new Uri(trasera, UriKind.RelativeOrAbsolute));
                    seleccionAnterior.Source = new BitmapImage(new Uri(trasera, UriKind.RelativeOrAbsolute));
                }));
            }        
        }

        // ----- METODOS -----

        public enum Tematica { Star, StevenUniverse, MyHeroAcademia};

        private string[] SeleccionarTematica(Tematica tema)
        {
            switch (tema)
            {
                case Tematica.Star:
                    return new string[] {@"Juego de cartas\Star\star1.jpg", @"Juego de cartas\Star\star2.jpg", @"Juego de cartas\Star\star3.jpg", 
                                         @"Juego de cartas\Star\star4.jpg", @"Juego de cartas\Star\star5.jpg", @"Juego de cartas\Star\star6.jpg",
                                         @"Juego de cartas\Star\star7.jpg", @"Juego de cartas\Star\star8.jpg", @"Juego de cartas\Star\star9.jpg", 
                                         @"Juego de cartas\Star\star10.jpg", @"Juego de cartas\Star\star11.jpg", @"Juego de cartas\Star\star12.jpg",
                                         @"Juego de cartas\Star\star13.jpg", @"Juego de cartas\Star\star14.jpg", @"Juego de cartas\Star\star15.jpg",
                                         @"Juego de cartas\Star\star16.jpg", @"Juego de cartas\Star\star17.jpg", @"Juego de cartas\Star\star18.jpg"};
                case Tematica.StevenUniverse:
                    return new string[] {@"Juego de cartas\Star\star1.jpg", @"Juego de cartas\Star\star2.jpg", @"Juego de cartas\Star\star3.jpg", 
                                         @"Juego de cartas\Star\star4.jpg", @"Juego de cartas\Star\star5.jpg", @"Juego de cartas\Star\star6.jpg",
                                         @"Juego de cartas\Star\star7.jpg", @"Juego de cartas\Star\star8.jpg", @"Juego de cartas\Star\star9.jpg", 
                                         @"Juego de cartas\Star\star10.jpg", @"Juego de cartas\Star\star11.jpg", @"Juego de cartas\Star\star12.jpg",
                                         @"Juego de cartas\Star\star13.jpg", @"Juego de cartas\Star\star14.jpg", @"Juego de cartas\Star\star15.jpg",
                                         @"Juego de cartas\Star\star16.jpg", @"Juego de cartas\Star\star17.jpg", @"Juego de cartas\Star\star18.jpg"};
                case Tematica.MyHeroAcademia:
                    return new string[] {@"Juego de cartas\Star\star1.jpg", @"Juego de cartas\Star\star2.jpg", @"Juego de cartas\Star\star3.jpg", 
                                         @"Juego de cartas\Star\star4.jpg", @"Juego de cartas\Star\star5.jpg", @"Juego de cartas\Star\star6.jpg",
                                         @"Juego de cartas\Star\star7.jpg", @"Juego de cartas\Star\star8.jpg", @"Juego de cartas\Star\star9.jpg", 
                                         @"Juego de cartas\Star\star10.jpg", @"Juego de cartas\Star\star11.jpg", @"Juego de cartas\Star\star12.jpg",
                                         @"Juego de cartas\Star\star13.jpg", @"Juego de cartas\Star\star14.jpg", @"Juego de cartas\Star\star15.jpg",
                                         @"Juego de cartas\Star\star16.jpg", @"Juego de cartas\Star\star17.jpg", @"Juego de cartas\Star\star18.jpg"};
            }

            throw new Exception("La tematica seleccionada no existe.");
        }

        private string[] CojeCartas(int cuantas, string[] cartas)
        {
            List<string> listaCartas = new List<string>(cartas);
            Random rnd = new Random();
            int aux;
            string[] cartasSeleccionadas = new string[cuantas];

            if (cartas.Length < cuantas)
                throw new Exception("No hay suficientes cartas para jugar");

            for (int i = 0; i < cuantas; i++)
            {
                aux = rnd.Next(0, cartas.Length - i);
                cartasSeleccionadas[i] = listaCartas[aux];
                listaCartas.RemoveAt(aux);
            }

            return cartasSeleccionadas;
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
            int vueltas = 5000;

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
            int columnas = 10;
            int lineas;
            int contador = 0;

            while ( cartas.Length % columnas != 0 ) { columnas--; }

            lineas = cartas.Length / columnas;

            string[,] tablero = new string[lineas, columnas];

            for ( int i = 0; i < lineas; i++ )
                for (int j = 0; j < columnas; j++)
                    tablero[i, j] = cartas[contador++];

            return tablero;
        }

        private string EligeTrasera()
        {
            Random rnd = new Random();
            string[] traseras = {
                                    @"Traseras de Cartas\azul1.jpg", @"Traseras de Cartas\azul2.jpg", @"Traseras de Cartas\azul3.jpg",
                                    @"Traseras de Cartas\roja1.jpg", @"Traseras de Cartas\roja2.jpg", @"Traseras de Cartas\roja3.jpg",
                                    @"Traseras de Cartas\negra1.jpg", @"Traseras de Cartas\negra2.jpg", @"Traseras de Cartas\negra3.jpg", 
                                    @"Traseras de Cartas\negra4.jpg", @"Traseras de Cartas\negra5.jpg"
                                };
            
            return traseras[rnd.Next(0,traseras.Length)];
        }

        private void DibujarCartas(String[,] cartas)
        {

            for ( int i = 0; i < cartas.GetLength(0); i++ )
            {
                // crear dockpanel
                DockPanel a = new DockPanel();
                a.Height = 175;

                for ( int j = 0; j < cartas.GetLength(1); j++ )
                {
                    // parseamos la ruta
                    //Uri imageUri = new Uri(cartas[i,j], UriKind.RelativeOrAbsolute);
                    Uri imageUri = new Uri(trasera, UriKind.RelativeOrAbsolute);
                    BitmapImage bitmap = new BitmapImage(imageUri);

                    // crear la imagen
                    Image carta = new Image();
                    carta.Source = bitmap;
                    carta.Name = string.Format("Pos_{0}_{1}", i, j);
                    carta.MouseLeftButtonUp += new MouseButtonEventHandler(Carta_Click);

                    // anadimos la carta al dockpanel
                    a.Children.Add(carta);
                }

                // anadir dockpanel al container
                stackContainer.Children.Add(a);
            }
        }


        /// <summary>
        /// DelayAction(5000, new Action(() =>
        ///     {
        ///         ((Image)sender).Source = new BitmapImage(new Uri(trasera, UriKind.RelativeOrAbsolute));
        ///     }));
        /// </summary>
        /// <param name="millisecond"></param>
        /// <param name="action"></param>
        private static void DelayAction(int millisecond, Action action)
        {
            var timer = new DispatcherTimer();
            timer.Tick += delegate
            {
                action.Invoke();
                timer.Stop();
            };

            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Start();
        }
    }
}
