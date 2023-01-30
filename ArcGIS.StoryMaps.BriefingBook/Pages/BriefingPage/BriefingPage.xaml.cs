using ArcGIS.StoryMaps.BriefingBook.Assets;
using ArcGIS.StoryMaps.BriefingBook.ViewModels;
using ArcGIS.StoryMaps.BriefingBook.Models;
using ArcGIS.StoryMaps.BriefingBook.Views;


namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    public partial class BriefingPage : ContentPage
    {
        private BriefingPageViewModel _viewModel;
        private BriefingPageContentModel _contentModel;
        private BriefingPageModel _pageModel;

        public BriefingPage(BriefingPageViewModel briefingPageViewModel)
        {
            InitializeComponent();

            _viewModel = briefingPageViewModel;

            BindingContext = briefingPageViewModel;
        }

        /// <summary>
        /// After BindingContext has been initialized, access it
        /// </summary>
        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            InitializePageLayout();
        }

        /// <summary>
        /// This method takes the received binding context to create the 
        /// briefing page layout structure.
        /// </summary>
        private void InitializePageLayout()
        {
            _contentModel = _viewModel.BriefingPageContentModel;
            _pageModel = _viewModel.BriefingPageModel;

            CreateClassificationBannerSpace(true);

            CreatePageHeader();

            CreateAndAddColumnDefinitions();

            CreateAndAddRowDefinitions();

            CreateLeftColmnBlocks();

            CreateRightColumnBlocks();

            CreateNavigationBar();

            CreateClassificationBannerSpace(false);
        }

        /// <summary>
        /// This method creates and adds the top or bottom classification banner
        /// </summary>
        private void CreateClassificationBannerSpace(bool forTopBannerOtherwiseBottom)
        {
            //If the verticalsplit is less than 1, then there are 2 rows, otherwise 1 row
            int totalRows = (_contentModel.RowSplitPercentage < 100 ? 2 : 1) + 3;

            RowDefinition rowDefinition = new()
            {
                Height = new GridLength(25)
            };

            BriefingPageContent.RowDefinitions.Add(rowDefinition);

            BriefingPageContent.Add(
                    view: CreateClassificationBanner(
                        columnSpan: 2,
                        rowSpan: 1,
                        classificationType: _pageModel.ClassificationType
                        ),
                    column: 0,
                    row: forTopBannerOtherwiseBottom ? 0 : totalRows
                );
        }

        /// <summary>
        /// This method creates and returns the page header
        /// </summary>
        private static Frame CreateClassificationBanner(int columnSpan, int rowSpan, ClassificationType classificationType)
        {
            Frame frame = new()
            {
                BackgroundColor = GetClassificationColor(classificationType),
                Margin = 0,
                Padding = 0,
                CornerRadius = 0,
                Content = new Label
                {
                    Text = classificationType.ToString(),
                    FontSize = 10,
                    LineHeight = 25,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Padding = 0,
                }
            };

            Grid.SetColumnSpan(frame, columnSpan);
            Grid.SetRowSpan(frame, rowSpan);

            return frame;
        }

        /// <summary>
        /// This method creates and adds the page header
        /// </summary>
        private void CreatePageHeader()
        {
            RowDefinition rowDefinition = new()
            {
                Height = new GridLength(50)
            };

            BriefingPageContent.RowDefinitions.Add(rowDefinition);

            BriefingPageContent.Add(
                    view: CreateHeaderFrame(
                        columnSpan: 2,
                        rowSpan: 1,
                        header: _pageModel.PageTitle
                        ),
                    column: 0,
                    row: 1
                );
        }

        /// <summary>
        /// This method creates and returns the page header
        /// </summary>
        private static Frame CreateHeaderFrame(int columnSpan, int rowSpan, string header)
        {
            Frame frame = new()
            {
                Margin = 0,
                Padding = new Thickness(10, 0, 10, 0),
                CornerRadius = 0,
                Content = new Label
                {
                    Text = header,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center,
                }
            };

            Grid.SetColumnSpan(frame, columnSpan);
            Grid.SetRowSpan(frame, rowSpan);

            return frame;
        }

        /// <summary>
        /// This method creates and returns the page header
        /// </summary>
        private static Color GetClassificationColor(ClassificationType classificationType)
        {
            switch (classificationType)
            {
                case ClassificationType.Unknown:
                    return Colors.Yellow;
                case ClassificationType.Classified:
                    return Colors.Red;
                case ClassificationType.Unclassified:
                    return Colors.LightGreen;
                default:
                    return Colors.Yellow;
            }
        }

        /// <summary>
        /// This method creates the column width split by percentage (50/50, 60/40, 40/60)
        /// </summary>
        private void CreateAndAddColumnDefinitions()
        {
            //If the horizontalsplit is less than 1, then there are 2 columns, otherwise 1 column
            int totalColumns = _contentModel.ColumnSplitPercentage < 100 ? 2 : 1;
            //If there are multiple horizontal split ratios, create multiple columns
            if (totalColumns > 1)
            {
                //Get the percent horizontal split (i.e. 50/50, 60/40, 40/60)
                int[] horizontalSplits = new[] { _contentModel.ColumnSplitPercentage, 100 - _contentModel.ColumnSplitPercentage };
                //Create columns based on array of horizontal split ratios
                for (int horizontalSplitX = 0; horizontalSplitX < totalColumns; horizontalSplitX++)
                {
                    BriefingPageContent.ColumnDefinitions.Add(CreateColumnDefinition(horizontalSplits[horizontalSplitX]));
                }
            }
            //Otherwise create 1 column
            else
            {
                BriefingPageContent.ColumnDefinitions.Add(CreateColumnDefinition(1));
            }
        }

        /// <summary>
        /// This method creates and returns the ColmnDefinition based on the percentage split received
        /// </summary>
        private static ColumnDefinition CreateColumnDefinition(double split)
        {
            ColumnDefinition columnDefinition = new()
            {
                Width = new GridLength(split, GridUnitType.Star)
            };

            return columnDefinition;
        }

        /// <summary>
        /// This method creates the row height split by percentage (50/50, 60/40, 40/60)
        /// </summary>
        private void CreateAndAddRowDefinitions()
        {
            //If the verticalsplit is less than 1, then there are 2 rows, otherwise 1 row
            int totalRows = _contentModel.RowSplitPercentage < 100 ? 2 : 1;

            //If there are multiple vertical split ratios, create mltiple rows
            if (totalRows > 1)
            {
                int[] verticalSplits = new[] { _contentModel.RowSplitPercentage, 100 - _contentModel.RowSplitPercentage };
                //Create number of rows
                for (int verticalSplitX = 0; verticalSplitX < totalRows; verticalSplitX++)
                {
                    BriefingPageContent.RowDefinitions.Add(CreateRowDefinition(verticalSplits[verticalSplitX]));
                }
            }
            //Otherwise create 1 row
            else
            {
                BriefingPageContent.RowDefinitions.Add(CreateRowDefinition(1));
            }
        }

        /// <summary>
        /// This method creates and returns the RowDefinition based on the percentage split received
        /// </summary>
        private static RowDefinition CreateRowDefinition(double split)
        {
            RowDefinition rowDefinition = new()
            {
                Height = new GridLength(split, GridUnitType.Star)
            };

            return rowDefinition;
        }

        /// <summary>
        /// This method adds all left column blocks to the content grid
        /// </summary>
        private void CreateLeftColmnBlocks()
        {
            //If the verticalsplit is less than 1, then there are 2 rows, otherwise 1 row
            int totalRows = _contentModel.RowSplitPercentage < 1 ? 2 : 1;

            //Create Column 1 blocks
            for (int leftBlock = 0; leftBlock < _contentModel.NumberLeftColumnBlocks; leftBlock++)
            {
                var contentItem = _contentModel.PageContents.SingleOrDefault(content => content.ColumnPosition == 0 && content.RowPosition == leftBlock, defaultValue: new ContentModel { ColumnPosition = 0, RowPosition = leftBlock + 2 });

                BriefingPageContent.Add(
                    view: CreateBlock(
                        columnSpan: 1,
                        rowSpan: totalRows > 1 ? (totalRows / _contentModel.NumberLeftColumnBlocks) : 1,
                        content: contentItem),
                    column: 0,
                    row: leftBlock + 2
                );
            }
        }

        /// <summary>
        /// This method adds all right column blocks to the content grid
        /// </summary>
        private void CreateRightColumnBlocks()
        {
            //If the verticalsplit is less than 1, then there are 2 rows, otherwise 1 row
            int totalRows = _contentModel.RowSplitPercentage < 100 ? 2 : 1;

            //Create Column 2 blocks
            for (int rightBlock = 0; rightBlock < _contentModel.NumberRightColumnBlocks; rightBlock++)
            {
                var contentItem = _contentModel.PageContents.SingleOrDefault(content => content.ColumnPosition == 1 && content.RowPosition == rightBlock, defaultValue: new ContentModel { ColumnPosition = 1, RowPosition = rightBlock + 2 });

                BriefingPageContent.Add(
                    view: CreateBlock(
                        columnSpan: 1,
                        rowSpan: totalRows > 1 ? (totalRows / _contentModel.NumberRightColumnBlocks) : 1,
                        content: contentItem),
                    column: 1,
                    row: rightBlock + 2
                );
            }
        }

        /// <summary>
        /// This method creates and returns the content block
        /// </summary>
        private static Frame CreateBlock(int columnSpan, int rowSpan, ContentModel content)
        {
            Frame frame = new()
            {
                CornerRadius = 0,
                Content = new Label
                {
                    Text = content.ContentType,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                }
            };

            Grid.SetColumnSpan(frame, columnSpan);
            Grid.SetRowSpan(frame, rowSpan);

            return frame;
        }

        /// <summary>
        /// This method creates a navigation bar and adds to grid
        /// </summary>
        private void CreateNavigationBar()
        {
            //If the verticalsplit is less than 1, then there are 2 rows, otherwise 1 row
            int totalRows = (_contentModel.RowSplitPercentage < 100 ? 2 : 1) + 2;

            RowDefinition rowDefintion = new()
            {
                Height = new GridLength(50)
            };

            BriefingPageContent.RowDefinitions.Add(rowDefintion);

            NavigationBarView navBarView = new()
            {
                PageTitle = "BIEFING BOOK TITLE",
            };

            Grid.SetColumnSpan(navBarView, 2);
            Grid.SetRowSpan(navBarView, 1);

            BriefingPageContent.Add(
                    view: navBarView,
                    column: 0,
                    row: totalRows
                );
        }
    }
}