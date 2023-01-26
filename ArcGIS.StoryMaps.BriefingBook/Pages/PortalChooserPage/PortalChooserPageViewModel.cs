﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Android.App.AppSearch;
using ArcGIS.StoryMaps.BriefingBook.Assets;
using ArcGIS.StoryMaps.BriefingBook.Models;

namespace ArcGIS.StoryMaps.BriefingBook.ViewModels
{
    public class PortalChooserPageViewModel : INotifyPropertyChanged
    {
        // public 
        public IEnumerable<PortalInfo> PortalItems => _savedPortalInfos.Where(SearchFunction);

        public string Message => GetMessage();

        // private
        private readonly IEnumerable<PortalInfo> _savedPortalInfos;

        private bool _isCheckingUrl = false;
        private bool _isValidUrl = false;

        private string _inputUrl = "";

        public string InputUrl
        {
            get { return _inputUrl; }

            set
            {
                if (value != _inputUrl)
                {
                    _inputUrl = !string.IsNullOrWhiteSpace(value) ? value.ToLower() : "";

                    _isCheckingUrl = false;
                    _isValidUrl = false;

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Message));
                    OnPropertyChanged(nameof(PortalItems));
                }
            }
        }

        public PortalChooserPageViewModel()
        {
        }

        public bool SearchFunction(PortalInfo portalInfo)
        {
            return portalInfo.Name.ToLower().Contains(InputUrl) || portalInfo.Url.ToLower().Contains(InputUrl);
        }

        private string GetMessage()
        {
            if (string.IsNullOrEmpty(InputUrl))
                return StringSources.EXAMPLE_PORTAL;

            if (_isCheckingUrl)
                return StringSources.CHECKING_URL;

            if (_isValidUrl)
                return StringSources.VALID_URL;

            return StringSources.INVALID_URL;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

