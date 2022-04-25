namespace AppUI.ViewModels;

public class MainWindowViewModel : ViewModelBase, IScreen
{
    public RoutingState Router { get; } = new RoutingState();
    
    public ReactiveCommand<Unit, IRoutableViewModel> GoContractInformation { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoIonInformation { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoSessionsInformation { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoGetPdf { get; }

    public MainWindowViewModel()
    {
        GoContractInformation =
            ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(new ContractInformationViewModel(this))
                );
        GoIonInformation =
            ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(new IonInformationViewModel(this))
                );
        GoSessionsInformation =
            ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(new SessionInformationViewModel(this))
            );
        GoGetPdf =
            ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(new GetPdfViewModel(this))
            );
    }
}