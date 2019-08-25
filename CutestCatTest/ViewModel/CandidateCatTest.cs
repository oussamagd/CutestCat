using CutestCat.Models;
using CutestCat.ViewModel;
using FluentAssertions;
using Xunit;

namespace CutestCatTest.ViewModel
{
    public class CandidateCatTest
    {
        [Fact]
        public void Test_WhenInitWhenParam_ThenFieldShouldBeFilled()
        {
            //Given
            var cat = new Cat() { Reference = "ref", Url = "url" };
            //When
            var candidateCatViewModel = new CadidateCatViewModel(cat);
            //then
            candidateCatViewModel.reference.Should().Be("ref");
            candidateCatViewModel.Url.Should().Be("url");

        }
    }
}
