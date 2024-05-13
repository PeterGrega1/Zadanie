using Zadanie.Models;

namespace UnitTests
{
    public class UnitTest
    {
        [Fact]
        public void MergeProducts_SingleProduct_ReturnsUnmodifiedList()
        {
            var data = new List<DataModelDto> { new DataModelDto { ProductId = "1", Quantity = 2 } };

            var result = MergeProducts(data);

             Assert.True(data.Count == result.Count);
        }

        [Fact]
        public void MergeProducts_MultipleProducts_MergesQuantities()
        {
            // Arrange
            var data = new List<DataModelDto>
            {
                new DataModelDto { ProductId = "1", Quantity = 2 },
                new DataModelDto { ProductId = "1", Quantity = 3 },
                new DataModelDto { ProductId = "2", Quantity = 5 }
            };

            var expectedMergedData = new List<DataModelDto>
            {
                new DataModelDto { ProductId = "1", Quantity = 5 },
                new DataModelDto { ProductId = "2", Quantity = 5 }
            };

            var result = MergeProducts(data);

            Assert.True(result.All(merged => expectedMergedData.Any(expected => merged.ProductId == expected.ProductId && merged.Quantity == expected.Quantity)));
        }

        private List<DataModelDto> MergeProducts(List<DataModelDto> data)
        {
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