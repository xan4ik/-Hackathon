namespace AppUI.ViewModels;

[DataContract]
public class SessionInformationViewModel : ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public SessionInformationViewModel()
    {
        HostScreen = Locator.Current.GetService<IScreen>();
    }
    public SessionInformationViewModel(IScreen screen) => HostScreen = screen;
}