using CutestCat.Repositories.Sql;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CutestCatTest.Repositories.Sql
{
   public class CatSqlMapperTest
    {
        [Fact]
        public void TestToModel_ShouldFillData()
        {
            //Given
            var cat = new CatSqlObjet() { Reference = "ref", Url = "url" , LostVoteCount = 1, WinVoteCount = 5};
            //When
            var tested = cat.ToModel();
            //Then
            tested.Reference.Should().Be("ref");
            tested.Url.Should().Be("url");
            tested.WinVoteCount.Should().Be(5);
            tested.LostVoteCount.Should().Be(1);
        }
    }
}
