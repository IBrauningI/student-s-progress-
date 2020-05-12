using StudentsProgress.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentsProgress.Web.Data.Entities;
using StudentsProgress.Web.Logics;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudentsProgress.Web.Data.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using StudentsProgress.Web.Models;

namespace StudentsProgress.Tests.UserTest
{
    public class PersonalCabinetTest
    {
        [Fact]
        public void AttendanceThrowException_WhenUserNull()
        {
            var identity = new IdentityUser()
            {
                Id = "1"
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, identity.Id),
            }, "mock"));

            var mockUserManager = new Mock<IUserService>();
            mockUserManager.Setup(x => x.GetApplicationUser(user)).Returns(Task.FromResult(new ApplicationUser()));
            var mockLogic = new Mock<IPersonalCabinetLogic>();

            //setup mockLOgic

            var controller = new PersonalCabinetController(mockUserManager.Object, mockLogic.Object);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };


            Assert.ThrowsAsync<Exception>(() => controller.Attendance());
        }

        [Fact]
        public async Task ReturnsCorrectData_AttendanceAsync()
        {
            var identity = new IdentityUser()
            {
                Id = "1"
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, identity.Id),
            }, "mock"));

            var mockUserManager = new Mock<IUserService>();
            mockUserManager.Setup(x => x.GetApplicationUser(user)).Returns(Task.FromResult(new ApplicationUser()
            {
                Id = "1"
            }));
            var mockLogic = new Mock<IPersonalCabinetLogic>();
            var atees = new List<Attendance>()
            {
                new Attendance
                {
                    Id=1,
                   PassesCount=3,
                   Subject = new Subject { Name = "Math", LecturesCount =5 }
                }
            };
            var stud = new Student()
            {
                Id = 1
            };
            mockLogic.Setup(logic => logic.GetAttendanceById(1)).Returns(Task.FromResult(atees));
            mockLogic.Setup(logic => logic.GetStudentById("1")).Returns(Task.FromResult(stud));


            var controller = new PersonalCabinetController(mockUserManager.Object, mockLogic.Object);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            var res = await controller.Attendance();
            //var res = await controller.AccountManager();
            var viewResult = Assert.IsType<ViewResult>(res);

            var model = Assert.IsAssignableFrom<List<AttendanceViewModel>>(
                   viewResult.ViewData.Model);
            //var model2 = Assert.IsAssignableFrom<Student>(
            //       viewResult.ViewData.Model);
            Assert.Equal(model[0].Subject, atees[0].Subject.Name);
            Assert.Equal(model[0].LecturesCount, atees[0].Subject.LecturesCount);
            Assert.Equal(model[0].PassesCount, atees[0].PassesCount);
            // mockLogic.Verify(s => s.GetAttendanceById(), Times.Once);

        }
    }
}
