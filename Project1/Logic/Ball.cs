using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Logic
{
    public abstract class BallAbstract
    {
        public abstract float xPosition { get; set; }
        public abstract float yPosition { get; set; }
        public abstract float xSpeed { get; set; }
        public abstract float ySpeed { get; set; }
        public abstract float radius { get; set; }

        public abstract void UpdatePosition();
        public abstract void ChangeDirection(char axis);
        public static BallAbstract CreateAPI(float x, float y, float xSpeed, float ySpeed, float radius)
        {
            return new Ball(x, y, xSpeed, ySpeed, radius);
        }
    }

    internal class Ball : BallAbstract
    {
        private float x;
        private float y;
        private float xS;
        private float yS;
        private float r;
        public override float xPosition
        {
            get => x;
            set => x = value;
        }
        public override float yPosition 
        { 
            get => y;
            set => y = value; 
        }
        public override float xSpeed 
        { 
            get => xS;
            set => xS = value; 
        }
        public override float ySpeed 
        { 
            get => yS; 
            set => yS = value; 
        }
        public override float radius 
        { 
            get => r; 
            set => r = value; 
        }

        public Ball(float xPosition, float yPosition, float xSpeed, float ySpeed, float radius)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.radius = radius;
        }

        public override void UpdatePosition()
        {
            this.xPosition += this.xSpeed;
            this.yPosition += this.ySpeed;
        }

        public override void ChangeDirection(char axis)
        {
            if (axis == 'x')
            {
                if (this.xSpeed > 0)
                {
                    this.xSpeed *= -1;
                }
            }
            else if (axis == 'y')
            {
                if (this.ySpeed > 0)
                {
                    ySpeed *= -1;
                }
            }
        }
    }
}
