using System.Windows;
using Microsoft.Phone.Controls;
using WPChartingTest.Models;

namespace WPChartingTest.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.DataContext = App.ViewModel;
        }

        private void ToggleLegend_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            chart1.LegendVisible = (chart1.LegendVisible == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed);
            chart2.LegendVisible = (chart2.LegendVisible == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed);
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModel.Items.Add(new Item { Caption = "Test " + (App.ViewModel.Items.Count + 1).ToString(), Value = 42 });
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (App.ViewModel.Items.Count > 0)
            {
                App.ViewModel.Items.Remove(App.ViewModel.Items[App.ViewModel.Items.Count - 1]);
            }
        }
    }
}