using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IOCO.Demo.Services.Employee;
using IOCO.Demo.StateControl;
using IOCO.Demo.ViewModels.Base;
using IOCO.Models;
using Xamarin.Forms;

namespace IOCO.Demo.ViewModels
{
    public class DetailsViewModel: BaseDetailsViewModelBase<FullEmployee>
    {
        private readonly IEmployeeService _employeeService;

        public DetailsViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
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
                var deleted = await _employeeService.Delete("Employee", Detail.EmployeeId.Value);
                if (deleted)
                {
                    await NavigationService.NavigateBackAsync();
                }
                else
                {
                    //Show error message;
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
