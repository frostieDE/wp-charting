using System.Collections.ObjectModel;
using WPChartingTest.Models;

namespace WPChartingTest.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Item> Items
        {
            get;
            private set;
        }

        public MainViewModel()
        {
            this.Items = new ObservableCollection<Item>();
            this.Items.Add(new Item { Caption = "Test 1", Value = 10 });
            this.Items.Add(new Item { Caption = "Test 2", Value = 60 });
        }
    }
}
