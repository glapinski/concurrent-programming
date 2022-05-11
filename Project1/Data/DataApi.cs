using System;

namespace Data
{
    public abstract class DataAbstractAPI
    {
        public abstract double getBallPositionX(int ballId);
        public abstract double getBallPositionY(int ballId);
        public abstract double getBallRadious(int ballId);
        public abstract double getBallXSpeed(int ballId);
        public abstract double getBallYSpeed(int ballId);
        public abstract void setBallXSpeed(int ballId, double newXSpeed);
        public abstract void setBallYSpeed(int ballId, double newYSpeed);
        public abstract void createBalls(int ballsAmount);
        public static DataAbstractAPI CreateAPI()
        { 
            return new DataAPI(); 
        }
    }
    internal class DataAPI : DataAbstractAPI
    {
        private BallRepository ballRepository;
        public DataAPI()
        {
            this.ballRepository = new BallRepository();
        }
        public override void createBalls(int ballsAmount)
        {
            ballRepository.CreateBalls(ballsAmount);
        }

        public override double getBallPositionX(int ballId)
        {
            return this.ballRepository.getBall(ballId).x;
        }

        public override double getBallPositionY(int ballId)
        {
            return this.ballRepository.getBall(ballId).y;
        }

        public override double getBallRadious(int ballId)
        {
            return this.ballRepository.getBall(ballId).r;
        }

        public override double getBallXSpeed(int ballId)
        {
            return this.ballRepository.getBall(ballId).xS;
        }
        public override double getBallYSpeed(int ballId)
        {
            return this.ballRepository.getBall(ballId).yS;
        }

        public override void setBallXSpeed(int ballId, double newXSpeed)
        {
            this.ballRepository.getBall(ballId).xS = newXSpeed;
        }

        public override void setBallYSpeed(int ballId, double newYSpeed)
        {
            this.ballRepository.getBall(ballId).yS = newYSpeed;
        }
    }
}
