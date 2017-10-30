namespace TSP_Immune
{
    public class City
    {
        private double x, y;
        public double X { get { return x; } }
        public double Y { get { return y; } }
        public City(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}