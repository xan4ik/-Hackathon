namespace AppUI.ViewModels;

[DataContract]
public class IonInformationViewModel : ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public IonInformationViewModel()
    {
        HostScreen = Locator.Current.GetService<IScreen>();
    }
    public IonInformationViewModel(IScreen screen) => HostScreen = screen;
}