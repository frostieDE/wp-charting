using System.Windows;
using Microsoft.Phone.Controls;

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
    }
}