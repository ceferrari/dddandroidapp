using System.Collections.Generic;
using App.Application.ViewModels;
using App.Domain.Entities;

namespace App.Application.Interfaces
{
    public interface ICustomerService
    {
        bool Create(CustomerViewModel customerViewModel);

        CustomerViewModel GetFirst(CustomerViewModel customerViewModel);

        IEnumerable<CustomerViewModel> GetAll();

        bool Exists(CustomerViewModel customerViewModel);
    }
}
