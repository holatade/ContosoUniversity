using AutoMapper;
using Contoso.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Mapper
{
    public class VierModelToDomain : Profile
    {
        public VierModelToDomain()
        {
            CreateMap<StudentViewModel, Student>();
        }
    }
}
