using GamePlatform.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Service.Interface
{
    public interface IDeveloperService
    {
        List<Developer> GetAllDevelopers();
        Developer GetDeveloper(Guid? id);
        void CreateDeveloper(Developer developer);
        void UpdateDeveloper(Developer developer);
        void DeleteDeveloper(Guid? id);
    }
}
