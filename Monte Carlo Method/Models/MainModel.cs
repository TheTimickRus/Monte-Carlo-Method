using MonteCarloMethod.Properties;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MonteCarloMethod.Models
{
    public class MainModel : INotifyPropertyChanged
    {
        public MainModel()
        {
            Instanse = this;
        }

        public static MainModel Instanse { get; private set; }

        #region Properties

        private string _functions;

        public string Functions
        {
            get => _functions;
            set
            {
                _functions = value;
                OnPropertyChanged();
            }
        }

        private double _a;

        public double A
        {
            get => _a;
            set
            {
                _a = value;
                OnPropertyChanged();
            }
        }

        private double _b;

        public double B
        {
            get => _b;
            set
            {
                _b = value;
                OnPropertyChanged();
            }
        }

        private double _c;

        public double C
        {
            get => _c;
            set
            {
                _c = value;
                OnPropertyChanged();
            }
        }

        private double _d;

        public double D
        {
            get => _d;
            set
            {
                _d = value;
                OnPropertyChanged();
            }
        }

        private int _iterationCount;

        public int IterationCount
        {
            get => _iterationCount;
            set
            {
                _iterationCount = value;
                OnPropertyChanged();
            }
        }

        private double _exValue;
        public double ExValue
        {
            get => _exValue;
            set { _exValue = value; OnPropertyChanged(); }
        }

        private double _progress;

        public double Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        private string _answer;

        public string Answer
        {
            get => _answer;
            set
            {
                _answer = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}