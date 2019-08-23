using CutestCat.Repositories.Http;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CutestCatTest.Repositories.Http
{
    public class CatHttpMapperTest
    {
        [Fact]
        public void TestToModel_ShouldFillData()
        {
            //Given
            var cat = new CatHttpObject() { Id = "ref", Url = "url" };
            //When
            var tested = cat.ToModel();
            //Then
            tested.Reference.Should().Be("ref");
            tested.Url.Should().Be("url");
        }
    }
}
