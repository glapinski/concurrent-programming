using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class BallAbstract
    {
        public abstract float BallX { get; }
        public abstract float BallY { get; }
        public abstract float BallXSpeed { get; }
        public abstract float BallYSpeed { get; }
        public abstract float BallRadius { get; }
        public static BallAbstract CreateBall(ref Logic.BallAbstract parent)
        {
            return new Ball(ref parent);
        }
    }

    internal class Ball : BallAbstract
    {
        private Logic.BallAbstract _parent;
        public override float BallX
        {
            get => _parent.xPosition;
        }
        public override float BallY
        {
            get => _parent.yPosition;
        }
        public override float BallXSpeed
        {
            get => _parent.xSpeed;
        }
        public override float BallYSpeed
        {
            get => _parent.ySpeed;
        }
        public override float BallRadius
        {
            get => _parent.radius;
        }
        public Ball(ref Logic.BallAbstract parent)
        {
            _parent = parent;
        }
    }
}
