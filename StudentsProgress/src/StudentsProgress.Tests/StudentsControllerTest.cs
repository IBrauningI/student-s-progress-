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
    public class StudentsControllerTest
    {
        public class HomeControllerTests
        {
            [Fact]
            public async Task Index_ReturnsAViewResult_WithData()
            {
                // Arrange
                var st = new List<Student>()
                {
                    new Student
                    {

                        Id=1,
                        Faculty="AMI",
                        GroupId=1,
                        Group=new Group{Name="AMI31"},
                        UserId="1"
                    }
                };
                var mockLogic = new Mock<IStudentsLogic>();
                mockLogic.Setup(repo => repo.GetStudents()).Returns(Task.FromResult(st));
                var controller = new StudentsController(mockLogic.Object);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Student>>(
                    viewResult.ViewData.Model);
                Assert.Equal(st[0].Group.Name, model[0].Group.Name);

            }

            [Fact]
            public async Task GetStudent_OnlyOnce()
            {
                // Arrange
                var st = new List<Student>()
                {
                };
                var mockLogic = new Mock<IStudentsLogic>();
                mockLogic.Setup(repo => repo.GetStudents()).Returns(Task.FromResult(st));
                var controller = new StudentsController(mockLogic.Object);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Student>>(
                    viewResult.ViewData.Model);
                mockLogic.Verify(s => s.GetStudents(), Times.Once);
            }
        }
    }
}
