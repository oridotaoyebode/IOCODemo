using System.Collections.Generic;

namespace IOCO.Xamarin.ViewModels.Base
{
    public abstract class BaseListViewModelBase<T> : ViewModelBase where T: class
    {

        private int _rating;

        public int Rating
        {
            get => _rating;
            set => this.SetProperty(ref _rating, value);
        }

        private Dictionary<string, string> _customValues;

        public Dictionary<string, string> CustomValues
        {
            get => _customValues;
            set => this.SetProperty(ref _customValues, value);
        }

        private bool _showButton;
        public bool ShowButton
        {
            get => _showButton;
            set => this.SetProperty(ref _showButton, value);
        }

        private T _detail;

        public T Detail
        {
            get => _detail;
            set => SetProperty(ref _detail, value);
        }
    }
}
