using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Save.Parcel
{
    public class ParcelSave
    {
        private readonly AplicationContext _aplicationContext;

        public ParcelSave(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
