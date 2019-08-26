using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using IOCO.Demo.Services.Employee;
using IOCO.Demo.Services.Person;
using IOCO.Demo.StateControl;
using IOCO.Demo.ViewModels.Base;
using IOCO.Demo.ViewModels.Validation;
using IOCO.Models;
using Xamarin.Forms;

namespace IOCO.Demo.ViewModels
{
    public class AddPersonViewModel: ViewModelBase
    {
        private readonly IPeopleService _peopleService;
        private readonly IEmployeeService _employeeService;
        private ValidatableObject<string> _firstName;
        public ValidatableObject<string> FirstName
        {
            get => _firstName;
            set => this.SetProperty(ref _firstName, value);
        }

        private ValidatableObject<string> _lastName;
        public ValidatableObject<string> LastName
        {
            get => _lastName;
            set => this.SetProperty(ref _lastName, value);
        }

        private ValidatableObject<string> _employeeNumber;
        public ValidatableObject<string> EmployeeNumber
        {
            get => _employeeNumber;
            set => this.SetProperty(ref _employeeNumber, value);
        }

        private ValidatableObject<DateTime> _birthDate;
        public ValidatableObject<DateTime> BirthDate
        {
            get => _birthDate;
            set => this.SetProperty(ref _birthDate, value);
        }


        public AddPersonViewModel(IPeopleService peopleService, IEmployeeService employeeService)
        {
            _peopleService = peopleService;
            _employeeService = employeeService;
            
            _employeeNumber = new ValidatableObject<string>();
            _firstName = new ValidatableObject<string>();
            _lastName = new ValidatableObject<string>();
            _birthDate = new ValidatableObject<DateTime>();
            AddValidations();
        }

        public ICommand SaveCommand => new Command(async () =>
        {

            try
            {

                
                if (FirstName.Validate() && LastName.Validate() && EmployeeNumber.Validate() && BirthDate.Validate())
                {
                    State = State.Loading;
                    var person = new Person()
                    {
                        FirstName = this.FirstName.Value,
                        LastName = this.LastName.Value,
                        BirthDate = this.BirthDate.Value,
                        PersonId = new Random().Next(1, 6000)
                    };

                    var createdPerson = await _peopleService.Create("People", person);
                    if (createdPerson != null)
                    {
                        var employee = new Employee()
                        {
                            EmployedDate = DateTime.Now,
                            EmployeeNumber = this.EmployeeNumber.Value,
                            PersonId = createdPerson.PersonId.Value,
                            EmployeeId = new Random().Next(1, 6000)


                        };
                        var createdEmployee = await _employeeService.Create("Employees", employee);
                        if (createdEmployee != null)
                        {
                            await DialogService.ShowAlertAsync("Employee Created", "Success", "OK");
                            await NavigationService.NavigateBackAsync();
                        }

                    }
                }
                else
                {
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await DialogService.ShowAlertAsync("An error occurred.", "Error", "Ok");
            }
            finally
            {
                State = State.None;

            }


        });
        private void AddValidations()
        {
            _firstName.Validations.Add(new IsNotNullOrEmptyRule<string>(){ValidationMessage = "First name should not be empty"});
            _lastName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Last name should not be empty" });
            _employeeNumber.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Employee number should not be empty" });
            _birthDate.Validations.Add(new IsDateNotEmpty<DateTime>(){ValidationMessage = "Please select a date of birth"});
        }


    }
}
