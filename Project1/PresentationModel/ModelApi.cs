using System;
using System.Collections.Generic;
using Logic;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract List<BallModel> balls { get; }
        public abstract void createBallsAndStart(int ballsAmount);
        public static ModelAbstractApi CreateApi()
        {
            return new ModelApi();
        }
    }

    internal class ModelApi : ModelAbstractApi
    {
        public override List<BallModel> balls => ChangeBallForModel();
        private LogicAbstractApi _logicApi;
        
        public ModelApi()
        {
            _logicApi = _logicApi ?? LogicAbstractApi.CreateApi();
        }

        public override void createBallsAndStart(int ballsAmount)
        {
            _logicApi.createBalls(ballsAmount);
            _logicApi.start();
        }
        public List<BallModel> ChangeBallForModel()
        {
            List<BallModel> result = new List<BallModel>();

            for (int i = 0; i < 20; i++)
            {
                result.Add(new BallModel(_logicApi.getBallX(i + 1), _logicApi.getBallY(i + 1)));
            }
            return result;
        }
    }
}
