using Gov.LightingSystem.Feedback.Controllers;
using Gov.LightingSystem.Feedback.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace LightingSystemFeedbackTest
{
    [TestClass]
    public class LightingFeedbackTest
    {
        private FeedbackController feedbackController;
        private Mock<ISession> mockSession;
        private Mock<IUserFeedbackRepository> feedbackRepoMock;
        [TestInitialize]
        public void init()
        {
            //HttpContext.Session 

            feedbackRepoMock = new Mock<IUserFeedbackRepository>();
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockSession = new Mock<ISession>();
            mockHttpContext.Setup(c => c.Session).Returns(mockSession.Object);
            feedbackController = new FeedbackController(feedbackRepoMock.Object);
            feedbackController.ControllerContext.HttpContext = mockHttpContext.Object;
        }

        [TestMethod]
        public void ReturnsUserFeedbackWithValidRequest()
        {           
            UserFeedback testFeedback = new UserFeedback
            {
                Id = 1234,
                UserName = "Test user",
                EmailAddress = "test@gmail.com",
                HomeAddress = "123 test address, leeds",
                IsHappyWithService = true,
                LevelOfBrightness = 9
            };

            var testFeedBackBytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(testFeedback));

            mockSession.Setup(s => s.TryGetValue(It.IsAny<string>(), out testFeedBackBytes)).Returns(true);

            UserFeedback feedback = new UserFeedback
            {
                Id = 0,
                UserName = "Test user",
                EmailAddress = "test@gmail.com",
                HomeAddress = "123 test address, leeds",
                IsHappyWithService = true,
                LevelOfBrightness = 92
            };
            feedbackRepoMock.Setup(m => m.AddFeedback(feedback)).Returns(testFeedback);
            feedbackController.PostSummary();
            Assert.AreNotEqual(feedback.Id, testFeedback.Id);
            Assert.IsNotNull(testFeedback);
            feedbackRepoMock.Verify(mock => mock.AddFeedback(It.IsAny<UserFeedback>()), Times.Once());
        }

        [TestMethod]
        public void ReturnsErrorMessageWithInvalidRequest()
        {
            UserFeedback feedback = new UserFeedback
            {
                Id = 0,
                UserName =  string.Empty               
            };

            byte[] dummy = null;

            mockSession.Setup(s => s.TryGetValue(It.IsAny<string>(), out dummy)).Returns(true);

            ViewResult result = (ViewResult)feedbackController.Index(1, feedback);

            Assert.IsTrue(result.ViewData.Values.Contains("Missing User Name"));

        }
    }
}
