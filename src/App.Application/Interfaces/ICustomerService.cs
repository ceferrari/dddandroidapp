using App.Application.ViewModels;

namespace App.Application.Interfaces
{
    public interface ICustomerService
    {
        bool Create(CustomerViewModel customerViewModel);

        bool Exists(CustomerViewModel customerViewModel);
    }
}
