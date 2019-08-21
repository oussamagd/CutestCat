//using System;
//using System.Collections.Generic;
//using System.Linq;
//using CareApi.Domains.Accounts;
//using CareApi.Domains.Articles;
//using CareApi.Domains.Orders;
//using CareApi.Usecases.Orders;
//using FluentAssertions;
//using Moq;
//using Xunit;

//namespace CareApiTests.Usescases.Orders
//{
//    public class GetOrdersTest
//    {
//        public GetOrdersTest()
//        {
//            orderRepositoryMocked = new Mock<IOrderRepository>();
//            customerRepositoryMocked = new Mock<IAccountRepository>();
//            articleRepositoryMocked = new Mock<IArticleRepository>();

//            tested = new GetOrders(orderRepositoryMocked.Object, customerRepositoryMocked.Object,
//              articleRepositoryMocked.Object);
//        }

//        private readonly GetOrders tested;
//        private readonly Mock<IOrderRepository> orderRepositoryMocked;
//        private readonly Mock<IAccountRepository> customerRepositoryMocked;
//        private readonly Mock<IArticleRepository> articleRepositoryMocked;


//        [Fact]
//        public void TestByAccountId_WhenAccountDoesNotHaveOrders_ShouldReturnEmpty()
//        {
//            //Given
//            var accountId = 1;
//            orderRepositoryMocked.Setup(mock => mock.GetOrdersByAccountId(accountId))
//              .Returns(new List<Order>());

//            //When
//            var result = tested.ByAccountId(accountId);

//            //Then
//            result.Should().BeEmpty();
//        }

//        [Fact]
//        public void TestByAccountId_WhenAccountHasOrders_ShouldReturnOrders()
//        {
//            //Given
//            var accountId = 1;
//            var ordersAccount_1 = new List<Order>
//      {
//        new Order
//        {
//          Id = 1, Articles = new List<Article> {new Article {Id = 101}, new Article {Id = 102}}
//        },
//        new Order {Id = 2}
//      };

//            orderRepositoryMocked.Setup(mock => mock.GetOrdersByAccountId(accountId)).Returns(ordersAccount_1);

//            var articlesEnhanced = new List<Article>
//      {
//        new Article
//        {
//          Id = 101, Picture = new Picture {SmallUrl = "small1.png", LargeUrl = "large1.png"}, Type = ArticleType.EBOOK,
//          Category = ArticleCategory.CHILDCARE
//        },
//        new Article
//        {
//          Id = 102, Picture = new Picture {SmallUrl = "small2.png", LargeUrl = "large2.png"}, Type = ArticleType.EBOOK,
//          Category = ArticleCategory.CHILDCARE
//        }
//      };
//            articleRepositoryMocked.Setup(mock => mock.GetEnhancedArticles(It.IsAny<List<Article>>()))
//              .Returns(articlesEnhanced);
//            //When
//            var result = tested.ByAccountId(accountId);

//            //Then

//            result.Should().NotBeEmpty()
//              .And.HaveCount(2);
//            var order_1 = result.First(order => order.Id == 1);
//            order_1.Articles.First(article => article.Id == 101).Picture.SmallUrl.Should().Be("small1.png");
//            order_1.Articles.First(article => article.Id == 101).Picture.LargeUrl.Should().Be("large1.png");
//            order_1.Articles.First(article => article.Id == 101).Type.Should().Be(ArticleType.EBOOK);
//            order_1.Articles.First(article => article.Id == 101).Category.Should().Be(ArticleCategory.CHILDCARE);
//        }

//        [Fact]
//        public void TestByAccountReference_WhenAccountDoesNotExist_ShouldThrowAccountNotFoundException()
//        {
//            //Given
//            var accountReference = "aaa-bbb-ccc";

//            customerRepositoryMocked.Setup(mock => mock.GetAccountByReference(accountReference))
//              .Returns((Account)null);

//            //When
//            Action getOrdersByAccountReferenceAction = () => tested.ByAccountReference(accountReference);

//            //Then
//            getOrdersByAccountReferenceAction.Should().Throw<AccountNotFoundException>();
//        }

//        [Fact]
//        public void TestByAccountReference_WhenAccountDoesNotHaveOrders_ShouldReturnEmpty()
//        {
//            //Given
//            var accountReference = "aaa-bbb-ccc";
//            var accountId = 1;
//            customerRepositoryMocked.Setup(mock => mock.GetAccountByReference(accountReference))
//              .Returns(new Account { Id = accountId, Reference = accountReference });
//            orderRepositoryMocked.Setup(mock => mock.GetOrdersByAccountId(accountId))
//              .Returns(new List<Order>());

//            //When
//            var result = tested.ByAccountReference(accountReference);

//            //Then
//            result.Should().BeEmpty();
//        }

//        [Fact]
//        public void TestByAccountReference_WhenAccountHasOrders_ShouldReturnOrders()
//        {
//            //Given
//            var accountReference = "aaa-bbb-ccc";
//            var accountId = 1;
//            customerRepositoryMocked.Setup(mock => mock.GetAccountByReference(accountReference))
//              .Returns(new Account { Id = accountId, Reference = accountReference });
//            var ordersAccount_1 = new List<Order>
//      {
//        new Order
//        {
//          Id = 1, Articles = new List<Article> {new Article {Id = 101}, new Article {Id = 102}}
//        },
//        new Order {Id = 2}
//      };
//            orderRepositoryMocked.Setup(mock => mock.GetOrdersByAccountId(accountId)).Returns(ordersAccount_1);

