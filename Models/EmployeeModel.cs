namespace CRUDSample.Models;

public class EmployeeModel
{
    public int EmpId { get; set; }
    public string EmpNm { get; set; } = string.Empty;
    public string Dept { get; set; } = string.Empty;
    public string Desig { get; set; } = string.Empty;
    public int Salary { get; set; }
    public string JDate { get; set; } = string.Empty;
}