using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;

namespace App.Domain.Commands.CustomerCommands
{
    public class CreateCustomerCommand : Command
    {
        private readonly IRepository _repository;
        private readonly Customer _customer;

        public CreateCustomerCommand(IRepository repository, Customer customer)
        {
            _repository = repository;
            _customer = customer;

        }
        
        public override bool IsValid()
        {
            return true;
        }

        public override bool Execute()
        {
            if (IsValid())
            {
                _repository.Create(_customer);
                _repository.Save();
            }

            return true;
        }
    }
}
