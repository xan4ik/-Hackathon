namespace AppUI.Views;

public partial class SessionInformationView : ReactiveUserControl<SessionInformationViewModel>
{
    public SessionInformationView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}