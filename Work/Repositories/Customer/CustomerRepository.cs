using LoginComponent.DataBase;
using LoginComponent.Interface.IRepositories.Customer;

namespace LoginComponent.Repositories.Customer
{
    public class CustomerRepository : ICustomerRepsitory
    {

        private readonly AplicationContext _aplicationContext;

        public CustomerRepository(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
