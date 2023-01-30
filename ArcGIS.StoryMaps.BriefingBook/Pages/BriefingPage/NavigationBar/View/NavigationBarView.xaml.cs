namespace ArcGIS.StoryMaps.BriefingBook.Views;

public partial class NavigationBarView : ContentView
{
    public static readonly BindableProperty PageTitleProperty = BindableProperty.Create(nameof(PageTitle), typeof(string), typeof(NavigationBarView), string.Empty);

    public string PageTitle
    {
        get => (string)GetValue(NavigationBarView.PageTitleProperty);
        set => SetValue(NavigationBarView.PageTitleProperty, value);
    }

    public NavigationBarView()
    {
        InitializeComponent();
    }

    void PriorPageButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Console.WriteLine("PRIOR PAGE CLICKED NAVIGATION BAR VIEW");
    }

    void NextPageButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Console.WriteLine("Next PAGE CLICKED NAVIGATION BAR VIEW");
    }

    void FavoriteButton_Clicked(System.Object sender, System.EventArgs e)
    {
    }

    void PopupMenuButton_Clicked(System.Object sender, System.EventArgs e)
    {
    }

    void ProfileButton_Clicked(System.Object sender, System.EventArgs e)
    {
    }

    void NavigationMenuButton_Clicked(System.Object sender, System.EventArgs e)
    {
        if (Shell.Current.FlyoutBehavior == FlyoutBehavior.Disabled)
        {
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
        }
        else if (Shell.Current.FlyoutBehavior == FlyoutBehavior.Locked)
        {
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        }
        else if (Shell.Current.FlyoutBehavior == FlyoutBehavior.Flyout)
        {
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        }
    }
}
