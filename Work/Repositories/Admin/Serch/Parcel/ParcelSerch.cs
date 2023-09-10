using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Serch.Parcel
{
    public class ParcelSerch
    {
        private readonly AplicationContext _aplicationContext;

        public ParcelSerch(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
