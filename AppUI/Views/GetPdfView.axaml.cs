namespace AppUI.Views;

public partial class GetPdfView : ReactiveUserControl<GetPdfViewModel>
{
    public GetPdfView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}