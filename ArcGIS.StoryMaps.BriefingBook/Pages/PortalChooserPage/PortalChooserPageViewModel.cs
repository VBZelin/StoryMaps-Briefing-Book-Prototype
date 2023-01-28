﻿using System;
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
        /// <summary>
        /// Parameters
        /// </summary>
        public Esri.ArcGISRuntime.Portal.ArcGISPortal CurrentSecuredPortal { get; private set; }

        public SignInType CurrentSignInType { get; private set; }

        public string Message => GetMessage();

        public ICommand NextButtonClickedCommand { get; private set; }

        private readonly IEnumerable<PortalInfo> _savedPortalInfos;

        private SQLiteDatabaseService _sqlDatabaseService;
        private ArcGISRuntimeService _arcGISRuntimeService;

        /// <summary>
        /// All data bindings
        /// </summary>
        private List<PortalInfo> _portalInfos;
        public List<PortalInfo> PortalInfos
        {
            get { return _portalInfos; }

            set
            {
                SetProperty(ref _portalInfos, value);
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

        public PortalChooserPageViewModel(SQLiteDatabaseService sqlDatabaseService, ArcGISRuntimeService arcGISRuntimeService)
        {
            _sqlDatabaseService = sqlDatabaseService;
            _arcGISRuntimeService = arcGISRuntimeService;

            NextButtonClickedCommand = new Command(
                execute: async () =>
                {
                    var securedPortalUrl = CurrentSecuredPortal.Uri.ToString();

                    var portalInfo = new PortalInfo
                    {
                        Name = CurrentSecuredPortal.PortalInfo.PortalName,
                        Url = securedPortalUrl,
                        UnixTime = DateTime.UtcNow,
                        Json = "{}"
                    };

                    await _sqlDatabaseService.AddPortalInfoAsync(portalInfo);

                    var pageParameters = new Dictionary<string, object>()
                    {
                        ["PortalUrl"] = securedPortalUrl,
                        ["SignInType"] = CurrentSignInType
                    };

                    await Shell.Current.GoToAsync($"/{nameof(SignInPage)}", pageParameters);
                },
                canExecute: () =>
                {
                    return IsValidUrl;
                }
                );
        }

        public async Task FilterPortalInfos()
        {
            var portalInfos = await _sqlDatabaseService.GetPortalInfosSortedByUnixTimeAsync(InputUrl);

            PortalInfos = portalInfos;
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
                var securedPortal = await _arcGISRuntimeService.GetPortalIfUrlIsValid(realUrl);

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

