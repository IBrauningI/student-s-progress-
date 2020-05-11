using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentsProgress.Web.Controllers;
using StudentsProgress.Web.Data.Entities;
using StudentsProgress.Web.Logics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using AutoFixture.AutoMoq;

namespace StudentsProgress.Tests
{
    public class GroupsControllerTest
    {
        public class HomeControllerTests
        {
            [Fact]
            public async Task Index_ReturnsAViewResult_WithData()
            {
                // Arrange
                var gr = new List<Group>()
                {
                    new Group
                    {

                        Id=1,
                        Name="AMI31"
                    }
                };
                var mockLogic = new Mock<IGroupsLogic>();
                mockLogic.Setup(repo => repo.GetGroups()).Returns(Task.FromResult(gr));
                var controller = new GroupsController(mockLogic.Object);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Group>>(
                    viewResult.ViewData.Model);
                Assert.Equal(gr[0].Name, model[0].Name);

            }

            [Fact]
            public async Task GetGroup_OnlyOnce()
            {
                // Arrange
                var gr = new List<Group>()
                {
                };
                var mockLogic = new Mock<IGroupsLogic>();
                mockLogic.Setup(repo => repo.GetGroups()).Returns(Task.FromResult(gr));
                var controller = new GroupsController(mockLogic.Object);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Group>>(
                    viewResult.ViewData.Model);
                mockLogic.Verify(s => s.GetGroups(), Times.Once);
            }
        }
    }
}
