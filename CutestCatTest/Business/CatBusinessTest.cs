using CutestCat.Business;
using CutestCat.Models;
using CutestCat.Repositories.Http;
using CutestCat.Repositories.Sql;
using CutestCatTest.data;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CutestCatTest.Business
{

    public class CatBusinessTest
    {
        private readonly CatBusiness Tested;
        private readonly Mock<ICatSqlRepository> catSqlRepositoryMocked;
        private readonly Mock<ICatHttpRepository> catHttpRepositoryMocked;

        public CatBusinessTest()
        {
            catSqlRepositoryMocked = new Mock<ICatSqlRepository>();
            catHttpRepositoryMocked = new Mock<ICatHttpRepository>();
            Tested = new CatBusiness(catSqlRepositoryMocked.Object, catHttpRepositoryMocked.Object);
        }

        [Fact]
        public void TestRandomIndex_ShouldReturnCorrectNumberOfIndex()
        {
            //given
            var number = 3;
            //When
            var index = Tested.GetRandomIndex(1000, number);
            //then
            index.Count.Should().Be(number);
        }

        [Fact]
        public void TestRandomIndex_ShouldReturnDifferentIndex()
        {
            //given
            var number = 2;
            //When
            var index = Tested.GetRandomIndex(1000, number);
            //then
            index[0].Should().NotBe(index[1]);

        }

        [Fact]
        public void TestGetCandidates_ShouldReturnCorrectNumberOfCandidates()
        {
            //Given
            catHttpRepositoryMocked.Setup(mock => mock.GetAllCandidates()).Returns(CatFactory.GetAllCandidates());

            //When
            var candidates = Tested.GetCandidates();

            //Then
            candidates.Count.Should().Be(CatBusiness.NbrOfCandidate);
        }

        [Fact]
        public void TestGetCandidates_ShouldReturnDifferentCandidates()
        {
            //Given
            catHttpRepositoryMocked.Setup(mock => mock.GetAllCandidates()).Returns(CatFactory.GetAllCandidates());

            //When
            var candidates = Tested.GetCandidates();

            //Then
            candidates[0].Reference.Should().NotBe(candidates[1].Reference);
            candidates[0].Url.Should().NotBe(candidates[1].Url);
        }

        [Fact]
        public void TestGetCandidates_ShouldReturnCandidatesWithLoadedData()
        {
            //Given
            catHttpRepositoryMocked.Setup(mock => mock.GetAllCandidates()).Returns(CatFactory.GetAllCandidates());

            //When
            var candidates = Tested.GetCandidates();

            //Then
            candidates[0].Reference.Should().NotBeNull();
            candidates[0].Url.Should().NotBeNull();
            candidates[1].Reference.Should().NotBeNull();
            candidates[1].Url.Should().NotBeNull();

        }

        [Fact]
        public void TestGetCat_ShouldReturnCatWithLoadedData()
        {
            //Given
            catSqlRepositoryMocked.Setup(mock => mock.GetCats()).Returns(CatFactory.GetCats());

            //When
            var cats = Tested.GetCats();

            //Then
            cats[0].Reference.Should().Be("Ugly");
            cats[0].Url.Should().Be("ugly.com");
            cats[0].LostVoteCount.Should().Be(10);
            cats[0].WinVoteCount.Should().Be(1);

        }

        [Fact]
        public void TestGetCat_WhenNoDatainDB_ShouldReturnEmptyList()
        {
            //Given
            catSqlRepositoryMocked.Setup(mock => mock.GetCats()).Returns(new List<Cat>());

            //When
            var cats = Tested.GetCats();

            //Then
            cats.Count.Should().Be(0);

        }
    }
}
