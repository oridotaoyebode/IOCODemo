using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace IOCO.Demo.ViewModels.Base
{
    public abstract class BaseListViewModelBase<T>: ViewModelBase where T: class
    {
       


        private bool _isGroupingEnabled = true;

        public bool IsGroupingEnabled
        {
            get => _isGroupingEnabled;
            set => SetProperty(ref _isGroupingEnabled, value);
        }
        private ObservableCollection<T> _collection;

        public ObservableCollection<T> Collection
        {
            get => _collection;
            set => SetProperty(ref _collection, value);
        }


        private string _selectedId;

        public string SelectedId
        {
            get => _selectedId;
            set => this.SetProperty(ref _selectedId, value);
        }
        protected IEnumerable<T> ReferenceResults { get; set; }

        private string _searchTerm;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                SetProperty(ref _searchTerm, value);
                SearchCommand.Execute(_searchTerm);
            }
        }

      protected IEnumerable<T> Search(string searchWord, IEnumerable<T> items, params string[] propertiesToSearch)
        {
            try
            {
                if (string.IsNullOrEmpty(searchWord))
                {
                    return items;
                }
                var queryParts = searchWord.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var filteredList = queryParts
                    .Aggregate(items, (current, token) =>
                    {
                        var results = new List<T>();
                        foreach (var s in propertiesToSearch)
                        {
                            var r = current.Where(p =>
                                p.GetType().GetProperty(s).GetValue(p).ToString().ToLower().Contains(token.ToLower()));
                            results.AddRange(r);
                        }

                        return results;
                    });
                return filteredList.ToList().Distinct();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }

            return null;

        }

        private ICommand _searchCommand;

        protected ICommand SearchCommand
        {
            get => _searchCommand;
            set => SetProperty(ref _searchCommand, value);
        }

        protected IEnumerable<IGrouping<string, T>> GetGrouping(IEnumerable<T> list,  Func<T, string> order, string propertyName)
        {
            IEnumerable<IGrouping<string, T>> grouping = null;
            var orderList = list.OrderBy(order).GroupBy(c =>
                c.GetType().GetProperty(propertyName)?.GetValue(c)?.ToString().Substring(0, 1).ToUpper() ?? "#");
            grouping = orderList;
            return grouping;
        }
        protected IEnumerable<IGrouping<DateTime, T>> GetDateGrouping(IEnumerable<T> list, Func<T, DateTime> order, string propertyName)
        {
            IEnumerable<IGrouping<DateTime, T>> grouping = null;
            var orderList = list.OrderBy(order).GroupBy(c => 
                DateTime.Parse(c.GetType().GetProperty(propertyName)?.GetValue(c).ToString()).Date);
            grouping = orderList;
            return grouping;
        }
    }
}
