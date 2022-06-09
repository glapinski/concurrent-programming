using System;
using Data;

namespace Logic
{
    public class Collision
    {
        public static bool IsCollision(IBall current, IBall other)
        {
            double distance = Math.Sqrt(Math.Pow(current.PositionX + current.MoveX - (other.PositionX + other.MoveX), 2) + Math.Pow(current.PositionY + current.MoveY - (other.PositionY + other.MoveY), 2));

            if (Math.Abs(distance) <= current.Radius + other.Radius)
            {
                return true;
            }

            return false;
        }

        public static void IsTouchingBoundaries(IBall ball, int boardSize)
        {
            double newX = ball.PositionX + ball.MoveX;

            if ((newX > boardSize && ball.PositionX > 0) || (newX < 0 && ball.PositionX < 0))
            {
                ball.SpeedX *= -1;
            }

            double newY = ball.PositionY + ball.MoveY;

            if ((newY > boardSize && ball.PositionY > 0) || (newY < 0 && ball.PositionY < 0))
            {
                ball.SpeedY *= -1;
            }

        }

        public static void ImpulseSpeed(IBall current, IBall other)
        {
            Vector2 currentVelocity = new Vector2(current.SpeedX, current.SpeedY);
            Vector2 currentPosition = new Vector2(current.PositionX, current.PositionY);
            double currentMass = current.Mass;

            Vector2 otherVelocity = new Vector2(other.SpeedX, other.SpeedY);
            Vector2 otherPosition = new Vector2(other.PositionX, other.PositionY);
            double otherMass = other.Mass;


            double fDistance = Math.Sqrt((currentPosition.X - otherPosition.X) * (currentPosition.X - otherPosition.X) + (currentPosition.Y - otherPosition.Y) * (currentPosition.Y - otherPosition.Y));

            double nx = (otherPosition.X - currentPosition.X) / fDistance;
            double ny = (otherPosition.Y - currentPosition.Y) / fDistance;

            double tx = -ny;
            double ty = nx;

            // Dot Product Tangent
            double dpTan1 = currentVelocity.X * tx + currentVelocity.Y * ty;
            double dpTan2 = otherVelocity.X * tx + otherVelocity.Y * ty;

            // Dot Product Normal
            double dpNorm1 = currentVelocity.X * nx + currentVelocity.Y * ny;
            double dpNorm2 = otherVelocity.X * nx + otherVelocity.Y * ny;

            // Conservation of momentum in 1D
            double m1 = (dpNorm1 * (currentMass - otherMass) + 2.0f * otherMass * dpNorm2) / (currentMass + otherMass);
            double m2 = (dpNorm2 * (otherMass - currentMass) + 2.0f * currentMass * dpNorm1) / (currentMass + otherMass);

            double currentNewVelocityX = tx * dpTan1 + nx * m1;
            double currentNewVelocityY = ty * dpTan1 + ny * m1;

            double otherNewVelocityX = tx * dpTan2 + nx * m2;
            double otherNewVelocityY = ty * dpTan2 + ny * m2;

            current.SpeedX = currentNewVelocityX;
            current.SpeedY = currentNewVelocityY;

            other.SpeedX = otherNewVelocityX;
            other.SpeedY = otherNewVelocityY;
        }
    }
}
