using System;
using System.Collections.Generic;
using System.Threading;

namespace Data
{
    public abstract class DataAbstractAPI : IObserver<int>, IObservable<int>
    {
        public abstract double getBallPositionX(int ballId);
        public abstract double getBallPositionY(int ballId);
        public abstract double getBallRadius(int ballId);

        public abstract double getBallMass(int ballId);
        public abstract double getBallXSpeed(int ballId);
        public abstract double getBallYSpeed(int ballId);
        public abstract void setBallSpeed(int ballId, double xSpeed, double ySpeed);
        public abstract void createBalls(int ballsAmount);
        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(int value);

        public abstract int getBoardSize();

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
        static object _lock = new object();
        private Barrier barrier;
        public DataAPI()
        {
            this.ballRepository = new BallRepository();
            observers = new List<IObserver<int>>();
        }
        public override void createBalls(int ballsAmount)
        {
            barrier = new Barrier(ballsAmount);
            ballRepository.CreateBalls(ballsAmount);

            foreach (var ball in ballRepository.balls)
            {
                Subscribe(ball);
                ball.StartMove();
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

        public override double getBallRadius(int ballId)
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
        public override void setBallSpeed(int ballId, double xSpeed, double ySpeed)
        {
            this.ballRepository.getBall(ballId).xS = xSpeed;
            this.ballRepository.getBall(ballId).yS = ySpeed;
        }

        public override int getBoardSize()
        {
            return ballRepository.BoardSize;
        }

        public override double getBallMass(int ballId)
        {
            return this.ballRepository.getBall(ballId).m;
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

        public override void OnNext(int ballId)
        {
            Monitor.Enter(_lock);
            try
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                System.Diagnostics.Debug.WriteLine("Observer: Ball: " + ballId + " moved on thread: " + threadId);

                foreach (var observer in observers)
                {
                    observer.OnNext(ballId);
                }
                // Critical piece of code

                /*     int threadId = Thread.CurrentThread.ManagedThreadId;
                     Console.WriteLine($" Thread: {threadId} Entered into the critical section ");
                     for (int num = 1; num <= 3; num++)
                     {
                         Console.WriteLine($" num: {num}");
                         //Pausing the thread execution for 2 seconds
                         //Thread.Sleep(TimeSpan.FromSeconds(2));
                     }*/
            }

            catch (SynchronizationLockException exception)
            {
                Console.WriteLine(exception.Message);
            }

            finally
            {
                // Releasing object
                Monitor.Exit(_lock);
                //Console.WriteLine($"Thread : {Thread.CurrentThread.ManagedThreadId} Released");
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
