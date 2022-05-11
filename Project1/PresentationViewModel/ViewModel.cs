using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using Prism.Mvvm;
using Model;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ModelAbstractApi _modelApi;
        public ObservableCollection<BallModel> balls { get; set; }
        public ICommand StartButtonClicked { get; set; }
        private string _inputText;
        private Task _task;

        private bool active;
        private bool notActive = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel() : this(ModelAbstractApi.CreateApi())
        {
           
        }

        public ViewModel(ModelAbstractApi baseModel)
        {
            Active = true;
            this._modelApi = baseModel;
            StartButtonClicked = new RelayCommand(() => StartButtonClickHandler());
            balls = new ObservableCollection<BallModel>();
        }
        
        private void StartButtonClickHandler()
        {
            _modelApi.createBallsAndStart(readFromBox());
            _task = new Task(ChangePosition);
            _task.Start();
        }

        public void ChangePosition()
        {
            while (true)
            {
                ObservableCollection<BallModel> ballsList = new ObservableCollection<BallModel>();

                foreach (BallModel ball in _modelApi.balls)
                {
                    ballsList.Add(ball);
                }
                balls = ballsList;
                RaisePropertyChanged(nameof(balls));
                Thread.Sleep(10);
            }
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int readFromBox()
        {
            int result;
            if(Int32.TryParse(InputText,out result) && InputText != "0")
            {
                result = Int32.Parse(InputText);
                ErrorMessage = " ";
                Active = !Active;
                NotActive = !NotActive;
                return result;
            }
            ErrorMessage = "Zła liczba!";
            return 0;
        }

        public bool NotActive
        {
            get
            {
                return notActive;
            }
            set
            {
                notActive = value;
                RaisePropertyChanged(nameof(NotActive));
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                RaisePropertyChanged(nameof(Active));
            }
        }

        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                RaisePropertyChanged(nameof(InputText));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }
    }
}
