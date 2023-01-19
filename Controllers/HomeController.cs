using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDSample.Models;
using System.Collections;

namespace CRUDSample.Controllers;
public class HomeController : Controller
{
    public static List<EmployeeModel> EmpList = new List<EmployeeModel>();
    Hashtable departments = new Hashtable {
    {"1", "System Development"},
    {"2", "Research and Development"},
    {"3", "Recruitment and HR"},
    {"4", "Network Management"}
    };
    string alertMsg = "";
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public void sharedMember(string Msg)
    {
        if (Msg != "")
        {
            ViewBag.Message = Msg;
        }
        ViewBag.EmployeeModel = EmpList.OrderBy(_ => _.EmpId).ToList();
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Employee()
    {
        sharedMember("");
        return View("EmployeeList");
    }
    [HttpPost]
    public IActionResult CreateEmployee(int EmpId, string EmpNm, string Dept, string Desig, int Salary, string JDate)
    {
        bool temp = false;
        int id = 0;
        string deptNm = string.Empty;
        for (int i = 0; i < EmpList.Count; i++)
        {
            if (EmpList[i].EmpId == EmpId)
            {
                id = EmpId;
                temp = true;
            }
        }
        if (temp == true)
        {
            alertMsg = string.Format("Employee Id: " + id + " Already Exists!");
            sharedMember(alertMsg);
            return View("EmployeeList");
        }
        else
        {
            for (int i = 1; i <= 4; i++)
            {
                if (Dept == i.ToString())
                {
                    if (departments.ContainsKey(Dept))
                    {
                        deptNm = departments[Dept].ToString();
                    }
                }
            }
            var newModelist = new EmployeeModel
            {
                EmpId = EmpId,
                EmpNm = EmpNm,
                Dept = deptNm,
                Desig = Desig,
                Salary = Salary,
                JDate = JDate,
            };
            EmpList.Add(newModelist);
            alertMsg = string.Format("New Employee created successfully!");
            sharedMember(alertMsg);
            return View("EmployeeList");
        }
    }
    public IActionResult Search(int EmpId)
    {
        List<EmployeeModel> EmpSearchList = new List<EmployeeModel>();
        if (EmpId != 0)
        {
            for (int i = 0; i < EmpList.Count; i++)
            {
                if (EmpList[i].EmpId == EmpId)
                {
                    EmpSearchList.Add(EmpList[i]);
                }
            }
            ViewBag.EmployeeModel = EmpSearchList;
            return View("EmployeeList");
        }
        else
        {
            sharedMember("");
            return View("EmployeeList");
        }
    }

    public IActionResult Edit(int EmpId)
    {
        List<EmployeeModel> EmpEditList = new List<EmployeeModel>();
        string deptVal = "";
        string deptKey = "";
        for (int i = 0; i < EmpList.Count; i++)
        {
            if (EmpList[i].EmpId == EmpId)
            {
                EmpEditList.Add(EmpList[i]);
                deptVal = EmpList[i].Dept;
            }
        }
        foreach (string key in departments.Keys)
        {
            if (departments[key].ToString() == deptVal)
            {
                deptKey = key;
            }
        }
        ViewBag.Message = deptKey;
        ViewBag.EmployeeModel = EmpEditList;
        return View("EditEmployee");
    }

    public IActionResult EditEmployee(int EmpId, string EmpNm, string Dept, string Desig, int Salary, string JDate)
    {
        string deptNm = "";
        for (int i = 1; i <= 4; i++)
        {
            if (Dept == i.ToString())
            {
                if (departments.ContainsKey(Dept) && departments != null)
                {
                    deptNm = departments[Dept].ToString();
                }
            }
        }
        for (int i = 0; i < EmpList.Count; i++)
        {
            if (EmpList[i].EmpId == EmpId)
            {
                EmpList[i].EmpNm = EmpNm.Trim();
                EmpList[i].Dept = deptNm.Trim();
                EmpList[i].Desig = Desig.Trim();
                EmpList[i].Salary = Salary;
                EmpList[i].JDate = JDate;
            }
        }
        alertMsg = string.Format("Employee details updated successfully!");
        sharedMember(alertMsg);
        return View("EmployeeList");
    }
    public IActionResult Delete(int EmpId)
    {
        for (int i = 0; i < EmpList.Count; i++)
        {
            if (EmpList[i].EmpId == EmpId)
            {
                EmpList.Remove(EmpList[i]);
            }
        }
        alertMsg = string.Format("Employee Id: " + EmpId + " deleted successfully!");
        sharedMember(alertMsg);
        return View("EmployeeList");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
