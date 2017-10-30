using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Immune
{
    class ElasticNeuralNetwork
    {
        private int Neurons_Number;
        private double NEAR = 0.01;
        private double MOMENTUM = 0.999;
        private City[] Cities;
        private double[,] Neuron_X_Y;
        public double[,] Weight;
        private double[,] Radius;
        private double theta; // Коэффициент обучаемости нейронной сети
        private double phi; // Коэффициент соседства нейронов
        private Random rnd = new Random();

        public ElasticNeuralNetwork(City[] cities, int neurons_number,double THETA,double PHI)
        {
          
            Neurons_Number = neurons_number;
            Cities = cities;
            theta = THETA;
            phi = PHI;

            Initialize();
        }

        private void Initialize()
        {
            // [Индекс города][0 = x, 1 = y]
            Neuron_X_Y = new double[Neurons_Number, 2];         // [Размер нейронной сети][0 = x, 1 = y]
            Weight = new double[Neurons_Number, 2];           // [Размер нейронной сети][0 = x, 1 = y]
            Radius = new double[Neurons_Number, Neurons_Number]; // Радиус соседства нейронов (кольца)
            
            for (int i = 0; i < Neurons_Number; i++) // Распределяем нейроны по кольцу
            {
                var angle = 2 * Math.PI * i / Neurons_Number;  // KOLCO

                Neuron_X_Y[i, 0] = 0.5 + 0.5 * Math.Cos(angle);
                Neuron_X_Y[i, 1] = 0.5 + 0.5 * Math.Sin(angle);

                Weight[i, 0] = rnd.NextDouble();
                Weight[i, 1] = rnd.NextDouble();
            }
            ComputeMatrix(theta); // классифицируют линейный массив 
        }

        private void ComputeMatrix(double theta)
        {
            for (int i = 0; i < Neurons_Number; i++)
            {
                Radius[i, i] = 1.0;
                for (int j = i + 1; j < Neurons_Number; j++)
                {
                    Radius[i, j] = Math.Exp(-1.0 * (GetDistance(i, j) * GetDistance(i, j)) / (2.0 * Math.Pow(theta, 2)));
                    Radius[j, i] = Radius[i, j];
                }
            }
        }

        private double GetDistance(int index, int index2) //порядок соседства между победившим нейроном J и представленным нейроном j, 
        {
            double dx = Neuron_X_Y[index, 0] - Neuron_X_Y[index2, 0];
            double dy = Neuron_X_Y[index, 1] - Neuron_X_Y[index2, 1];
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private int FindMinimum(double location1, double location2)
        {
            double minimumDistance = double.MaxValue;
            int minimumIndex = -1;
            for (int i = 0; i < Neurons_Number; i++)
            {
                double distance = (Math.Pow((location1 - Weight[i, 0]), 2)) + (Math.Pow((location2 - Weight[i, 1]), 2));//Ищет нейрон с минимальным расстоянием до города
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    minimumIndex = i;
                }
            }
            return minimumIndex;
        }

        public void AlgorithmIteration()
        {
            // Pick a city for random comparison
            var index = (int)(rnd.NextDouble() * Cities.Length);
            var locationX = Cities[index].X + (rnd.NextDouble() * NEAR) - NEAR / 2;
            var locationY = Cities[index].Y + (rnd.NextDouble() * NEAR) - NEAR / 2;

            var minimumIndex = FindMinimum(locationX, locationY);

            // Update all weights.
            for (int i = 0; i < Neurons_Number; i++)
            {
                Weight[i, 0] += (phi * Radius[i, minimumIndex] * (locationX - Weight[i, 0]));
                Weight[i, 1] += (phi * Radius[i, minimumIndex] * (locationY - Weight[i, 1]));
                //Radius[i, i] = 1.0;
                //for (int j = i + 1; j < Neurons_Number; j++)
                //{
                //    Radius[i, j] = Math.Exp(-1.0 * (GetDistance(i, j) * GetDistance(i, j)) / (2.0 * Math.Pow(theta, 2)));//получаем ответвленную дистанцию между победившим нейроном  и его соседом 
                //    Radius[j, i] = Radius[i, j];
                //}
            }

            phi *= MOMENTUM;
            theta *= MOMENTUM;

            ComputeMatrix(theta);
        }

        private void PrintRing()
        {
            for (int i = 0; i < Neurons_Number; i++)
            {
                string tempX = string.Format("{0:F1}", Weight[i, 0]);
                string tempY = string.Format("{0:F1}", Weight[i, 1]);
                Console.WriteLine("(" + tempX + ", " + tempY + ") ");
            }
            Console.WriteLine();
        }
    }
}
