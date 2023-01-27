using ArcGIS.StoryMaps.BriefingBook.ViewModels;
using ArcGIS.StoryMaps.BriefingBook.Models;

namespace ArcGIS.StoryMaps.BriefingBook.Pages
{
    public partial class BriefingPage : ContentPage
    {
        public BriefingPage(BriefingPageViewModel briefingPageViewModel)
        {
            InitializeComponent();
            BindingContext = briefingPageViewModel;
        }

        /// <summary>
        /// After BindingContext has been initialized, access it
        /// </summary>
        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            CreateLayout();
        }

        /// <summary>
        /// This method takes the received binding context to create the 
        /// briefing page layout structure.
        /// </summary>
        private void CreateLayout()
        {
            BriefingPageViewModel briefingPageViewModel = (BriefingPageViewModel)BindingContext;
            BriefingPageModel briefingPageModel = briefingPageViewModel.BriefingPageModel;
            BriefingPageContentModel briefingPageContentModel = briefingPageViewModel.BriefingPageContentModel;

            CreateClassificationBannerSpace(briefingPageModel: briefingPageModel, forTopBannerOtherwiseBottom: true, briefingPageContentModel: briefingPageContentModel);

            CreatePageHeader(briefingPageModel: briefingPageModel);

            CreateAndAddColumnDefinitions(briefingPageContentModel: briefingPageContentModel);
            CreateAndAddRowDefinitions(briefingPageContentModel: briefingPageContentModel);

            CreateLeftColmnBlocks(briefingPageContentModel: briefingPageContentModel);
            CreateRightColumnBlocks(briefingPageContentModel: briefingPageContentModel);

            CreateClassificationBannerSpace(briefingPageModel: briefingPageModel, forTopBannerOtherwiseBottom: false, briefingPageContentModel: briefingPageContentModel);
        }

        /// <summary>
        /// This method creates and adds the top or bottom classification banner
        /// </summary>
        private void CreateClassificationBannerSpace(BriefingPageModel briefingPageModel, bool forTopBannerOtherwiseBottom, BriefingPageContentModel briefingPageContentModel)
        {
            //If the verticalsplit is less than 1, then there are 2 rows, otherwise 1 row
            int totalRows = (briefingPageContentModel.RowSplit < 100 ? 2 : 1) + 3;

            RowDefinition rd = new()
            {
                Height = new GridLength(25)
            };
            briefingPageContent.RowDefinitions.Add(rd);

            briefingPageContent.Add(
                    view: CreateClassificationBanner(
                        columnSpan: 2,
                        rowSpan: 1,
                        classification: briefingPageModel.Classification
                        ),
                    column: 0,
                    row: forTopBannerOtherwiseBottom ? 0 : totalRows
                );
        }

        /// <summary>
        /// This method creates and returns the page header
        /// </summary>
        private static Frame CreateClassificationBanner(int columnSpan, int rowSpan, BriefingPageModel.ClassificationTypes classification)
        {
            Console.WriteLine($"This is calassification {classification.ToString()}");

            Frame f = new()
            {
                BackgroundColor = GetClassificationColor(classification),
                Margin = 0,
                Padding = 0,
                Content = new Label
                {
                    Text = classification.ToString(),
                    FontSize = 10,
                    LineHeight = 25,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Padding = 0,
                }
            };
            Grid.SetColumnSpan(f, columnSpan);
            Grid.SetRowSpan(f, rowSpan);
            return f;
        }

