using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Zaliczenie.Controllers;
using Zaliczenie.Models;
using Zaliczenie.Services;

namespace WebApplicationTestProject
{
    [TestFixture]
    public class StudentControllerTests
    {
        private List<StudentViewModel> _students;
        private Mock<IStudentService> _studentServiceMock;

        [SetUp]
        public void Setup()
        {
            // Prepare sample students
            _students = new List<StudentViewModel>
            {
                new StudentViewModel() { Id = 1, Name = "Asterix", IndexNumber = "000001", Email = "asterix@example.com" },
                new StudentViewModel() { Id = 2, Name = "Obelix", IndexNumber = "000002", Email = "obelix@example.com" }
            };

            _studentServiceMock = new Mock<IStudentService>();
        }

        #region Index
        [Test]
        public void TestIndexAction()
        {
            // Arrange
            _studentServiceMock.Setup(m => m.FindAll()).Returns(_students);
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");
            Assert.IsNotNull(viewResult.Model);
            Assert.IsInstanceOf<List<StudentViewModel>>(viewResult.Model);
            var modelStudents = viewResult.Model as List<StudentViewModel>;
            Assert.That(modelStudents, Has.Count.EqualTo(2));
        }
        #endregion

        #region Create
        [Test]
        public void TestCreate_Get_ReturnsViewResult()
        {
            // Arrange
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void TestCreate_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var newStudent = new StudentViewModel() { Id = 3, Name = "Cacofonix", IndexNumber = "000003", Email = "cacofonix@example.com" };
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Create(newStudent);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
            _studentServiceMock.Verify(s => s.Add(newStudent), Times.Once);
        }

        [Test]
        public void TestCreate_Post_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var newStudent = new StudentViewModel() { Id = 3, Name = "", IndexNumber = "000003", Email = "invalid@example.com" };
            var controller = new StudentController(_studentServiceMock.Object);
            // Simulate a validation error
            controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = controller.Create(newStudent);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual(newStudent, viewResult.Model);
        }
        #endregion

        #region Edit
        [Test]
        public void TestEdit_Get_ValidId_ReturnsViewWithStudent()
        {
            // Arrange
            var student = _students.First();
            _studentServiceMock.Setup(s => s.FindById(student.Id.Value)).Returns(student);
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Edit(student.Id.Value);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual(student, viewResult.Model);
        }

        [Test]
        public void TestEdit_Get_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _studentServiceMock.Setup(s => s.FindById(It.IsAny<int>())).Returns((StudentViewModel)null);
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Edit(999);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void TestEdit_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var student = _students.First();
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Edit(student);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
            _studentServiceMock.Verify(s => s.Update(student), Times.Once);
        }

        [Test]
        public void TestEdit_Post_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var student = _students.First();
            var controller = new StudentController(_studentServiceMock.Object);
            controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = controller.Edit(student);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual(student, viewResult.Model);
        }
        #endregion

        #region Delete
        [Test]
        public void TestDelete_Get_ValidId_ReturnsViewWithStudent()
        {
            // Arrange
            var student = _students.First();
            _studentServiceMock.Setup(s => s.FindById(student.Id.Value)).Returns(student);
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Delete(student.Id.Value);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual(student, viewResult.Model);
        }

        [Test]
        public void TestDelete_Get_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _studentServiceMock.Setup(s => s.FindById(It.IsAny<int>())).Returns((StudentViewModel)null);
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Delete(999);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void TestDelete_Post_RedirectsToIndex()
        {
            // Arrange
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.DeleteConfirmed(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
            _studentServiceMock.Verify(s => s.Delete(1), Times.Once);
        }
        #endregion

        #region Details
        [Test]
        public void TestDetails_Get_ValidId_ReturnsViewWithStudent()
        {
            // Arrange
            var student = _students.First();
            _studentServiceMock.Setup(s => s.FindById(student.Id.Value)).Returns(student);
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Details(student.Id.Value);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual(student, viewResult.Model);
        }

        [Test]
        public void TestDetails_Get_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _studentServiceMock.Setup(s => s.FindById(It.IsAny<int>())).Returns((StudentViewModel)null);
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Details(999);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        #endregion

        #region Search
        [Test]
        public void TestSearch_WithEmptyQuery_RedirectsToIndex()
        {
            // Arrange
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Search("");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [Test]
        public void TestSearch_WithValidQuery_ReturnsIndexViewWithFilteredStudents()
        {
            // Arrange
            _studentServiceMock.Setup(s => s.FindAll()).Returns(_students);
            var controller = new StudentController(_studentServiceMock.Object);

            // Act
            var result = controller.Search("Asterix");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            Assert.IsNotNull(viewResult.Model);
            var filtered = viewResult.Model as List<StudentViewModel>;
            Assert.That(filtered, Has.Count.EqualTo(1));
            Assert.AreEqual("Asterix", filtered.First().Name);
        }
        #endregion
    }
}
