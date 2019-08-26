using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
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
    public class MainViewModel: BaseListViewModelBase<FullEmployee>
    {
        private readonly IPeopleService _peopleService;
        private readonly IEmployeeService _employeeService;

        public MainViewModel(IPeopleService peopleService, IEmployeeService employeeService)
        {
            _peopleService = peopleService;
            _employeeService = employeeService;
        }
        private void LoadData(IEnumerable<FullEmployee> employees)
        {
           
            employees = employees?.OrderBy(r => r.FirstName).ToList();
            Collection = employees != null ? new ObservableCollection<FullEmployee>(employees) : new ObservableCollection<FullEmployee>();
            
            ShowNoResults = !Collection.Any();

            if (ShowNoResults)
            {
                State = State.Empty;
            }
            else
            {
                State = State.None;
            }
          
        }

        public ICommand GotoDetailsPageCommand => new Command<FullEmployee>(employee =>
            {
                NavigationService.NavigateToAsync<DetailsViewModel>(employee);
            });

        public ICommand GotoAddDetailsPageCommand => new Command(() =>
        {
            NavigationService.NavigateToAsync<AddPersonViewModel>();
        });
        public override async Task InitializeAsync(object navigationData)
        {

            try
            {
                State = State.Loading;
                var persons = await _peopleService.Get("People").ConfigureAwait(false);
                var employees = await _employeeService.Get("Employees").ConfigureAwait(false);
                this.Collection = new ObservableCollection<FullEmployee>();
                if (persons != null && persons.Any() && employees != null && employees.Any())
                {

                    foreach (var person in persons)
                    {
                        var employee = employees.FirstOrDefault(r => r.PersonId == person.PersonId);
                        this.Collection.Add(new FullEmployee()
                        {
                            EmployeeId = employee?.EmployeeId,
                            PersonId = person.PersonId.Value,
                            FirstName = person.FirstName?.Trim(),
                            LastName = person.LastName?.Trim(),
                            BirthDate = person.BirthDate ?? new DateTime(1970,1,1),
                            EmployeeNumber = employee?.EmployeeNumber?.Trim(),
                            EmployedDate = employee?.EmployedDate ?? new DateTime(1970,1,1),
                            TerminatedDate = employee?.TerminatedDate ?? new DateTime(1970, 1, 1),
                        });
                    }
                }
                State = State.None;
                this.ReferenceResults = Collection.ToList();
                if (!this.Collection.Any())
                {
                    State = State.Empty;
                }
                SearchCommand = new Command<string>(s =>
                {
                    var searchResults = Search(s, this.ReferenceResults,  "FirstName");
                    LoadData(searchResults);
                });
            }
            catch (Exception e)
            {
                
            }
           
        }
    }
}
