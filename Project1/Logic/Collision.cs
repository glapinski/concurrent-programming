using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Collision
    {
        private int mass;
        private int radius;
        private Vector2 position;
        private Vector2 velocity;

        public Collision(double positionX, double positionY,double speedX, double speedY, int radius,int mass)
        {
            this.velocity = new Vector2(speedX, speedY);
            this.position = new Vector2(positionX, positionY);
            this.radius = radius;
            this.mass = mass;
        }

        public bool IsCollision(double tmpX, double tmpY, double tmpRadius, bool mode)
        {
            double actualX;
            double actualY;
            if(mode)
            {
                actualX = position.X + velocity.X;
                actualY = position.Y + velocity.Y;
            }

            else
            {
                actualX = position.X;
                actualY = position.Y;
            }
            double distance = Math.Sqrt(Math.Pow(actualX - tmpX,2) + Math.Pow(actualY - tmpY, 2));
        
            if(Math.Abs(distance)<=radius + tmpRadius)
            {
                return true;
            }
            return false;
        }

        public bool areXTouching(int boardSize)
        {
            double newX = position.X + velocity.X;
            if ((newX > boardSize && velocity.X > 0) || (newX < 0 && &velocity.X < 0))
            {
                return true;
            }
            return false;
         }

        public bool areYTouching(int boardSize)
        {
            double newY = position.Y + velocity.Y;
            if ((newY > boardSize && velocity.Y > 0) || (newY < 0 && &velocity.Y < 0))
            {
                return true;
            }
            return false;
        }

        public Vector2[] HitSpeed (double tmpX, double tmpY , double speedX, double speedY,double tmpMass)
        {
            Vector2 velocityTmp = new Vector2(speedX, speedY);
            Vector2 positionTmp = new Vector2(tmpX, tmpY);

            double fDistance = Math.Sqrt((position.X - positionTmp.X) * (position.X - positionTmp.X) + (position.Y - positionTmp.Y) * (position.Y - positionTmp.Y));

            double newX = (positionTmp.X - position.X) / fDistance;
            double newY = (positionTmp.Y - position.Y) / fDistance;

            double tx = -newY;
            double ty = newX;


        }
    }
