using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentsProgress.Web.Controllers;
using StudentsProgress.Web.Data.Entities;
using StudentsProgress.Web.Logics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;


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
            [Fact]
            public async Task UpdateView_ReturnsAViewResult_WithData()
            {
                // Arrange
                var model = new Student()
                {
                    Id = 1,
                    Faculty = "AMI",
                    Group = new Group { Name = "AMI31" },
                    GroupId = 1,
                    UserId = "1"
                };

                var mockLogic = new Mock<IStudentsLogic>();
                int studentId = 1;

                mockLogic.Setup(repo => repo.GetStudent(studentId)).Returns(Task.FromResult(model));
                var controller = new StudentsController(mockLogic.Object);

                //  Act
                IActionResult actionResult = await controller.Edit(studentId, model);

                // Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(actionResult);
                mockLogic.Verify(repo => repo.UpdateStudent(model), Times.Once);

            }

            [Fact]
            public async Task UpdateDelete_ReturnsAViewResult()
            {
                // Arrange
                var model = new Student()
                {
                    Id = 1,
                    Faculty = "AMI",
                    Group = new Group { Name = "AMI31" },
                    GroupId = 1,
                    UserId = "1"
                };

                var mockLogic = new Mock<IStudentsLogic>();
                int studentId = 1;

                mockLogic.Setup(repo => repo.GetStudent(studentId)).Returns(Task.FromResult(model));
                var controller = new StudentsController(mockLogic.Object);

                //  Act
                IActionResult actionResult = await controller.DeleteConfirmed(studentId);

                // Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(actionResult);
                mockLogic.Verify(repo => repo.DeleteStudent(model), Times.Once);

            }
        }
    }
}
