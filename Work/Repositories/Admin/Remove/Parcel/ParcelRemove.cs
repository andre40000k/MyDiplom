using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Remove.Parcel
{
    public class ParcelRemove
    {
        private readonly AplicationContext _aplicationContext;

        public ParcelRemove(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
