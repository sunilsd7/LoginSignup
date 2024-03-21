using LoginSignup.Models;

namespace LoginSignup.ViewModel
{
    public class EmployeeListViewModel
    {
        public IEnumerable<Employee> Employee { get; }

        public EmployeeListViewModel(IEnumerable<Employee> employee)
        {

            Employee = employee;
        }
    }
}
