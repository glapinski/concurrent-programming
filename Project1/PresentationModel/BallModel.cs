using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    public class BallInModel : IBall
    {
        //public int BallId { get; set; }
        public int Diameter { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public BallInModel(double top, double left, int radius)
        {
            //this.BallId = ballId;
            Top = top;
            Left = left;
            Diameter = radius * 2;
        }

        private double top;

        public double Top
        {
            get { return top; }
            set
            {
                if (top == value)
                    return;
                top = value;
                RaisePropertyChanged();
            }
        }

        private double left;

        public double Left
        {
            get { return left; }
            set
            {
                if (left == value)
                    return;
                left = value;
                RaisePropertyChanged();
            }
        }

        public void Move(double poitionX, double positionY)
        {
            Left = poitionX;
            Top = positionY;
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}