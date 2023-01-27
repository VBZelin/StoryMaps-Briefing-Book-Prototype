using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ArcGIS.StoryMaps.BriefingBook.Assets;
using ArcGIS.StoryMaps.BriefingBook.Models;
using ArcGIS.StoryMaps.BriefingBook.Services;
using ArcGIS.StoryMaps.BriefingBook.Helpers;
using ArcGIS.StoryMaps.BriefingBook.Pages;

namespace ArcGIS.StoryMaps.BriefingBook.ViewModels
{
    public class PortalChooserPageViewModel : INotifyPropertyChanged
    {
        // public
        public IEnumerable<PortalInfo> PortalInfos { get; set; }

        public string Message => GetMessage();

        public ICommand NextButtonClickedCommand { private set; get; }

        // private
        private readonly IEnumerable<PortalInfo> _savedPortalInfos;

        private SQLiteDatabaseServer _sqlDatabaseServer;

        private string _inputUrl = "";

        public string InputUrl
        {
            get { return _inputUrl; }

            set
            {
                if (value != _inputUrl)
                {
                    _inputUrl = !string.IsNullOrWhiteSpace(value) ? value.ToLower() : "";

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        private bool _isValidUrl = false;

        public bool IsValidUrl
        {
            get { return _isValidUrl; }

            set
            {
                if (value != _isValidUrl)
                {
                    _isValidUrl = value;

                    RefreshCanExecutes();

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        private bool _isCheckingUrl = false;

        public bool IsCheckingUrl
        {
            get { return _isCheckingUrl; }

            set
            {
                if (value != _isCheckingUrl)
                {
                    _isCheckingUrl = value;

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        public PortalChooserPageViewModel(SQLiteDatabaseServer sqlDatabaseServer)
        {
            _sqlDatabaseServer = sqlDatabaseServer;

            NextButtonClickedCommand = new Command(
                execute: async () =>
                {
                    await Shell.Current.GoToAsync($"/{nameof(SignInPage)}");
                },
                canExecute: () =>
                {
                    return IsValidUrl;
                }
                );
        }

        public async Task FilterPortalInfos()
        {
            var portalInfos = await _sqlDatabaseServer.GetPortalInfosSortedByUnixTimeAsync(InputUrl);

            PortalInfos = portalInfos;
        }

        public async Task ValidateUrl()
        {
            IsCheckingUrl = true;

            var realUrl = Utility.GetRealPortalUrl(InputUrl);

            IsValidUrl = Utility.IsValidUrl(realUrl);

            await Task.Delay(200);

            IsCheckingUrl = false;
        }

        private string GetMessage()
        {
            if (string.IsNullOrEmpty(InputUrl))
                return StringSources.EXAMPLE_PORTAL;

            if (IsCheckingUrl)
                return StringSources.CHECKING_URL;

            if (IsValidUrl)
                return StringSources.VALID_URL;

            return StringSources.INVALID_URL;
        }

        private void RefreshCanExecutes()
        {
            (NextButtonClickedCommand as Command).ChangeCanExecute();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

