namespace AppUI.Views;

public partial class IonInformationView : ReactiveUserControl<IonInformationViewModel>
{
    public IonInformationView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}