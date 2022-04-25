namespace AppUI.ViewModels;

[DataContract]
public class ContractInformationViewModel : ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public ContractInformationViewModel()
    {
        HostScreen = Locator.Current.GetService<IScreen>();
    }
    public ContractInformationViewModel(IScreen screen) => HostScreen = screen;
}