using System;
using System.Collections.Generic;
using Data;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract void createBalls(uint count);
        public abstract List<Ball> GetBalls();

        public abstract void start();
        public static LogicAbstractApi CreateApi(DataAbstractAPI data = default(DataAbstractAPI))
        {
            return new LogicApi(data == null ? DataAbstractAPI.CreateAPI() : data);
        }

    }

    internal class LogicApi : LogicAbstractApi
    {
        
        private DataAbstractAPI _dataAPI;
        private Task _changePosition;
        private Region _region;

        public LogicApi(DataAbstractAPI dataAPI)
        {
            _dataAPI = dataAPI;
            _region = new Region(500);
        }

        public override void createBalls(uint count)
        {
            _region.addBalls(count);         
        }

        public override List<Ball> GetBalls()
        {
            return _region.balls;
        }

        public override void start()
        {
            if (_region.balls.Count > 0)
            {
                _changePosition = Task.Run(_region.MoveBalls);
            }
        }
    }
}
