using LoadAndUpdateUsingJqueryAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoadAndUpdateUsingJqueryAjax.DAO;
using System.Web.Script.Serialization;

namespace LoadAndUpdateUsingJqueryAjax.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult LoadData()
        {
            var dao = new EmployeeDAO();
            var listDataEmployee = new List<tbl_Employee>();
            var listEm = dao.GetListEmployees();
            foreach (var item in listEm)
            {
                listDataEmployee.Add(new tbl_Employee()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Age = item.Age,
                    Status = item.Status
                });
            }
            return Json(new
            {
                data = listDataEmployee,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(string model)
        {
            // chuyển đổi 1 string sang
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            tbl_Employee employee = serializer.Deserialize<tbl_Employee>(model);

            var listEm = new EmployeeDbContext();
            var entity = listEm.tbl_Employee.Single(x => x.ID == employee.ID);
            entity.Age = employee.Age;
            listEm.SaveChanges();
            return Json(new
            {
                status = true
            });
        }
    }
}