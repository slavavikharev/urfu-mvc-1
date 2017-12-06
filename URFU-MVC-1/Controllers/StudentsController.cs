using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using URFU_MVC_1.Interfaces;
using URFU_MVC_1.Models;

namespace URFU_MVC_1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsController(
            [FromServices]IStudentsRepository studentRepository
        )
        {
            _studentsRepository = studentRepository;
        }

        [HttpGet("/students/")]
        public IActionResult Students()
        {
            var students = _studentsRepository.Get().ToList();

            return View(
                new StudentsViewModel
                {
                    Students = students
                }
            );
        }

        [HttpPost("/students/")]
        public IActionResult NewStudent([FromForm]StudentModel studentData)
        {
            _studentsRepository.Add(studentData);
            _studentsRepository.SaveChanges();

            return RedirectToAction("Students");
        }

        [HttpGet("/students/{id}/")]
        public IActionResult Student(int id)
        {
            var student = _studentsRepository.Get().SingleOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(
                new StudentViewModel
                {
                    Student = student
                }
            );
        }

        [HttpPatch("/students/{id}/")]
        public IActionResult EditStudent(int id, [FromForm]StudentModel studentData)
        {
            var student = _studentsRepository.Get().SingleOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            student.FirstName = (string)studentData.FirstName;
            student.LastName = (string)studentData.LastName;
            _studentsRepository.SaveChanges();

            return RedirectToAction("Student", new { id });
        }

        [HttpDelete("/students/{id}/")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _studentsRepository.Get().SingleOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            _studentsRepository.Remove(student);

            return RedirectToAction("Students");
        }
    }
}
