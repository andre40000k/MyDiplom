using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Remove.Transport
{
    public class TransportRemove
    {
        private readonly AplicationContext _aplicationContext;

        public TransportRemove(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
