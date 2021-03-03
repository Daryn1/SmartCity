using SmartCity.Data.Entities.Medicine;

namespace SmartCity.Data.Repositories
{
    public class HealthDepartmentRepository : BaseRepository<HealthDepartment>
    {
        public HealthDepartmentRepository(SmartCityDbContext context) : base(context) { }
       


    }
}
