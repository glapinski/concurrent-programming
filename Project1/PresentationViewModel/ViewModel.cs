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
        private ModelAbstractApi _modelAbstractApi;

        public ICommand StartButtonClicked { get; set; }

        public List<BallAbstract> Balls
        {
            get => _modelAbstractApi.GetBalls();

        }
    }
}
