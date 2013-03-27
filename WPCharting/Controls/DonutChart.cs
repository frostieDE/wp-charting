using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPCharting.Controls
{
    public class DonutChart : PieChart
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.SumOfValues > 0)
            {
                Grid grid = base.GetTemplateChild("PART_Grid") as Grid;

                if (grid != null)
                {
                    grid.Children.Add(new Ellipse { Width = this.Width * 0.4, Height = this.Width * 0.4, Fill = new SolidColorBrush(Colors.Black) });
                }
            }
        }
    }
}
