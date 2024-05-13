using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Zadanie.Models;

namespace Zadanie.Services
{
    public interface IServices
    {
        public bool Generate(List<DataModelDbo> data);
    }
    public class Services : IServices
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public Services(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }
        public Services(IMapper mapper) => _mapper = mapper;

        public bool Generate(List<DataModelDbo> data)
        {
            List<DataModelDto> mergeData =  MergeProducts(_mapper.Map<List<DataModelDto>>(data));
            return !_cache.Set("MemoryData", mergeData, TimeSpan.FromDays(1)).Equals(null);
        }

        public List<DataModelDto> MergeProducts(List<DataModelDto> data)
        {
            if(_cache.Get<List<DataModelDto>>("MemoryData")!=null)
                data.AddRange(_cache.Get<List<DataModelDto>>("MemoryData"));

            return data
                .GroupBy(item => item.ProductId) 
                .Select(group => new DataModelDto
                {
                    ProductId = group.Key,
                    Quantity = group.Sum(product => product.Quantity) 
                })
                .ToList(); 
        }
    }
}
