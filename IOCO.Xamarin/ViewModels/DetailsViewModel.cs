using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IOCO.Demo.Services.Employee;
using IOCO.Demo.Services.Person;
using IOCO.Demo.StateControl;
using IOCO.Demo.ViewModels.Base;
using IOCO.Models;
using Xamarin.Forms;

namespace IOCO.Demo.ViewModels
{
    public class DetailsViewModel: BaseDetailsViewModelBase<FullEmployee>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPeopleService _peopleService;

        public DetailsViewModel(IEmployeeService employeeService, IPeopleService peopleService)
        {
            _employeeService = employeeService;
            _peopleService = peopleService;
        }
        public override Task InitializeAsync(object navigationData)
        {
            this.Detail = navigationData as FullEmployee;
            return base.InitializeAsync(navigationData);
        }

        public ICommand DeleteCommand => new Command(async () =>
        {
            try
            {
                State = State.Loading;
                var deleted = await _employeeService.Delete("Employees", Detail.EmployeeId.Value);
                if (deleted)
                {
                    var deletedPerson = await _peopleService.Delete("People", Detail.PersonId);
                    if (deletedPerson)
                    {
                        await DialogService.ShowAlertAsync("Employee deleted successfully", "Success", "OK");

                        await NavigationService.NavigateBackAsync();

                    }
                    
                }
                else
                {
                    await DialogService.ShowAlertAsync("Error deleting the employee.", "Error", "OK");
                }
            }
            catch (Exception e)
            {
                State = State.None;

            }

            finally
            {
                State = State.None;
            }
        });
    }
}
