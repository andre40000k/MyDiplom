using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Update.Transport
{
    public class TransportUpdate
    {
        private readonly AplicationContext _aplicationContext;

        public TransportUpdate(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
