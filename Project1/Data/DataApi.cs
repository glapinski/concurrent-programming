using System;
using System.Collections.Generic;

namespace Data
{
    public abstract class DataAbstractAPI : IObserver<int>, IObservable<int>
    {
        public abstract double getBallPositionX(int ballId);
        public abstract double getBallPositionY(int ballId);
        public abstract double getBallRadious(int ballId);
        public abstract double getBallXSpeed(int ballId);
        public abstract double getBallYSpeed(int ballId);
        public abstract void setBallXSpeed(int ballId, double xSpeed);
        public abstract void setBallYSpeed(int ballId, double ySpeed);
        public abstract void createBalls(int ballsAmount);
        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(int value);

        public abstract IDisposable Subscribe(IObserver<int> observer);
        public static DataAbstractAPI CreateAPI()
        { 
            return new DataAPI(); 
        }
    }
    internal class DataAPI : DataAbstractAPI
    {
        private BallRepository ballRepository;
        private IDisposable unsubscriber;

        private IList<IObserver<int>> observers;
        public DataAPI()
        {
            this.ballRepository = new BallRepository();
            observers = new List<IObserver<int>>();
        }
        public override void createBalls(int ballsAmount)
        {
            ballRepository.CreateBalls(ballsAmount);

            foreach (var ball in ballRepository.balls)
            {
                Subscribe(ball);
            }
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
        public override void setBallXSpeed(int ballId, double xSpeed)
        {
            this.ballRepository.getBall(ballId).xS = xSpeed;
        }
        public override void setBallYSpeed(int ballId, double ySpeed)
        {
            this.ballRepository.getBall(ballId).yS = ySpeed;
        }

        #region observer

        public virtual void Subscribe(IObservable<int> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public override void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public override void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public override void OnNext(int value)
        {
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
