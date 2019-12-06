using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRD_High_School.Models;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace PRD_High_School.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly StudentContextModel _context;

        public AdminController(StudentContextModel context)
        {
            _context = context;
        }

        [Authorize(Roles = "Students")]

        [HttpGet]
        
        public  IActionResult ShowOldStudentAdmissionFees(string searchBy, string search,int? page)
        {
           
           if(searchBy == "StudentId")
          {
                return View(_context.StudentItems.Where(x=>x.StudentId.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1,2));
               
         }
            else
            {
               
                return View(_context.StudentItems.Where(x => x.Section == search || search == null).ToList().ToPagedList(page ?? 1,2));
               
            }
            
        }



        
        [HttpGet]
        public IActionResult AddOldStudentAdmissionFees()
        {
            
                return View(new StudentModel());

        }

      
        [HttpGet]
        public IActionResult EditOldStudentAdmissionFees(int id = 0)
        {
            
              return View(_context.StudentItems.Find(id));
       

        }


        

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddOldStudentAdmissionFees([Bind("Id,StudentName,StudentId,Class,Section,FixedPrice,Ammount,CollectionDate,PaymentType,Status,DateofBirth")] StudentModel student)
        {
           

            if (ModelState.IsValid)
            {
               
                 _context.Add(student);

                _context.SaveChanges();
                return RedirectToAction(nameof(ShowOldStudentAdmissionFees));
               
            }
            return View(student);
        }




        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult EditOldStudentAdmissionFees([Bind("Id,StudentName,StudentId,Class,Section,FixedPrice,Ammount,CollectionDate,PaymentType,Status,DateofBirth")] StudentModel student)
        {


            if (ModelState.IsValid)
            {
                
                  _context.Update(student);

                _context.SaveChanges();
                return RedirectToAction(nameof(ShowOldStudentAdmissionFees));

            }
            return View(student);
        }



        
        public IActionResult Delete(int? id)
        {
           var targetStudentTobeDelete = _context.StudentItems.Find(id);
            _context.StudentItems.Remove(targetStudentTobeDelete);
            _context.SaveChanges();
            return RedirectToAction(nameof(ShowOldStudentAdmissionFees));

        }



    }
}