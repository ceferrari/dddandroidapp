using App.Application.Interfaces;
using App.Application.ViewModels;
using App.Domain.Commands.CustomerCommands;
using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using AutoMapper;

namespace App.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository _repository;

        public CustomerService(IRepository repository)
        {
            _repository = repository;
        }

        public bool Create(CustomerViewModel customerViewModel)
        {
            var customer = Mapper.Map<Customer>(customerViewModel);
            var command = new CreateCustomerCommand(_repository, customer);
            return command.Execute();
        }

        public bool Exists(CustomerViewModel customerViewModel)
        {
            var customer = Mapper.Map<Customer>(customerViewModel);
            return _repository.GetExists<Customer>(x => x.Email.Equals(customer.Email));
        }
    }
}
