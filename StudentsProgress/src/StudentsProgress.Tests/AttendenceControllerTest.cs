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
    public class AttendenceControllerTest
    {
        public class HomeControllerTests
        {
            [Fact]
            public async Task Index_ReturnsAViewResult_WithData()
            {
                // Arrange
                var attes = new List<Attendance>()
                {
                    new Attendance
                    {
                        Id=1,
                        PassesCount=3,
                        StudentId=1,
                        SubjectId=1,
                        Student=new Student{ Faculty="AMI"}


                    }
                };
                var mockLogic = new Mock<IAttendancesLogic>();
                mockLogic.Setup(repo => repo.GetAttendances()).Returns(Task.FromResult(attes));
                var controller = new AttendancesController(mockLogic.Object);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Attendance>>(
                    viewResult.ViewData.Model);
                mockLogic.Verify(s => s.GetAttendances(), Times.Once);
                Assert.Equal(attes[0].Student.Faculty, model[0].Student.Faculty);
            }

            [Fact]
            public async Task GetAttendence_OnlyOnce()
            {
                // Arrange
                var attes = new List<Attendance>()
                {
                    new Attendance
                    {
                        Id=1,
                        PassesCount=3,
                        StudentId=1,
                        SubjectId=1,
                        Student=new Student{ Faculty="AMI"}


                    }
                };
                var mockLogic = new Mock<IAttendancesLogic>();
                mockLogic.Setup(repo => repo.GetAttendances()).Returns(Task.FromResult(attes));
                var controller = new AttendancesController(mockLogic.Object);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Attendance>>(
                    viewResult.ViewData.Model);
                mockLogic.Verify(s => s.GetAttendances(), Times.Once);
                Assert.Equal(attes[0].Student.Faculty, model[0].Student.Faculty);
            }

            [Fact]
            public async Task UpdateView_ReturnsAViewResult_WithData()
            {
                // Arrange
                var attes = new Attendance()
                {
                    Id = 1,
                    PassesCount = 3,
                    StudentId = 1,
                    SubjectId = 1,
                    Student = new Student { Faculty = "AMI" }
                };
                
                var mockLogic = new Mock<IAttendancesLogic>();
                int attendanceId = 1;

                mockLogic.Setup(repo => repo.GetAttendance(attendanceId)).Returns(Task.FromResult(attes));              
                var controller = new AttendancesController(mockLogic.Object);

                //  Act
                IActionResult actionResult = await controller.Edit(attendanceId, attes);

                // Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(actionResult);
                mockLogic.Verify(repo => repo.UpdateAttendance(attes), Times.Once);

            }
        }
    }
}
