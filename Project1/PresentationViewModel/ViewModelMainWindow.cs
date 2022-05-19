using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ViewModelMainWindow : INotifyPropertyChanged
    {
        private ModelAPI modelApi;
        public ObservableCollection<IBall> Balls { get; set; }

        public ICommand StartButtonClick { get; set; }
        private string inputText;

        private bool state;

        public bool State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                RaisePropertyChanged(nameof(State));
            }
        }


        public string InputText
        {
            get
            {
                return inputText;
            }
            set
            {
                inputText = value;
                RaisePropertyChanged(nameof(InputText));
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelMainWindow() : this(ModelAPI.CreateApi())
        {

        }

        public ViewModelMainWindow(ModelAPI baseModel)
        {
            State = true;
            this.modelApi = baseModel;
            StartButtonClick = new RelayCommand(() => StartButtonClickHandler());
            Balls = new ObservableCollection<IBall>();
            IDisposable observer = modelApi.Subscribe(x => Balls.Add(x));
        }

        private void StartButtonClickHandler()
        {
            modelApi.AddBallsAndStart(readFromTextBox());
        }

        public int readFromTextBox()
        {
            int number;
            if (Int32.TryParse(InputText, out number) && InputText != "0")
            {
                number = Int32.Parse(InputText);
                ErrorMessage = "";
                State = false;
                if (number > 10)
                {
                    return 10;
                }
                return number;
            }
            ErrorMessage = "Nieprawidłowa liczba";
            return 0;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}