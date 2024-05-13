using AutoMapper;
using Zadanie.Models;
using Microsoft.Extensions.Logging;

namespace Zadanie.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<DataModelDbo, DataModelDto>();
            CreateMap<DataModelDto, DataModelDbo>();
        }
    }
}
