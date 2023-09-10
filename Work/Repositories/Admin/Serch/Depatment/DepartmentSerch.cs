using LoginComponent.DataBase;

namespace LoginComponent.Repositories.Admin.Serch.Depatment
{
    public class DepartmentSerch
    {
        private readonly AplicationContext _aplicationContext;

        public DepartmentSerch(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
    }
}
