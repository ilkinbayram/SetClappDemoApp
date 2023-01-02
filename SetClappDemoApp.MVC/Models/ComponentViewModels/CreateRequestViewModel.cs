using Core.Entities.Concrete;

namespace SetClappDemoApp.MVC.Models.ComponentViewModels
{
    public class CreateRequestViewModel
    {
        public List<User> ExistWorkers { get; set; }
        public User ActiveUser { get; set; }
    }
}
