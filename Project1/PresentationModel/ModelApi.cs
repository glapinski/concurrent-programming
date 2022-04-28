﻿using System;
using System.Collections.Generic;
using Logic;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract List<BallModel> balls { get; }
        public abstract void CreateBalls(int count);
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

        public override void CreateBalls(int count)
        {
            _logicApi.createBalls(count);
            _logicApi.start();
        }
    }
}
