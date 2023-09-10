using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Save.Depatment
{
    public class DepartmentSave
    {
        private readonly AplicationContext _aplicationContext;

        public DepartmentSave(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
