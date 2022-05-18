using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : IObservable<int>
    {
        public int id { get; }
        public double x { get; set; }
        public double y { get; set; }
        public double xS { get; set; }
        public double yS { get; set; }
        public int r { get; } = 10;
        public double m { get; } = 10;
        private Task BallUpdater;
        internal readonly IList<IObserver<int>> observers;
        public int regionSize { get; set; } = 479;

        Random rng = new Random();
        public double generateRandomDouble(double min, double max)
        {
            return rng.NextDouble() * (max - min) + min;
        }

        public Ball(int id)
        {
            this.id = id;

            Random rng = new Random();
            observers = new List<IObserver<int>>();

            this.x = Convert.ToDouble(rng.Next(1, 500));
            this.y = Convert.ToDouble(rng.Next(1, 500));

            this.xS = rng.NextDouble() * (5 - 2) + 2;
            this.yS = rng.NextDouble() * (5 - 2) + 2;
        }

        public void MoveBall()
        {
            while(true)
            {
                /*double x2 = x + xS;
                double y2 = y + yS;

                if (x2 > regionSize-10 || x2 < 0)
                {
                    xS = -xS;
                }
                if (y2 > regionSize-10 || y2 < 0)
                {
                    yS = -yS;
                }

                x = x2;
                y = y2;*/
                ChangeBallPosition();

                foreach(var observer in observers.ToList())
                {
                    if (observer != null)
                    {
                        observer.OnNext(id);
                    }
                }
                System.Threading.Thread.Sleep(1);

                 /*double newX = x + xS;
                double newY = y + yS;

                if (newX > regionSize || newX < 0)
                {
                    xS = -xS;
                }

                if (newY > regionSize || newY < 0)
                {
                   yS = -yS;
                }

                x += xS;
                y += yS;

                //Inform observers when position change
                //double[] position = { PositionX, PositionY };
                //Point position = new Point(PositionX, PositionY);
                int threadId = Thread.CurrentThread.ManagedThreadId;

                //if (observers != null)
                //{
                foreach (var observer in observers.ToList())
                {
                    //if (position is null) 
                    //observer.OnError(new NullReferenceException("Position is incorrect"));
                    //else
                    if (observer != null)
                    {
                        System.Diagnostics.Debug.WriteLine("Ball: " + id + " moved on thread: " + threadId);
                        observer.OnNext(id);
                    }

                }
                //}


                System.Threading.Thread.Sleep(1);*/
            }
        }

        public void StartMove()
        {
            this.BallUpdater = new Task(MoveBall);
            BallUpdater.Start();
        }

        public void ChangeBallPosition()
        {
            x += xS;
            y += yS;
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

    /*public struct Point
    {
        public double X;
        public double Y;

        public Point(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }*/
}
