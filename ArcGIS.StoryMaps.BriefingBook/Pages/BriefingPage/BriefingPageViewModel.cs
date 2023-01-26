using System;
using CommunityToolkit.Mvvm.ComponentModel;
using ArcGIS.StoryMaps.BriefingBook.Models;

namespace ArcGIS.StoryMaps.BriefingBook.ViewModels
{
    [QueryProperty(nameof(BriefingPageModel), "BriefingPageModel")]
    [QueryProperty(nameof(BriefingPageContentModel), "BriefingPageContentModel")]
    public partial class BriefingPageViewModel : ObservableObject
    {
        public BriefingPageViewModel()
        {
        }

        [ObservableProperty]
        public BriefingPageModel briefingPageModel;

        [ObservableProperty]
        public BriefingPageContentModel briefingPageContentModel;
    }
}

