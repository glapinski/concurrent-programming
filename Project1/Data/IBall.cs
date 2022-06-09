using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IBall : IObservable<IBall>
    {
        int Id { get; }

        double PositionX { get; }
        double PositionY { get; }

        int Radius { get; }
        double Mass { get; }

        double SpeedX { get; set; }
        double SpeedY { get; set; }

        double MoveX { get; set; }
        double MoveY { get; set; }
    }
}