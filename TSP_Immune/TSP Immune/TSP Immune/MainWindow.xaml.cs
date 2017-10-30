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
using System.Windows.Threading;
using Microsoft.Win32;
using System.IO;


namespace TSP_Immune
{
    public partial class MainWindow : Window
    {
        public static Random rnd = new Random();
        private const int WorldWidth = 1200;
        private const int WorldHeight = 650;
        private const int WorldPadding = 50;
        int Iteration_Number;
        private const int CitySize = 3;
        private const int NeuronSize = 2;
        private int _iterationsPerTick;
        private DispatcherTimer _timer;
        private ElasticNeuralNetwork _elasticNeuralNetwork;
        private City[] _cities;
        private Ellipse[] _neuronsCircles;
        private Line[] _neuronsLines;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += TimerOnTick;
            StartButton.IsEnabled = false;
        }


        private void TimerOnTick(object sender, EventArgs eventArgs)
        {

            double Len = GetWay();
            Length_label.Content = (Math.Round(Len, 2)).ToString();
            if (_elasticNeuralNetwork == null) return;
            for (var i = 0; i < _iterationsPerTick; i++)
            {
                _elasticNeuralNetwork.AlgorithmIteration();

                IterationCount.Content = ++Iteration_Number;
            }

            var points = new Point[_elasticNeuralNetwork.Weight.GetLength(0)];
            for (var i = 0; i < points.Length; i++)
                points[i] = new Point(_elasticNeuralNetwork.Weight[i, 0], _elasticNeuralNetwork.Weight[i, 1]);

            for (var i = 0; i < points.Length; i++)
            {
                var circle = _neuronsCircles[i];
                Canvas.SetLeft(circle, points[i].X - NeuronSize);
                Canvas.SetTop(circle, points[i].Y - NeuronSize);

                var line = _neuronsLines[i];
                line.X1 = points[i].X;
                line.Y1 = points[i].Y;
                line.X2 = points[(i + 1) % points.Length].X;
                line.Y2 = points[(i + 1) % points.Length].Y;
            }
        }

