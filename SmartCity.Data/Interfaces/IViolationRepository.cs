using System;
using System.Collections.Generic;
using SmartCity.Common.Enums;
using SmartCity.Data.Entities.Police;

namespace SmartCity.Data.Interfaces
{
    public interface IViolationRepository
    {
        Violation[] GetAll(int max);
        bool AddViolation(Violation item, string blamingUserLogin, string blamedUserLogin);

        Violation[] GetByGivenSettings(string searchword, DateTime? dateFrom, DateTime? dateTo,
            WayOfOrder orderWay, string neededStatus, out int foundTotal, int currentPage = 0, int pageMax = 50);

        Violation Get(long id);
        List<Violation> GetAll();
        void Save(Violation model);
        void Delete(long id);
    }
}