//            var articlesEnhanced = new List<Article>
//      {
//        new Article {Id = 101, Picture = new Picture {SmallUrl = "small1.png", LargeUrl = "large1.png"}},
//        new Article {Id = 102, Picture = new Picture {SmallUrl = "small2.png", LargeUrl = "large2.png"}}
//      };
//            articleRepositoryMocked.Setup(mock => mock.GetEnhancedArticles(It.IsAny<List<Article>>()))
//              .Returns(articlesEnhanced);

//            //When
//            var result = tested.ByAccountReference(accountReference);

//            //Then
//            result.Should().NotBeEmpty()
//              .And.HaveCount(2);
//            var order_1 = result.First(order => order.Id == 1);
//            order_1.Articles.First(article => article.Id == 101).Picture.SmallUrl.Should().Be("small1.png");
//            order_1.Articles.First(article => article.Id == 101).Picture.LargeUrl.Should().Be("large1.png");
//        }

//        [Fact]
//        public void TestGetDistinctArticlesByIdAndMarketPlace_ShouldReturnDistinctArticles()
//        {
//            //Given
//            var orders = new List<Order>
//      {
//        new Order
//        {
//          Articles = new List<Article>
//          {
//            new Article {Id = 1, IsMarketPlace = true, Label = "article_MarketPlace_1"},
//            new Article {Id = 1, IsMarketPlace = false, Label = "article_NotMarketPlace_1"}
//          }
//        },
//        new Order
//        {
//          Articles = new List<Article>
//          {
//            new Article {Id = 2, IsMarketPlace = true, Label = "article_MarketPlace_2"},
//            new Article {Id = 2, IsMarketPlace = false, Label = "article_NotMarketPlace_2"}
//          }
//        },
//        new Order
//        {
//          Articles = new List<Article>
//          {
//            new Article {Id = 1, IsMarketPlace = true, Label = "article_MarketPlace_1"},
//            new Article {Id = 1, IsMarketPlace = false, Label = "article_NotMarketPlace_1"},
//            new Article {Id = 2, IsMarketPlace = true, Label = "article_MarketPlace_2"},
//            new Article {Id = 2, IsMarketPlace = false, Label = "article_NotMarketPlace_2"},
//            new Article {Id = 3, IsMarketPlace = true, Label = "article_MarketPlace_3"},
//            new Article {Id = 3, IsMarketPlace = false, Label = "article_NotMarketPlace_3"}
//          }
//        }
//      };

//            //When
//            var result = tested.GetDistinctArticlesByIdAndMarketPlace(orders);

//            //Then
//            result.Should().NotBeEmpty();
//            result.Should().HaveCount(6);
//            result.Should().ContainSingle(a => a.Id == 1 && a.IsMarketPlace);
//            result.Should().ContainSingle(a => a.Id == 1 && !a.IsMarketPlace);
//            result.Should().ContainSingle(a => a.Id == 2 && a.IsMarketPlace);
//            result.Should().ContainSingle(a => a.Id == 2 && !a.IsMarketPlace);
//            result.Should().ContainSingle(a => a.Id == 3 && a.IsMarketPlace);
//            result.Should().ContainSingle(a => a.Id == 3 && !a.IsMarketPlace);
//        }

//        [Fact]
//        public void TestByAccountId_ShouldGetPicturesForDistinctArticlesAndMarketPlace()
//        {
//            //Given
//            var accountId = 1;
//            var ordersAccount_1 = new List<Order>
//      {
//        new Order
//        {
//          Articles = new List<Article>
//          {
//            new Article {Id = 101, IsMarketPlace = true, Label = "article_MarketPlace_1"},
//            new Article {Id = 102, IsMarketPlace = false, Label = "article_NotMarketPlace_1"}
//          }
//        },
//        new Order
//        {
//          Articles = new List<Article>
//          {
//            new Article {Id = 101, IsMarketPlace = true, Label = "article_MarketPlace_1"},
//            new Article {Id = 102, IsMarketPlace = false, Label = "article_NotMarketPlace_1"}
//          }
//        }
//      };

//            orderRepositoryMocked.Setup(mock => mock.GetOrdersByAccountId(accountId)).Returns(ordersAccount_1);

//            var articlesEnhanced = new List<Article>
//      {
//        new Article {Id = 101, Picture = new Picture {SmallUrl = "small1.png", LargeUrl = "large1.png"}},
//        new Article {Id = 102, Picture = new Picture {SmallUrl = "small2.png", LargeUrl = "large2.png"}}
//      };
//            articleRepositoryMocked.Setup(mock => mock.GetEnhancedArticles(It.IsAny<List<Article>>()))
//              .Returns(articlesEnhanced);
//            //When
//            var result = tested.ByAccountId(accountId);

//            //Then
//            result.Should().NotBeEmpty();
//            result.Should().HaveCount(2);

//            articleRepositoryMocked.Verify(mock => mock.GetEnhancedArticles(
//              It.Is<List<Article>>(articles => articles.Count == 2)
//            ), Times.Once);
//        }
//    }
//}