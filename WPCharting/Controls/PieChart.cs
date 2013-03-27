using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WPCharting.Helper;

namespace WPCharting.Controls
{
    public class PieChart : ContentControl
    {
        #region Properties
        private IEnumerable<ChartItem> _items;
        private double _sumOfValues = 0;
        private AccentColors _colors = new AccentColors();

        protected double SumOfValues
        {
            get
            {
                return this._sumOfValues;
            }
        }
        #endregion

        #region Dependency Properties
        #region ItemsSource Dependency Property
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(PieChart), new PropertyMetadata(null, OnItemsSourceChanged));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)base.GetValue(ItemsSourceProperty); }
            set { base.SetValue(ItemsSourceProperty, value); this.LoadItems(); }
        }

        private static void OnItemsSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((PieChart)obj).OnItemsSourceChanged();
        }

        private void OnItemsSourceChanged()
        {
            this.LoadItems();
        }

        #endregion

        #region LegendVisible Dependency Property
        public static readonly DependencyProperty LegendVisibleProperty = DependencyProperty.Register("LegendVisible", typeof(Visibility), typeof(PieChart), new PropertyMetadata(Visibility.Visible));

        public Visibility LegendVisible
        {
            get { return (Visibility)base.GetValue(LegendVisibleProperty); }
            set { base.SetValue(LegendVisibleProperty, value); }
        }

        #endregion

        #region CaptionMemberPath
        public static readonly DependencyProperty CaptionMemberPathProperty = DependencyProperty.Register("CaptionMemberPath", typeof(string), typeof(PieChart), new PropertyMetadata("Caption"));

        public string CaptionMemberPath
        {
            get { return (string)base.GetValue(CaptionMemberPathProperty); }
            set { base.SetValue(CaptionMemberPathProperty, value); }
        }
        #endregion

        #region ValueMemberPath
        public static readonly DependencyProperty ValueMemberPathProperty = DependencyProperty.Register("ValueMemberPath", typeof(string), typeof(PieChart), new PropertyMetadata("Value"));

        public string ValueMemberPath
        {
            get { return (string)base.GetValue(ValueMemberPathProperty); }
            set { base.SetValue(ValueMemberPathProperty, value); }
        }
        #endregion

        #region LegendItemTemplate
        public static readonly DependencyProperty LegendItemTemplateProperty = DependencyProperty.Register("LegendItemTemplate", typeof(DataTemplate), typeof(PieChart), null);

        public DataTemplate LegendItemTemplate
        {
            get { return (DataTemplate)base.GetValue(LegendItemTemplateProperty); }
            set { base.SetValue(LegendItemTemplateProperty, value); }
        }
        #endregion

        #endregion

        #region Constructor
        public PieChart()
        {
            DefaultStyleKey = typeof(PieChart);
        }
        #endregion

        #region OnApplyTemplate
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Grid grid = base.GetTemplateChild("PART_Grid") as Grid;
            ItemsControl panel = base.GetTemplateChild("PART_Legend") as ItemsControl;

            if (grid != null && panel != null && this._items != null && this.SumOfValues > 0)
            {
                double width = this.Width;
                double lastAngle = 0;
                double startX = this.Width / 2;
                double startY = 0;

                int i = 0;

                List<UIElement> elements = new List<UIElement>();

                foreach (ChartItem item in this._items)
                {
                    double _percent = (double)item.Value / this._sumOfValues;
                    double _angle = lastAngle + 360 * _percent;
                    double d = Math.PI * _angle / 180;

                    double dX = Math.Sin(d) * width / 2;
                    double dY = width / 2 - Math.Cos(d) * width / 2;

                    elements.Add(this.CreatePath(startX + dX, startY + dY, _angle > 180, i));

                    item.Percent = _percent;
                    item.Color = this._colors[i % this._colors.Count];

                    lastAngle = _angle;
                    ++i;
                }

                elements.Reverse();
                grid.Children.Clear();

                foreach (UIElement elem in elements)
                {
                    grid.Children.Add(elem);
                }

                panel.ItemsSource = this._items;
            }
        }
        #endregion

        #region CreatePath
        private UIElement CreatePath(double endX, double endY, bool largeArc, int index)
        {
            double startX = this.Width / 2, startY = 0;

            if (Math.Round(startX) == Math.Round(endX) && Math.Round(startY) == Math.Round(endY))
            {
                return new Ellipse { Width = this.Width, Height = this.Width, Fill = this._colors[index % this._colors.Count] };
            }

            double radius = this.Width / 2d;

            LineSegment ls1 = new LineSegment { Point = new Point(startX, startY) };
            ArcSegment as1 = new ArcSegment { Point = new Point(endX, endY), SweepDirection = SweepDirection.Clockwise, Size = new Size(radius, radius), IsLargeArc = largeArc };
            LineSegment ls2 = new LineSegment { Point = new Point(radius, radius) };

            PathFigure pf = new PathFigure { StartPoint = new Point(startX, startY) };
            pf.Segments.Add(ls1);
            pf.Segments.Add(as1);
            pf.Segments.Add(ls2);

            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);

            return new Path { Width = this.Width, Height = this.Height, Fill = this._colors[index % this._colors.Count], Data = pg };
        }
        #endregion

        #region GetValue
        private static object GetValueHelper(object obj, string member, object defaultValue)
        {
            PropertyInfo info = obj.GetType().GetProperty(member);

            if (info != null)
            {
                return info.GetValue(obj, null);
            }

            return defaultValue;
        }
        #endregion

        #region LoadItems
        private void LoadItems()
        {
            this.LoadItems(true);
        }

        private void LoadItems(bool subscribe)
        {
            List<ChartItem> items = new List<ChartItem>();
            double sum = 0;

            foreach (object item in this.ItemsSource)
            {
                try
                {
                    ChartItem _item = new ChartItem();
                    _item.Caption = PieChart.GetValueHelper(item, this.CaptionMemberPath, "").ToString();

                    var res = PieChart.GetValueHelper(item, this.ValueMemberPath, 0);
                    _item.Value = Convert.ToDouble(res);

                    sum += _item.Value;
                    items.Add(_item);
                }
                catch { }
            }

            this._items = items;
            this._sumOfValues = sum;

            /* Check if ItemsSource implements INotifyPropertyChanged-Interface
             * so we can hook the PropertyChanged event in order to update
             * the control
             */
            if (subscribe && this.ItemsSource is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)this.ItemsSource).PropertyChanged += new PropertyChangedEventHandler(PieChart_PropertyChanged);
            }

            // Apply changes to the control
            this.OnApplyTemplate();
        }

        private void PieChart_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Reload all items
            this.LoadItems(false);
        }
        #endregion
    }
}
