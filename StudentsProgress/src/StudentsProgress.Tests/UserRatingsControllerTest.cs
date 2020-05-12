using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentsProgress.Web.Controllers;
using StudentsProgress.Web.Data.Entities;
using StudentsProgress.Web.Logics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace StudentsProgress.Tests
{
    public class UserRatingControllerTest
    {
        public class HomeControllerTests
        {
            [Fact]
            public async Task Index_ReturnsAViewResult_WithData()
            {
                // Arrange
                var usrait = new List<UserRating>()
                {
                    new UserRating
                    {

                        Id=1,
                        SemestrPoints=43,
                        SumPoints=78,
                        StudentId=1,
                        SubjectId=1,
                        Subject=new Subject{Name="Math"}

                    }
                };
                var mockLogic = new Mock<IUserRatingsLogic>();
                mockLogic.Setup(repo => repo.GetUserRatings()).Returns(Task.FromResult(usrait));
                var controller = new UserRatingsController(mockLogic.Object);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<UserRating>>(
                    viewResult.ViewData.Model);
                Assert.Equal(usrait[0].Subject.Name, model[0].Subject.Name);

            }

            [Fact]
            public async Task GetUserRating_OnlyOnce()
            {
                // Arrange
                var usrait = new List<UserRating>()
                {
                };
                var mockLogic = new Mock<IUserRatingsLogic>();
                mockLogic.Setup(repo => repo.GetUserRatings()).Returns(Task.FromResult(usrait));
                var controller = new UserRatingsController(mockLogic.Object);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<UserRating>>(
                    viewResult.ViewData.Model);
                mockLogic.Verify(s => s.GetUserRatings(), Times.Once);

            }

            [Fact]
            public async Task UpdateView_ReturnsAViewResult_WithData()
            {
                // Arrange
                var rate = new UserRating()
                {
                    Id = 1,
                    SemestrPoints = 43,
                    SumPoints = 78,
                    StudentId = 1,
                    SubjectId = 1,
                    Subject = new Subject { Name = "Math" }
                };              

                var mockLogic = new Mock<IUserRatingsLogic>();
                int userRatId = 1;

                mockLogic.Setup(repo => repo.GetUserRating(userRatId)).Returns(Task.FromResult(rate));
                var controller = new UserRatingsController(mockLogic.Object);

                //  Act
                IActionResult actionResult = await controller.Edit(userRatId, rate);

                // Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(actionResult);
                mockLogic.Verify(repo => repo.UpdateUserRating(rate), Times.Once);

            }
        }
    }
}
