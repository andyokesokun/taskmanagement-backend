using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.Map
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            CreateMap<Dtos.Task, Entities.Task>();
        }
    }
}
