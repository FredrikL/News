using System.Web.Mvc;
using FakeItEasy;
using News.Controllers;
using News.Models;
using News.Models.Dto;
using News.Repository;
using NUnit.Framework;

namespace News.Tests
{
    [TestFixture]
    public class NewsControllerTests
    {
#pragma warning disable 649
        [UnderTest] private NewsController _controller;
        [Fake] private INewsRepository _repository;
#pragma warning restore 649

        [SetUp]
        public void Setup()
        {
            Fake.InitializeFixture(this);
        }

        [TestCase("","")]
        [TestCase("url","")]
        [TestCase("","title")]
        public void Should_return__when_url_or_title_is_empty(string url, string title)
        {
            var input = new SubmitItemDto {Url = url, Title = title};

            var result = (HttpStatusCodeResult)_controller.Submit(input);
            
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public void Should_send_url_to_repository_during_submit()
        {
            var url = "url://";
            var input = new SubmitItemDto {Url = url, Title = "title"};

            _controller.Submit(input);

            A.CallTo(() => _repository.Add(A<NewsItem>.That.Matches(n => n.Url == url))).MustHaveHappened();
        }

        [Test]
        public void Should_send_title_to_repository_during_submit()
        {
            var title = "some title";
            var input = new SubmitItemDto { Url = "url://", Title = title };

            _controller.Submit(input);

            A.CallTo(() => _repository.Add(A<NewsItem>.That.Matches(n => n.Title == title))).MustHaveHappened();
        }
    }
}
