using System;
using System.Collections.Generic;
using Logic;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        protected abstract LogicAbstractApi Logic { get; }
        public abstract List<BallAbstract> GetBalls();
        public abstract void CreateBalls(uint count);
        public abstract uint WindowWidth { get; set; }
        public abstract uint WindowHeight { get; set; }
        public abstract string BallsNumber { get; set; }
        public abstract bool BeginSimulationClicked { get; set; }
        public static ModelAbstractApi CreateApi(uint width, uint height)
        {
            return new ModelApi(width, height);
        }
    }

    internal class ModelApi : ModelAbstractApi
    {
        private List<BallAbstract> _balls;
        private LogicAbstractApi _logic;
        private uint _windowWidth;
        private uint _windowHeight;
        private string _ballsNumber;
        private bool _beginSimulationClicked;
        public override uint WindowWidth 
        { 
            get => _windowWidth;
            set => _windowWidth = value;
        }
        public override uint WindowHeight 
        { 
            get => _windowHeight;
            set => _windowHeight = value;
        }
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

        public override List<BallAbstract> GetBalls()
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
            _balls = new List<BallAbstract>();
        }
    }
}
