using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArcGIS.StoryMaps.BriefingBook.Assets;
using ArcGIS.StoryMaps.BriefingBook.Services;

namespace ArcGIS.StoryMaps.BriefingBook.ViewModels
{
    public class SignInPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// All data bindings
        /// </summary>
        private SignInType _signInType = SignInType.Unknown;
        public SignInType SignInType
        {
            get { return _signInType; }

            set
            {
                SetProperty(ref _signInType, value);
            }
        }

        /// <summary>
        /// Parameters
        /// </summary>
        private ArcGISRuntimeService _arcGISRuntimeService;

        public SignInPageViewModel(ArcGISRuntimeService arcGISRuntimeService)
        {
            _arcGISRuntimeService = arcGISRuntimeService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(property, value))
            {
                return false;
            }

            property = value;

            this.RaisedOnPropertyChanged(propertyName);

            return true;
        }

        private void RaisedOnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

