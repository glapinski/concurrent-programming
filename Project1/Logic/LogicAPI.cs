using System;
using System.Collections.Generic;
using Data;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractApi : IObserver<int>
    {
        public abstract void createBalls(int numberOfBalls);
        public abstract void start();
        public abstract double getBallX(int ballId);
        public abstract double getBallY(int ballId);
        public abstract double getBallRadius(int ballId);

        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(int value);
        public static LogicAbstractApi CreateApi(DataAbstractAPI data = default(DataAbstractAPI))
        {
            return new LogicApi(data == null ? DataAbstractAPI.CreateAPI() : data);
        }

    }

    internal class LogicApi : LogicAbstractApi
    {
        private readonly DataAbstractAPI dataAPI;
        private IDisposable unsubscriber;
        static object _lock = new object();
        //private CollisionControler collisionControler;
        public LogicApi(DataAbstractAPI dataAPI)
        {
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

        public override void createBalls(int numberOfBalls)
        {
            dataAPI.createBalls(numberOfBalls);        
        }

        public override void start()
        {
            dataAPI.createBalls(20);
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
