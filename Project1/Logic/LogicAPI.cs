using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using Data;

namespace Logic
{
    public abstract class LogicAbstractApi : IObserver<int>, IObservable<int>
    {
        public abstract void createBallsAndStart(int numberOfBalls);
        public abstract double getBallX(int ballId);
        public abstract double getBallY(int ballId);
        public abstract double getBallRadius(int ballId);

        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(int value);
        
        public abstract IDisposable Subscribe(IObserver<int> observer);
        public static LogicAbstractApi CreateApi(DataAbstractAPI data = default(DataAbstractAPI))
        {
            return new LogicApi(data == null ? DataAbstractAPI.CreateAPI() : data);
        }

        public class BallChangeEventArgs : EventArgs
        {
            public int ballId { get; set; }
        }
    }

    internal class LogicApi : LogicAbstractApi, IObservable<int>
    {
        private readonly DataAbstractAPI dataAPI;
        private IDisposable unsubscriber;
        static object _lock = new object();
        private IObservable<EventPattern<BallChangeEventArgs>> eventObservable = null;
        public event EventHandler<BallChangeEventArgs> BallChanged;
        public LogicApi(DataAbstractAPI dataAPI)
        {
            eventObservable = eventObservable.FromEventPattern<BallChangeEventArgs>(this, "BallChanged");
            this.dataAPI = dataAPI;
            Subscribe(dataAPI);
        }
        public override double getBallX(int ballId)
        {
            return this.dataAPI.getBallPositionX(ballId);
        }

        public override double getBallY(int ballId)
        {
            return this.dataAPI.getBallPositionY(ballId);
        }

        public override double getBallRadius(int ballId)
        {
            return this.dataAPI.getBallRadius(ballId);
        }

        public override void createBallsAndStart(int numberOfBalls)
        {
            dataAPI.createBalls(numberOfBalls);
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
            /*       Monitor.Enter(_lock);
              try
              {
                  new System.Threading.Thread(() =>
                  {*/
            System.Diagnostics.Debug.WriteLine("Collision check: Ball: " + value);

            for (int i = 1; i <= 20; i++)
            {
                if (value != i)
                {
                    if (Collision.IsCollision(dataAPI.getBallPositionX(value), dataAPI.getBallPositionY(value), dataAPI.getBallPositionX(i), dataAPI.getBallPositionY(i), dataAPI.getBallRadius(value), dataAPI.getBallRadius(i)))
                    {
                        //double temp = dataAPI.getBallSpeed(value);
                        //dataAPI.setBallSpeed(value, -dataAPI.getBallSpeed(i));
                        //dataAPI.setBallSpeed(i, -temp);
                        dataAPI.setBallXSpeed(value, -dataAPI.getBallXSpeed(i));
                        dataAPI.setBallYSpeed(value, -dataAPI.getBallYSpeed(i));
                        dataAPI.setBallXSpeed(i, -dataAPI.getBallXSpeed(value));
                        dataAPI.setBallYSpeed(i, -dataAPI.getBallYSpeed(value));
                    }
                }
            }
            /*       }).Start();
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
               }*/



        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        #endregion
    }
}
