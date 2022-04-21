using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    internal class Ball
    {
        public int xPosition { get; set; }
        public int yPosition { get; set; }

        private int xSpeed { get; set; }
        private int ySpeed { get; set; }

        public int radius { get; set; }
        public string color { get; }

        public Ball(int xPosition, int yPosition, int xSpeed, int ySpeed, int radius)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.radius = radius;
            this.color = "#fc1100";
        }
    }
}
