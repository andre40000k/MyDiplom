using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Serch.Transport
{
    public class TransportSerch
    {
        private readonly AplicationContext _aplicationContext;

        public TransportSerch(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
