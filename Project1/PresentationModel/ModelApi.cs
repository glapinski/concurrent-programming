using System;
using System.Collections.Generic;
using Logic;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract List<BallModel> balls { get; }
        public abstract void CreateBalls(uint count);
        public static ModelAbstractApi CreateApi()
        {
            return new ModelApi();
        }
    }

    internal class ModelApi : ModelAbstractApi
    {
        public override List<BallModel> balls => ChangeBall();
        private LogicAbstractApi _logicApi;
        
        public ModelApi()
        {
            _logicApi = _logicApi ?? LogicAbstractApi.CreateApi();
        }
        public List<BallModel> ChangeBall()
        {
            List<BallModel> ballModels = new List<BallModel>();

            foreach (Ball b in _logicApi.GetBalls())
            {
                ballModels.Add(new BallModel(b));
            }
            return ballModels;
        }

        public override void CreateBalls(uint count)
        {
            _logicApi.createBalls(count);
            _logicApi.start();
        }

/*
        public override string BallsNumber 
        { 
            get => _ballsNumber; 
            set => _ballsNumber = value; 
        }
        public override bool BeginSimulationClicked 
        { 
            get => _beginSimulationClicked; 
            set => _beginSimulationClicked = value; 
        }

        protected override LogicAbstractApi Logic
        {
            get => _logic;
        }

        public override void CreateBalls(uint count)
        {
            throw new NotImplementedException();
        }

        public override List<BallModel> GetBalls()
        {
            return _balls;
        }
        internal ModelApi(uint width, uint height)
        {
            WindowWidth = width;
            WindowHeight = height;
            BallsNumber = "0";
            BeginSimulationClicked = false;

            _logic = LogicAbstractApi.CreateApi(width, height);
            _balls = new List<BallModel>();
        } */
    }
}
