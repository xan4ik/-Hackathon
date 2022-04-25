namespace AppUI.ViewModels;

[DataContract]
public class GetPdfViewModel : ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public GetPdfViewModel()
    {
        HostScreen = Locator.Current.GetService<IScreen>();
    }
    public GetPdfViewModel(IScreen screen) => HostScreen = screen;
}