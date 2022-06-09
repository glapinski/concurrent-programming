using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using Data;

namespace Logic
{
    public abstract class LogicAPI : IObserver<IBall>, IObservable<IBall>
    {
        public abstract void AddBallsAndStart(int BallsAmount);
        public abstract IDisposable Subscribe(IObserver<IBall> observer);
        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(IBall ball);


        public static LogicAPI CreateLayer(DataAbstractAPI data = default(DataAbstractAPI))
        {
            return new BusinessLogic(data == null ? DataAbstractAPI.CreateDataApi() : data);
        }

        public class BallChaneEventArgs : EventArgs
        {
            public IBall ball { get; set; }
        }

        private class BusinessLogic : LogicAPI, IObservable<IBall>
        {
            private readonly DataAbstractAPI dataAPI;
            private IDisposable unsubscriber;
            static object _lock = new object();
            private IObservable<EventPattern<BallChaneEventArgs>> eventObservable = null;
            public event EventHandler<BallChaneEventArgs> BallChanged;
            Dictionary<int, IBall> ballTree;
            Barrier barrier;

            public BusinessLogic(DataAbstractAPI dataAPI)
            {
                eventObservable = Observable.FromEventPattern<BallChaneEventArgs>(this, "BallChanged");
                this.dataAPI = dataAPI;
                Subscribe(dataAPI);
                ballTree = new Dictionary<int, IBall>();
            }
            public override void AddBallsAndStart(int BallsAmount)
            {
                dataAPI.createBalls(BallsAmount);
                barrier = new Barrier(BallsAmount);
            }

            #region observer

            public virtual void Subscribe(IObservable<IBall> provider)
            {
                if (provider != null)
                    unsubscriber = provider.Subscribe(this);
            }

            public override void OnNext(IBall ball)
            {
                Monitor.Enter(_lock);
                try
                {
                    if (!ballTree.ContainsKey(ball.Id))
                    {
                        ballTree.Add(ball.Id, ball);
                    }

                    foreach (var item in ballTree)
                    {
                        if (item.Key != ball.Id)
                        {
                            if (Collision.IsCollision(ball, item.Value))
                            {
                                Collision.ImpulseSpeed(ball, item.Value);
                            }
                        }
                    }

                    Collision.IsTouchingBoundaries(ball, dataAPI.getBoardSize());

                    BallChanged?.Invoke(this, new BallChaneEventArgs() { ball = ball });

                }
                catch (SynchronizationLockException exception)
                {
                    throw new Exception("Checking collision synchronization lock not working", exception);
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }

            public override void OnCompleted()
            {
                Unsubscribe();
            }

            public override void OnError(Exception error)
            {
                throw error;
            }

            public virtual void Unsubscribe()
            {
                unsubscriber.Dispose();
            }

            #endregion

            #region observable
            public override IDisposable Subscribe(IObserver<IBall> observer)
            {
                return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.ball), ex => observer.OnError(ex), () => observer.OnCompleted());
            }
            #endregion

        }
    }
}