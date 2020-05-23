using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using Leson2AspNetCoreMVC.Models;

namespace Leson2AspNetCoreMVC.Controllers
{
    public class PersonController : Controller
    {
        private string conString = "Data Source=localhost;Initial Catalog=Person;Integrated Security=True";
        
        [HttpGet]
        
        public IActionResult Index()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(conString))
                {
                    var personList = db.Query<Person>("SELECT * FROM PERSON");
                    return View(personList);
                }
            }
            catch 
            {

                
            }
            return View(null);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(string LastName, string FirstName, string MiddleName)
        {
            var person = new Person()
            {
                LastName = LastName,
                FirstName= FirstName,
                MiddleName= MiddleName
            };
            using(IDbConnection db = new SqlConnection(conString))
            {
                db.Execute("INSERT INTO PERSON([LastName],[FirstName],[MiddleName])" +
                $"VALUES('{person.LastName}','{person.FirstName}','{person.MiddleName}')");         
            }
            return RedirectToAction("Index");
        }

        public IActionResult SelectId()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SelectById(int? Id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(conString))
                {
                    var personListbyId = db.Query<Person>($"SELECT * FROM PERSON WHERE Id = '{Id}' ").ToList();
                    return View(personListbyId);
                }
            }
            catch
            {

            }
            return View(null);

        }

        public IActionResult SelectFullName()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SearchFullName(string LastName, string FirstName, string MiddleName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(conString))
                {
                    var personListbyFullName = db.Query<Person>($"SELECT * FROM PERSON  WHERE LastName like '%{LastName}%' AND FirstName like '%{FirstName}%' AND MiddleName like '%{MiddleName}%' ").ToList();
                    return View(personListbyFullName);
                }
            }
            catch
            {

            }
            return View(null);
        }

    }
}