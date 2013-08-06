using System.Runtime.InteropServices;
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
        [Fake] private IUserRepository _userRepository;
#pragma warning restore 649

        [SetUp]
        public void Setup()
        {
            Fake.InitializeFixture(this);

            A.CallTo(() => _userRepository.IsLoggedIn()).Returns(true);
            A.CallTo(() => _repository.IsUrlUniqe(A<string>._)).Returns(true);
        }

        [TestCase("","")]
        [TestCase("url","")]
        [TestCase("","title")]
        public void Should_return_400_when_url_or_title_is_empty(string url, string title)
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

        [Test]
        public void Should_return_401_if_not_logged_in_and_submitting()
        {
            A.CallTo(() => _userRepository.IsLoggedIn()).Returns(false);

            var input = new SubmitItemDto { Url = "url", Title = "title" };

            var result = (HttpStatusCodeResult)_controller.Submit(input);

            Assert.That(result.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public void Should_set_submitted_by_before_calling_repo()
        {
            var id = 667;
            var input = new SubmitItemDto { Url = "url", Title = "title" };
            A.CallTo(() => _userRepository.GetCurrentUserId()).Returns(id);

            _controller.Submit(input);

            A.CallTo(() =>_repository.Add(A<NewsItem>.That.Matches(n => n.SubmittedBy == id))).MustHaveHappened();
        }

        [Test]
        public void Should_check_url_for_uniqueness_before_adding_item()
        {
            var url = "url://";
            var input = new SubmitItemDto { Url = url, Title = "title" };

            _controller.Submit(input);

            A.CallTo(() => _repository.IsUrlUniqe(url)).MustHaveHappened();
        }

        [Test]
        public void Should_return_X_if_url_isnt_uniqe()
        {
            var url = "url://";
            var input = new SubmitItemDto { Url = url, Title = "title" };
            A.CallTo(() => _repository.IsUrlUniqe(url)).Returns(false);

            var result = (HttpStatusCodeResult)_controller.Submit(input);

            Assert.That(result.StatusCode, Is.EqualTo(406));
        }

        [Test]
        public void Should_not_be_allowed_to_vote_if_not_logged_in()
        {
            A.CallTo(() => _userRepository.IsLoggedIn()).Returns(false);

            var result = (HttpStatusCodeResult) _controller.Vote(new VoteDto());

            Assert.That(result.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public void Should_send_vote_item_id_to_repository()
        {
            var id = 123;

            _controller.Vote(new VoteDto() {Id = id});

            A.CallTo(() => _repository.Vote(id, A<int>._)).MustHaveHappened();
        }

        [Test]
        public void Should_send_user_id_to_repository_when_voting()
        {
            var uid = 321;
            A.CallTo(() => _userRepository.GetCurrentUserId()).Returns(uid);

            _controller.Vote(new VoteDto());

            A.CallTo(() => _repository.Vote(A<int>._, uid)).MustHaveHappened();
        }
    }
}
