namespace AppUI.Views;

public partial class ContractInformationView : ReactiveUserControl<ContractInformationViewModel>
{
    public ContractInformationView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}