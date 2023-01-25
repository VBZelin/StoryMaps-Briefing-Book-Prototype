namespace ArcGIS.StoryMaps.BriefingBook.Shared.CustomComponents.UI
{
    public partial class ImageWithIndicator : Frame
    {
        public static readonly BindableProperty ShowIndicatorProperty = BindableProperty.Create("ShowIndicator", typeof(bool), typeof(Frame), false, propertyChanged: OnShowIndicatorChanged);

        public bool ShowIndicator
        {
            get => (bool)GetValue(ShowIndicatorProperty);
            set => SetValue(ShowIndicatorProperty, value);
        }

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create("ImageSource", typeof(ImageSource), typeof(Frame), null, propertyChanged: OnImageSourceChanged);

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public CustomImage()
        {
            InitializeComponent();

            ActivityIndicator.IsVisible = ShowIndicator;
            Image.Source = null;
        }

        private static void OnShowIndicatorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomImage)bindable;

            control.ActivityIndicator.IsVisible = (bool)newValue;
        }

        private static void OnImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomImage)bindable;

            control.Image.Source = null;
            control.Image.Source = (ImageSource)newValue;
        }
    }
}
