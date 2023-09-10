using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Remove.Depatment
{
    public class DepartmentRemove
    {
        private readonly AplicationContext _aplicationContext;

        public DepartmentRemove(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
