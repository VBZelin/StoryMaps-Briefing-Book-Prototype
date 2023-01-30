using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ArcGIS.StoryMaps.BriefingBook.Assets;
using ArcGIS.StoryMaps.BriefingBook.Models;
using ArcGIS.StoryMaps.BriefingBook.Services;
using ArcGIS.StoryMaps.BriefingBook.Helpers;
using ArcGIS.StoryMaps.BriefingBook.Pages;
using Esri.ArcGISRuntime.Portal;

namespace ArcGIS.StoryMaps.BriefingBook.ViewModels
{
    public class PortalChooserPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Parameters
        /// </summary>
        public ArcGISPortal CurrentSecuredPortal { get; private set; }

        public string Message => GetMessage();

        public ICommand NextButtonClickedCommand { get; private set; }
        public ICommand DeleteClickedCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        private SQLiteDatabaseService _sqlDatabaseService;
        private ArcGISRuntimeService _arcGISRuntimeService;

        /// <summary>
        /// All data bindings
        /// </summary>
        private List<PortalInfoItem> _portalInfoItems;
        public List<PortalInfoItem> PortalInfoItems
        {
            get { return _portalInfoItems; }

            set
            {
                SetProperty(ref _portalInfoItems, value);
            }
        }

        private string _inputUrl = "";
        public string InputUrl
        {
            get { return _inputUrl; }

            set
            {
                if (value != _inputUrl)
                {
                    value = !string.IsNullOrWhiteSpace(value) ? value.ToLower() : "";

                    SetProperty(ref _inputUrl, value);
                    RaisedOnPropertyChanged(nameof(Message));
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
                    SetProperty(ref _isCheckingUrl, value);
                    RaisedOnPropertyChanged(nameof(Message));
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
                    SetProperty(ref _isValidUrl, value);
                    RaisedOnPropertyChanged(nameof(Message));

                    RefreshCanExecutes();
                }
            }
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }

            set
            {
                if (value != _isBusy)
                {
                    SetProperty(ref _isBusy, value);
                }
            }
        }

        public PortalChooserPageViewModel(SQLiteDatabaseService sqlDatabaseService, ArcGISRuntimeService arcGISRuntimeService)
        {
            _sqlDatabaseService = sqlDatabaseService;
            _arcGISRuntimeService = arcGISRuntimeService;

            NextButtonClickedCommand = new Command(
                execute: async () =>
                {
                    // Remove "/sharing/rest" before adding to the database
                    var portalUrl = CurrentSecuredPortal.Uri.ToString().Replace("/sharing/rest", "");

                    var portalInfoItem = new PortalInfoItem
                    {
                        Name = CurrentSecuredPortal.PortalInfo.PortalName,
                        Url = portalUrl,
                        UnixTime = DateTime.UtcNow,
                        Json = "{}"
                    };

                    await AddPortalInfoItem(portalInfoItem);

                    var pageParameters = new Dictionary<string, object>()
                    {
                        ["PortalUrl"] = portalUrl,
                        ["SignInType"] = SignInType.OAuth
                    };

                    await Shell.Current.GoToAsync($"{nameof(SignInPage)}", pageParameters);
                },
                canExecute: () =>
                {
                    return IsValidUrl;
                }
                );

            DeleteClickedCommand = new Command<PortalInfoItem>(async (portalInfoItem) => await DeletePortalInfoItem(portalInfoItem));

            RefreshCommand = new Command(async () =>
            {
                IsBusy = true;

                await FilterPortalInfoItems();

                IsBusy = false;
            });
        }

        public async Task AddPortalInfoItem(PortalInfoItem portalInfoItem)
        {
            await _sqlDatabaseService.AddPortalInfoItemAsync(portalInfoItem);

            await FilterPortalInfoItems();
        }

        public async Task DeletePortalInfoItem(PortalInfoItem portalInfoItem)
        {
            await _sqlDatabaseService.DeletePortalInfoItemAsync(portalInfoItem);

            await FilterPortalInfoItems();
        }

        public async Task FilterPortalInfoItems()
        {
            var portalInfoItems = await _sqlDatabaseService.GetPortalInfoItemsSortedByUnixTimeAsync(InputUrl);

            PortalInfoItems = portalInfoItems;
        }

        public async Task ValidateUrl()
        {
            IsCheckingUrl = true;

            CurrentSecuredPortal = null;

            IsValidUrl = false;

            var realUrl = Utility.GetRealPortalUrl(InputUrl);

            var isUrl = Utility.IsUrl(realUrl);

            if (isUrl)
            {
                var securedPortal = await _arcGISRuntimeService.ArcGISPortalManager.GetPortalIfUrlIsValid(realUrl);

                if (securedPortal is not null)
                {
                    CurrentSecuredPortal = securedPortal;

                    IsValidUrl = true;
                }
            }

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

