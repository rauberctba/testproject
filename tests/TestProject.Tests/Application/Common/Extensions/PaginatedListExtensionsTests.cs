using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Application.Common.Extensions;
using TestProject.Application.Common.Models;
using Xunit;
using MockQueryable.Moq;

namespace TestProject.Tests.Application.Common.Extensions
{
    public class PaginatedListExtensionsTests
    {
        public class ToPaginatedListAsyncMethod
        {
            [Fact]
            public async Task ShouldReturnPaginatedListWithCorrectPagedItems()
            {
                var totalNumberOfElements = 100;
                var pageSize = 10;
                var pageNumber = 2;
                var firstIdOnPage2 = 11;
                var elementsToTest = Enumerable.Range(1, totalNumberOfElements).Select(i => new TestEntity { Id = i });
                var mock = elementsToTest.AsQueryable().BuildMock();

                var expectedItems = Enumerable.Range(firstIdOnPage2, pageSize).Select(i => new TestEntity { Id = i }).ToList();
                var expectedResult = new PaginatedList<TestEntity>(expectedItems, totalNumberOfElements, pageNumber, pageSize);

                var paginatedResult = await PaginatedListExtensions.ToPaginatedListAsync(mock.Object, pageNumber, pageSize);

                paginatedResult.Should().BeEquivalentTo(expectedResult);
            }
        }

        public class TestEntity
        {
            public int Id { get; set; }
        }
    }
}
