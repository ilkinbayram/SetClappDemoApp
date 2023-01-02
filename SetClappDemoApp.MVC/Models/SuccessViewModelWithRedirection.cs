namespace SetClappDemoApp.MVC.Models
{
    public class SuccessViewModelWithRedirection
    {
        public bool Success => true;
        public string? RedirectToUrl { get; set; }
    }
}
