using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Save.Transport
{
    public class TransportSave
    {
        private readonly AplicationContext _aplicationContext;

        public TransportSave(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
