using System;
using System.Collections.Generic;
using System.Text;
using Logic;

namespace Model
{
    public class BallModel
    {
        public double X { get; set; }
        public double Y { get; set; }

        public BallModel(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
