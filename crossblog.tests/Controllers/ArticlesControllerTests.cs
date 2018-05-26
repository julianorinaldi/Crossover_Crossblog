using crossblog.Controllers;
using crossblog.Domain;
using crossblog.Model;
using crossblog.Repositories;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace crossblog.tests.Controllers
{
    public class ArticlesControllerTests
    {
        private readonly ArticlesController _articlesController;
        private readonly Mock<IArticleRepository> _articleRepositoryMock = new Mock<IArticleRepository>();

        public ArticlesControllerTests()
        {
            _articlesController = new ArticlesController(_articleRepositoryMock.Object);
        }

        [Fact]
        public async Task ArticleSearch_ReturnsEmptyList()
        {
            // Arrange
            var articleDbSetMock = Builder<Article>.CreateListOfSize(3).Build().ToAsyncDbSetMock();
            _articleRepositoryMock.Setup(m => m.Query()).Returns(articleDbSetMock.Object);

            // Act
            var result = await _articlesController.Search("Invalid");

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as ArticleListModel;
            Assert.NotNull(content);

            Assert.Empty(content.Articles);
        }

        [Fact]
        public async Task ArticleSearch_ReturnsList()
        {
            // Arrange
            var articleDbSetMock = Builder<Article>.CreateListOfSize(3).Build().ToAsyncDbSetMock();
            _articleRepositoryMock.Setup(m => m.Query()).Returns(articleDbSetMock.Object);

            // Act
            var result = await _articlesController.Search("Title");

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as ArticleListModel;
            Assert.NotNull(content);

            Assert.Equal(3, content.Articles.Count());
        }

        [Fact]
        public async Task ArticleGet_NotFound()
        {
            // Arrange
            _articleRepositoryMock.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<Article>(null));

            // Act
            var result = await _articlesController.Get(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
        }

        [Fact]
        public async Task ArticleGet_ReturnsItem()
        {
            // Arrange
            _articleRepositoryMock.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<Article>(Builder<Article>.CreateNew().Build()));

            // Act
            var result = await _articlesController.Get(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as ArticleModel;
            Assert.NotNull(content);

            Assert.Equal("Title1", content.Title);
        }

        [Fact]
        public async Task ArticlePut_Item()
        {
            // Model
            var modelPut = new ArticleModel()
            {
                Id = 1,
                Title = "Title Altered",
                Content = "Content Altered",
            };

            // Arrange
            _articleRepositoryMock.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<Article>(Builder<Article>.CreateNew().Build()));

            // Act
            var result = await _articlesController.Put(1, modelPut);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as Article;
            Assert.NotNull(content);

            Assert.Equal(modelPut.Id, content.Id);
            Assert.Equal(modelPut.Content, content.Content);
            Assert.Equal(modelPut.Title, content.Title);
        }

        [Fact]
        public async Task ArticlePut_NotFound()
        {
            // Arrange
            _articleRepositoryMock.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<Article>(null));

            // Act
            var result = await _articlesController.Put(1, new ArticleModel());

            // Assert
            Assert.NotNull(result);

            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
        }


        [Fact]
        public async Task ArticlePost_Item()
        {
            // Model
            var modelPut = new ArticleModel()
            {
                Title = "Title New",
                Content = "Content New",
                Published = false,
                Date = DateTime.Now
            };

            // Act
            var result = await _articlesController.Post(modelPut);

            Assert.NotNull(result);

            var objectResult = result as CreatedResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as Article;
            Assert.NotNull(content);

            Assert.Equal(modelPut.Content, content.Content);
            Assert.Equal(modelPut.Title, content.Title);
        }

        [Fact]
        public async Task ArticleDelete_Item()
        {
            // Arrange
            _articleRepositoryMock.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<Article>(Builder<Article>.CreateNew().Build()));

            // Act
            var result = await _articlesController.Delete(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkResult;
            Assert.NotNull(objectResult);
        }

        [Fact]
        public async Task ArticleDelete_NotFound()
        {
            // Arrange
            _articleRepositoryMock.Setup(m => m.GetAsync(1)).Returns(Task.FromResult<Article>(null));

            // Act
            var result = await _articlesController.Delete(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
        }
    }
}
