using System;
using System.Collections.Generic;
using System.Threading;

namespace Data
{
    public abstract class DataAbstractAPI : IObserver<IBall>, IObservable<IBall>
    {
        public abstract int getBoardSize();
        public abstract void createBalls(int ballsAmount);
        public abstract int getBallsAmount();

        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(IBall Ball);

        public abstract IDisposable Subscribe(IObserver<IBall> observer);

        public static DataAbstractAPI CreateDataApi()
        {
            return new DataApi();
        }

        public class BallChaneEventArgs : EventArgs
        {
            public IBall newBall { get; set; }
        }

        private class DataApi : DataAbstractAPI
        {
            private BallRepository ballRepository;
            private IDisposable unsubscriber;
            private IList<IObserver<IBall>> observers;

            public DataApi()
            {
                this.ballRepository = new BallRepository();

                observers = new List<IObserver<IBall>>();
            }

            public override int getBoardSize()
            {
                return ballRepository.BoardSize;
            }
            public override int getBallsAmount()
            {
                return ballRepository.balls.Count;
            }

            public override void createBalls(int ballsAmount)
            {
                ballRepository.CreateBalls(ballsAmount);

                foreach (var ball in ballRepository.balls)
                {
                    Subscribe(ball);
                    ball.StartMoving();
                }

            }

            #region observer

            public virtual void Subscribe(IObservable<IBall> provider)
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

            public override void OnNext(IBall ball)
            {
                foreach (var observer in observers)
                {
                    observer.OnNext(ball);
                }

            }

            public virtual void Unsubscribe()
            {
                unsubscriber.Dispose();
            }

            #endregion

            #region provider

            public override IDisposable Subscribe(IObserver<IBall> observer)
            {
                if (!observers.Contains(observer))
                    observers.Add(observer);
                return new Unsubscriber(observers, observer);
            }

            private class Unsubscriber : IDisposable
            {
                private IList<IObserver<IBall>> _observers;
                private IObserver<IBall> _observer;

                public Unsubscriber
                (IList<IObserver<IBall>> observers, IObserver<IBall> observer)
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