        /// <summary>
        /// This method creates and adds the page header
        /// </summary>
        private void CreatePageHeader(BriefingPageModel briefingPageModel)
        {
            RowDefinition rd = new()
            {
                Height = new GridLength(50)
            };
            briefingPageContent.RowDefinitions.Add(rd);

            briefingPageContent.Add(
                    view: CreateHeaderFrame(
                        columnSpan: 2,
                        rowSpan: 1,
                        header: briefingPageModel.PageTitle
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
            Frame f = new()
            {
                Margin = 0,
                Padding = new Thickness(10, 0, 10, 0),
                Content = new Label
                {
                    Text = header,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center,
                }
            };
            Grid.SetColumnSpan(f, columnSpan);
            Grid.SetRowSpan(f, rowSpan);
            return f;
        }

        /// <summary>
        /// This method creates and returns the page header
        /// </summary>
        private static Color GetClassificationColor(BriefingPageModel.ClassificationTypes classification)
        {
            if (classification == BriefingPageModel.ClassificationTypes.Classified)
            {
                return Colors.Red;
            }
            else if (classification == BriefingPageModel.ClassificationTypes.Unclassified)
            {
                return Colors.Green;
            }
            return Colors.Green;
        }

        /// <summary>
        /// This method creates the column width split by percentage (50/50, 60/40, 40/60)
        /// </summary>
        private void CreateAndAddColumnDefinitions(BriefingPageContentModel briefingPageContentModel)
        {
            //If the horizontalsplit is less than 1, then there are 2 columns, otherwise 1 column
            int totalColumns = briefingPageContentModel.ColumnSplit < 100 ? 2 : 1;
            //If there are multiple horizontal split ratios, create multiple columns
            if (totalColumns > 1)
            {
                //Get the percent horizontal split (i.e. 50/50, 60/40, 40/60)
                int[] horizontalSplits = new[] { briefingPageContentModel.ColumnSplit, 100 - briefingPageContentModel.ColumnSplit };
                //Create columns based on array of horizontal split ratios
                for (int horizontalSplitX = 0; horizontalSplitX < totalColumns; horizontalSplitX++)
                {
                    briefingPageContent.ColumnDefinitions.Add(CreateColumnDefinition(horizontalSplits[horizontalSplitX]));
                }
            }
            //Otherwise create 1 column
            else
            {
                briefingPageContent.ColumnDefinitions.Add(CreateColumnDefinition(1));
            }
        }

        /// <summary>
        /// This method creates and returns the ColmnDefinition based on the percentage split received
        /// </summary>
        private static ColumnDefinition CreateColumnDefinition(double split)
        {
            ColumnDefinition cd = new()
            {
                Width = new GridLength(split, GridUnitType.Star)
            };
            return cd;
        }

        /// <summary>
        /// This method creates the row height split by percentage (50/50, 60/40, 40/60)
        /// </summary>
        private void CreateAndAddRowDefinitions(BriefingPageContentModel briefingPageContentModel)
        {
            //If the verticalsplit is less than 1, then there are 2 rows, otherwise 1 row
            int totalRows = briefingPageContentModel.RowSplit < 100 ? 2 : 1;

            //If there are multiple vertical split ratios, create mltiple rows
            if (totalRows > 1)
            {
                int[] verticalSplits = new[] { briefingPageContentModel.RowSplit, 100 - briefingPageContentModel.RowSplit };
                //Create number of rows
                for (int verticalSplitX = 0; verticalSplitX < totalRows; verticalSplitX++)
                {
                    briefingPageContent.RowDefinitions.Add(CreateRowDefinition(verticalSplits[verticalSplitX]));
                }
            }
            //Otherwise create 1 row
            else
            {
                briefingPageContent.RowDefinitions.Add(CreateRowDefinition(1));
            }
        }

        /// <summary>
        /// This method creates and returns the RowDefinition based on the percentage split received
        /// </summary>
        private static RowDefinition CreateRowDefinition(double split)
        {
            RowDefinition rd = new()
            {
                Height = new GridLength(split, GridUnitType.Star)
            };
            return rd;
        }

        /// <summary>
        /// This method adds all left column blocks to the content grid
        /// </summary>
        private void CreateLeftColmnBlocks(BriefingPageContentModel briefingPageContentModel)
        {
            //If the verticalsplit is less than 1, then there are 2 rows, otherwise 1 row
            int totalRows = briefingPageContentModel.RowSplit < 1 ? 2 : 1;

            //Create Column 1 blocks
            for (int leftBlock = 0; leftBlock < briefingPageContentModel.NumberLeftColumnBlocks; leftBlock++)
            {
                var contentItem = briefingPageContentModel.PageContents.SingleOrDefault(content => content.ColumnPosition == 0 && content.RowPosition == leftBlock, defaultValue: new ContentModel { ColumnPosition = 0, RowPosition = leftBlock + 2 });

                briefingPageContent.Add(
                    view: CreateBlock(
                        columnSpan: 1,
                        rowSpan: totalRows > 1 ? (totalRows / briefingPageContentModel.NumberLeftColumnBlocks) : 1,
                        content: contentItem),
                    column: 0,
                    row: leftBlock + 2
                );
            }
        }

        /// <summary>
        /// This method adds all right column blocks to the content grid
        /// </summary>
        private void CreateRightColumnBlocks(BriefingPageContentModel briefingPageContentModel)
        {
            //If the verticalsplit is less than 1, then there are 2 rows, otherwise 1 row
            int totalRows = briefingPageContentModel.RowSplit < 100 ? 2 : 1;

            //Create Column 2 blocks
            for (int rightBlock = 0; rightBlock < briefingPageContentModel.NumberRightColumnBlocks; rightBlock++)
            {
                var contentItem = briefingPageContentModel.PageContents.SingleOrDefault(content => content.ColumnPosition == 1 && content.RowPosition == rightBlock, defaultValue: new ContentModel { ColumnPosition = 1, RowPosition = rightBlock + 2 });

                briefingPageContent.Add(
                    view: CreateBlock(
                        columnSpan: 1,
                        rowSpan: totalRows > 1 ? (totalRows / briefingPageContentModel.NumberRightColumnBlocks) : 1,
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
            Frame f = new()
            {
                Content = new Label
                {
                    Text = content.ContentType,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                }
            };
            Grid.SetColumnSpan(f, columnSpan);
            Grid.SetRowSpan(f, rowSpan);
            return f;
        }
    }
}