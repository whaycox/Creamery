namespace Gouda.WebApp.ViewModels.Abstraction
{
    using Application.Abstraction;

    public interface IWebAppViewModel : IViewModel
    {
        string ID { get; set; }
        string Class { get; set; }
    }
}
