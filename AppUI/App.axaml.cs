namespace AppUI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Locator.CurrentMutable.Register<IViewFor<ContractInformationViewModel>>(() =>
                    new ContractInformationView());
                Locator.CurrentMutable.Register<IViewFor<IonInformationViewModel>>(() =>
                    new IonInformationView());
                Locator.CurrentMutable.Register<IViewFor<SessionInformationViewModel>>(() =>
                    new SessionInformationView());
                Locator.CurrentMutable.Register<IViewFor<GetPdfViewModel>>(() =>
                    new GetPdfView());
                
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}