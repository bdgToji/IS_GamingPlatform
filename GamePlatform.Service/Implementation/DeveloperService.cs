using GamePlatform.Domain.Domain;
using GamePlatform.Repository.Interface;
using GamePlatform.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Service.Implementation
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IRepository<Developer> _devRepository;

        public DeveloperService(IRepository<Developer> devRepository)
        {
            _devRepository = devRepository;
        }

        public List<Developer> GetAllDevelopers()
        {
            return _devRepository.GetAll().ToList();
        }

        public Developer GetDeveloper(Guid? id)
        {
            return _devRepository.Get(id);
        }

        public void CreateDeveloper(Developer developer)
        {
            _devRepository.Insert(developer);
        }

        public void UpdateDeveloper(Developer developer)
        {
            _devRepository.Update(developer);
        }

        public void DeleteDeveloper(Guid? id)
        {
            var dev = _devRepository.Get(id);
            _devRepository.Delete(dev);
        }
    }
}
