﻿using GamePlatform.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<AppUser> GetAll();
        AppUser Get(string? id);
        void Insert(AppUser entity);
        void Update(AppUser entity);
        void Delete(AppUser entity);
    }
}
