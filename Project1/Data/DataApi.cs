using System;
using System.Collections.Generic;
using System.Threading;

namespace Data
{
    public abstract class DataAbstractAPI : IObserver<int>, IObservable<int>
    {
        public abstract double getBallPositionX(int ballId);
        public abstract double getBallPositionY(int ballId);
        public abstract int getBallRadius(int ballId);
        public abstract double getBallSpeedX(int ballId);
        public abstract double getBallSpeedY(int ballId);
        public abstract double getBallMass(int ballId);
        public abstract int getBoardSize();
        public abstract void setBallSpeed(int ballId, double speedX, double speedY);
        public abstract void createBalls(int ballsAmount);
        public abstract int getBallsAmount();

        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(int value);

        public abstract IDisposable Subscribe(IObserver<int> observer);

        public static DataAbstractAPI CreateDataApi()
        {
            return new DataApi();
        }

        private class DataApi : DataAbstractAPI
        {
            private BallRepository ballRepository;
            private IDisposable unsubscriber;
            static object _lock = new object();
            private IList<IObserver<int>> observers;
            private Barrier barrier;

            public DataApi()
            {
                this.ballRepository = new BallRepository();

                observers = new List<IObserver<int>>();
            }

            public override double getBallPositionX(int ballId)
            {
                return this.ballRepository.GetBall(ballId).PositionX;
            }

            public override double getBallPositionY(int ballId)
            {
                return this.ballRepository.GetBall(ballId).PositionY;
            }

            public override int getBoardSize()
            {
                return ballRepository.BoardSize;
            }

            public override double getBallMass(int ballId)
            {
                return this.ballRepository.GetBall(ballId).Mass;
            }

            public override int getBallRadius(int ballId)
            {
                return this.ballRepository.GetBall(ballId).Radius;
            }

            public override double getBallSpeedX(int ballId)
            {
                return this.ballRepository.GetBall(ballId).MoveX;
            }

            public override double getBallSpeedY(int ballId)
            {
                return this.ballRepository.GetBall(ballId).MoveY;
            }

            public override void setBallSpeed(int ballId, double speedX, double speedY)
            {
                this.ballRepository.GetBall(ballId).MoveX = speedX;
                this.ballRepository.GetBall(ballId).MoveY = speedY;
            }

            public override int getBallsAmount()
            {
                return ballRepository.balls.Count;
            }

            public override void createBalls(int ballsAmount)
            {
                barrier = new Barrier(ballsAmount);
                ballRepository.CreateBalls(ballsAmount);

                foreach (var ball in ballRepository.balls)
                {
                    Subscribe(ball);
                    ball.StartMoving();
                }

            }

            #region observer

            public virtual void Subscribe(IObservable<int> provider)
            {
                if (provider != null)
                    unsubscriber = provider.Subscribe(this);
            }

            public override void OnCompleted()
            {
                Unsubscribe();
            }

            public override void OnError(Exception error)
            {
                throw error;
            }

            public override void OnNext(int value)
            {
                barrier.SignalAndWait();

                foreach (var observer in observers)
                {
                    observer.OnNext(value);
                }

            }

            public virtual void Unsubscribe()
            {
                unsubscriber.Dispose();
            }

            #endregion

            #region provider

            public override IDisposable Subscribe(IObserver<int> observer)
            {
                if (!observers.Contains(observer))
                    observers.Add(observer);
                return new Unsubscriber(observers, observer);
            }

            private class Unsubscriber : IDisposable
            {
                private IList<IObserver<int>> _observers;
                private IObserver<int> _observer;

                public Unsubscriber
                (IList<IObserver<int>> observers, IObserver<int> observer)
                {
                    _observers = observers;
                    _observer = observer;
                }

                public void Dispose()
                {
                    if (_observer != null && _observers.Contains(_observer))
                        _observers.Remove(_observer);
                }
            }

            #endregion
        }
    }
}