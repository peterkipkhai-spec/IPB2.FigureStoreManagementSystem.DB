namespace IPB2.FigureStoreManagementSystem.WinForm;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Form { Text = "Figure Store WinForms" });
    }
}
