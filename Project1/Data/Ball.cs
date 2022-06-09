using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class Ball : IObservable<int>
    {
        public int Id { get; }

        public double PositionX { get; private set; }
        public double PositionY { get; private set; }

        public int Radius { get; } = 15;
        public double Mass { get; } = 10;

        public double MoveX { get; set; }
        public double MoveY { get; set; }

        internal readonly IList<IObserver<int>> observers;

        private Task BallThread;

        public Ball(int id)
        {
            this.Id = id;

            Random random = new Random();

            observers = new List<IObserver<int>>();

            this.PositionX = Convert.ToDouble(random.Next(1, 500));
            this.PositionY = Convert.ToDouble(random.Next(1, 500));

            this.MoveX = random.NextDouble() * (3 - 2) + 2;
            this.MoveY = random.NextDouble() * (3 - 2) + 2;
        }

        public void StartMoving()
        {
            this.BallThread = new Task(MoveBall);
            BallThread.Start();
        }

        public void MoveBall()
        {
            while (true)
            {
                ChangeBallPosition();

                foreach (var observer in observers.ToList())
                {
                    if (observer != null)
                    {
                        observer.OnNext(Id);
                    }
                }
                System.Threading.Thread.Sleep(1);
            }
        }

        public void ChangeBallPosition()
        {
            PositionX += MoveX;
            PositionY += MoveY;
        }

        #region provider

        public IDisposable Subscribe(IObserver<int> observer)
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