        private void Generate_Cities_Buton(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            Iteration_Number = 0;
            IterationCount.Content = "0";
            var citiesCount = Convert.ToInt32(CitiesCountTextBox.Text);
            if (citiesCount > 6 && citiesCount < 250)
            {
                var random = new Random();
                _cities = new City[citiesCount];
                for (var i = 0; i < citiesCount; i++)
                {
                    do
                    {
                        _cities[i] = new City(
                            random.Next(WorldWidth - 2 * WorldPadding) + WorldPadding,
                            random.Next(WorldHeight - 2 * WorldPadding) + WorldPadding);
                    } while (i != 0 && Enumerable.Range(0, i).Select(j => _cities[j]).Min(j => Math.Pow(j.X - _cities[i].X, 2) + Math.Pow(j.Y - _cities[i].Y, 2)) < (4 * CitySize) * (4 * CitySize));
                }
                Canvas.Children.Clear();
                foreach (var city in _cities)
                {
                    var circle = new Ellipse()
                    {
                        Stroke = Brushes.Red,
                        Width = CitySize * 2,
                        Height = CitySize * 2,
                    };
                    Canvas.SetLeft(circle, city.X - CitySize);
                    Canvas.SetTop(circle, city.Y - CitySize);
                    Canvas.Children.Add(circle);
                }
                StartButton.IsEnabled = true;
            }
            else MessageBox.Show("Неверные данные!");
        }
        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        private void Start_Algoritm(object sender, RoutedEventArgs e)
        {

            Iteration_Number = 0;
            IterationCount.Content = "0";
            Canvas.Children.Clear();
            foreach (var city in _cities)
            {
                var circle = new Ellipse()
                {
                    Stroke = Brushes.Red,
                    Width = CitySize * 2,
                    Height = CitySize * 2,
                };
                Canvas.SetLeft(circle, city.X - CitySize);
                Canvas.SetTop(circle, city.Y - CitySize);
                Canvas.Children.Add(circle);
            }
            try
            {
                var phi = double.Parse(Neightbourhood_Koef_Textbox.Text);
                var theta = double.Parse(Stud_Coef_Textbox.Text);
                var neuronsCount = int.Parse(NeuronsCountTextBox.Text);
                _iterationsPerTick = int.Parse(IterationsCountTextBox.Text);
                _elasticNeuralNetwork = new ElasticNeuralNetwork(_cities, neuronsCount, phi, theta);
                _neuronsCircles = new Ellipse[neuronsCount];
                _neuronsLines = new Line[neuronsCount];
                if (phi < 2 && phi > 0.01 && theta > 0.1 && theta < 5 && _iterationsPerTick > 0 && _iterationsPerTick < 101)
                {
                    for (var i = 0; i < neuronsCount; i++)
                    {
                        _neuronsCircles[i] = new Ellipse
                        {
                            Fill = Brushes.GreenYellow,
                            Width = NeuronSize * 2,
                            Height = NeuronSize * 2

                        };
                        _neuronsLines[i] = new Line()
                        {
                            Stroke = Brushes.LightSeaGreen,
                            StrokeThickness = 1.5
                        };

                    }

                    foreach (var circle in _neuronsCircles)
                        Canvas.Children.Add(circle);
                    foreach (var line in _neuronsLines)
                        Canvas.Children.Add(line);


                    _timer.Start();


                }
                else
                    throw new OverflowException();
            }
            catch
            {
                MessageBox.Show("Неверные данные!");
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileName = "Cities File";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "TXT Files(.txt)|*.txt";
            dialog.Title = "Выберите txt файл";

            if (dialog.ShowDialog() == true)
            {

                _cities = ReadAndPArse(dialog.FileName);

            }
            else
            {
                MessageBox.Show("Вы не выбрали файл!!");
                return;

            }

            try
            {
                Canvas.Children.Clear();
                foreach (var city in _cities)
                {

                    var circle = new Ellipse()
                    {
                        Stroke = Brushes.Red,
                        Width = CitySize * 2,
                        Height = CitySize * 2,

                    };
                    Canvas.SetLeft(circle, city.X - CitySize);
                    Canvas.SetTop(circle, city.Y - CitySize);
                    Canvas.Children.Add(circle);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("File is empty!");
            }
            StartButton.IsEnabled = true;
            
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Stream mystream;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "TSP_Result";
            dialog.Filter = "TXT Files(.txt)|*.txt";
            dialog.Title = "Выберите куда сохранить";
            if (dialog.ShowDialog() == true)
            {
                if ((mystream = dialog.OpenFile()) != null)
                {
                    using (StreamWriter writer=new StreamWriter(mystream, Encoding.UTF8))
                    {
                        writer.WriteLine("Координаты:");
                        for (int i = 0; i < _cities.Length; i++)
                        {
                            //writer.WriteLine(Math.Round((((_elasticNeuralNetwork.Weight[i,0] - 60) * 120) / Canvas.ActualWidth), 1) + " " + Math.Round((((_elasticNeuralNetwork.Weight[i,1]) - 60) * 60) / Canvas.ActualHeight), 1);
                            writer.WriteLine(Math.Round((((_cities[i].X - 60) * 120) / Canvas.ActualWidth), 1) + " " + Math.Round((((_cities[i].Y) - 60) * 60) / Canvas.ActualHeight), 1);
                        }
                        writer.WriteLine("Длина цикла:"+Length_label.Content);
                        writer.WriteLine("Параметры алгоритма:");
                        writer.WriteLine("Нейронов:" + NeuronsCountTextBox.Text + "; " + "Количество городов:" + _cities.Length);
                        writer.WriteLine("Коэффициент обучаемости:" + Stud_Coef_Textbox.Text +"; "+ "Коэффициент соседства нейронов:" + Neightbourhood_Koef_Textbox.Text);
                    }
                }
                Visual visual = this; ;
                int quality = 100;
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
           (int)SystemParameters.FullPrimaryScreenWidth,
           (int)SystemParameters.FullPrimaryScreenHeight,
           96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(visual);

                JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                jpegBitmapEncoder.QualityLevel = quality;
                jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                string fileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+ "\\TSP_Screenshot.jpg";
                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileStream fileStream = new FileStream(fileName, FileMode.CreateNew);
                jpegBitmapEncoder.Save(fileStream);
                fileStream.Close();

                MessageBox.Show("Результат успешно сохранен!");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private City[] ReadAndPArse(string path)
        {
            City[] cities = null;
            List<City> citki = new List<City>();

            string Line;
            using (var reader = new StreamReader(path))
            {

                while ((Line = reader.ReadLine()) != null)
                {

                    string[] parse = { " ", "" };
                    string[] row = Line.Split(parse, StringSplitOptions.None);
                    cities = new City[row.Length / 2];

                    for (int i = 0; i < row.Length / 2; i++)
                    {
                        cities[i] = new City((((double.Parse(row[i+1]) * Canvas.ActualWidth) / 120.0) +60.0), (((double.Parse(row[i + 2]) * Canvas.ActualHeight) / 60.0) + 60.0));
                        citki.Add(cities[i]);
                    }
                }
            }
            return citki.ToArray(); 

            
        }
        private double GetWay()
        {
            double Len = 0;
            for (int i = 0; i < _elasticNeuralNetwork.Weight.Length / 2; i++)
            {
                double dx = _elasticNeuralNetwork.Weight[i, 0] - _elasticNeuralNetwork.Weight[(i + 1) % (_elasticNeuralNetwork.Weight.Length / 2), 0];
                double dy = _elasticNeuralNetwork.Weight[i, 1] - _elasticNeuralNetwork.Weight[(i + 1) % (_elasticNeuralNetwork.Weight.Length / 2), 1];
                Len += Math.Sqrt(dx * dx + dy * dy);
            }
            return Len /10.5;

        }
    }
}

