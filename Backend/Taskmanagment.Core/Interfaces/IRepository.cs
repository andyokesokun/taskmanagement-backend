﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> FindAll();
        Task<T> Find(int? id);
        Task Update(T t);
        Task Save(T t);
        Task Delete(int? id);
        Task<bool> Exist(int? id);
      
        



    }
}