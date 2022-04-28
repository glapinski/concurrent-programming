using System;
using System.Collections.Generic;
using System.Windows.Input;

using Prism.Mvvm;
using Model;
using System.Collections.ObjectModel;

namespace Presentation.ViewModel
{
    public class ViewModel : BindableBase
    {

        public ViewModel()
        {
            StartButtonClicked = new RelayCommand(Execute, CanExecute);

            uint windowWidth = 300;
            uint windowHeight = 300;

            _modelAbstractApi = ModelAbstractApi.CreateApi(windowWidth, windowHeight);
        }
        private ModelAbstractApi _modelAbstractApi;

        public ICommand StartButtonClicked { get; set; }

        public List<BallModel> Balls
        {
            get => _modelAbstractApi.GetBalls();

        }

        public uint WindowWidth
        {
            get => _modelAbstractApi.WindowWidth;
            set => _modelAbstractApi.WindowWidth = value;
        }

        public uint WindowHeight
        {
            get => _modelAbstractApi.WindowHeight;
            set => _modelAbstractApi.WindowHeight = value;
        }

        public bool StartClicked
        {
            get { return _modelAbstractApi.BeginSimulationClicked; }
            set { _modelAbstractApi.BeginSimulationClicked = value; }
        }

        public string BallsNumber
        {
            get { return _modelAbstractApi.BallsNumber; }
            set { _modelAbstractApi.BallsNumber = value; }
        }

        private void Execute(object value)
        {
            uint ballsNumber;
            if(uint.TryParse(BallsNumber, out ballsNumber))
            {
                StartClicked = true;
                _modelAbstractApi.CreateBalls(ballsNumber);
            }
        }

        private bool CanExecute(object value)
        {
            return !StartClicked;
        }

    }
}
