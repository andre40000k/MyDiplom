using LoginComponent.Interface.IRepositories.Customer;
using LoginComponent.Interface.IServices.Customer;

namespace LoginComponent.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepsitory _customerRepsitory;

        public CustomerService(ICustomerRepsitory customerRepsitory)
        {
            _customerRepsitory = customerRepsitory;
        }
    }
}
