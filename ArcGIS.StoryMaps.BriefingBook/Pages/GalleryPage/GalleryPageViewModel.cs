using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Esri.ArcGISRuntime.Portal;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
   public partial class GalleryPageViewModel:ObservableObject
    {
        int _queryStartIndex = 0;

        [ObservableProperty]
        ObservableCollection<GalleryItem> publicPortalItems;

        public ArcGISPortal portal;      

        [RelayCommand]
        public async void SearchPublicItems()
        {
            string queryExpression = $" type: (\"StoryMap\" NOT typekeywords:\"storymapcollection\")";
            PublicPortalItems = await SearchPortal(portal,queryExpression);
        }

        [RelayCommand]
        private  void OpenStoryMap(GalleryItem item)
        {
            PortalItem _webmapItem = item.StoryMapPortalItem;
            

        }

        /// <summary>
        /// Searches the ArcGISPortal for portalItems satisfying the query expression.
        /// </summary>
        /// <param name="portal"></param>
        /// <param name="queryExpression"></param>
        /// <returns></returns>
        public async Task<ObservableCollection<GalleryItem>>  SearchPortal(ArcGISPortal portal,string queryExpression)
        {
            ObservableCollection<GalleryItem> portalItems = new ObservableCollection<GalleryItem>();
            // Get web map portal items from a keyword search
            List<PortalItem> storyMapItems;
           // string queryExpression = $" type: (\"StoryMap\" NOT typekeywords:\"storymapcollection\")";


            // Create a query parameters object with the expression and a limit of 10 results
            PortalQueryParameters queryParams = new PortalQueryParameters(queryExpression, 10)
            {
                CanSearchPublic = true,
                StartIndex = _queryStartIndex,
                SortField = "title",
                SortOrder = PortalQuerySortOrder.Ascending
            };

            // Search the portal using the query parameters and await the results
            PortalQueryResultSet<PortalItem> findResult = await portal.FindItemsAsync(queryParams);

            // Get the items from the query results
            storyMapItems = findResult.Results.ToList();
            foreach (PortalItem item in storyMapItems)
            {
               GalleryItem _galleryItem = new GalleryItem()
                {
                    StoryMapPortalItem = item,
                    Title = item.Title,
                    Owner = item.Owner,
                    Thumbnail = item.Thumbnail.Source.ToString()
                };
               
                portalItems.Add(_galleryItem);
               
            }

            return portalItems;

        }

    }
}
