using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Update.Depatment
{
    public class DepartmentUpdate
    {
        private readonly AplicationContext _aplicationContext;

        public DepartmentUpdate(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
