using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IOCO.Demo.Behaviors
{
    public sealed class ItemTappedCommandListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
            BindableProperty.CreateAttached(
                "ItemTappedCommand",
                typeof(ICommand),
                typeof(ItemTappedCommandListView),
                default(ICommand),
                BindingMode.OneWay,
                null,
                PropertyChanged);

        static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ListView listView)
            {
                listView.ItemTapped -= ListViewOnItemTapped;
                listView.ItemTapped += ListViewOnItemTapped;
            }
        }

        static void ListViewOnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView list && list.IsEnabled && !list.IsRefreshing)
            {

                var command = GetItemTappedCommand(list);
                if (command != null && command.CanExecute(e.Item))
                {
                    command.Execute(e.Item);
                }
                list.SelectedItem = null;
            }
        }

        public static ICommand GetItemTappedCommand(BindableObject bindableObject) => (ICommand)bindableObject.GetValue(ItemTappedCommandProperty);

        public static void SetItemTappedCommand(BindableObject bindableObject, object value) => bindableObject.SetValue(ItemTappedCommandProperty, value);
    }
}
