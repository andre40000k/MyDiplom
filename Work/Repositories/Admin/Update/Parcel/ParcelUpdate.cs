using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Update.Parcel
{
    public class ParcelUpdate
    {
        private readonly AplicationContext _aplicationContext;

        public ParcelUpdate(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
