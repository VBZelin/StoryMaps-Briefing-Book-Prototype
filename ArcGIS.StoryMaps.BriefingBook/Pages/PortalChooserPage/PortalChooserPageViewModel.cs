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
        // public
        public IEnumerable<PortalInfo> PortalInfos { get; private set; }

        public object CurrentSecuredPortal { get; private set; }

        public Uri CurrentPortalUrl { get; private set; }

        public SignInType CurrentSignInType { get; private set; }

        public string Message => GetMessage();

        public ICommand NextButtonClickedCommand { get; private set; }

        // private
        private readonly IEnumerable<PortalInfo> _savedPortalInfos;

        private SQLiteDatabaseService _sqlDatabaseService;
        private ArcGISRuntimeService _portalService;

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

        public PortalChooserPageViewModel(SQLiteDatabaseService sqlDatabaseService, ArcGISRuntimeService portalService)
        {
            _sqlDatabaseService = sqlDatabaseService;
            _portalService = portalService;

            NextButtonClickedCommand = new Command(
                execute: async () =>
                {
                    var pageParameters = new Dictionary<string, object>()
                    {
                        ["PortalUrl"] = CurrentPortalUrl,
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
            CurrentPortalUrl = null;

            IsValidUrl = false;

            var realUrl = Utility.GetRealPortalUrl(InputUrl);

            var isUrl = Utility.IsUrl(realUrl);

            if (isUrl)
            {
                var securedPortal = await _portalService.GetPortalIfUrlIsValid(realUrl);

                if (securedPortal is not null)
                {
                    CurrentSecuredPortal = securedPortal;
                    CurrentPortalUrl = securedPortal.Uri;

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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

