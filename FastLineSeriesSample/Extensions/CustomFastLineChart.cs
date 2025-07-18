using Syncfusion.Maui.Toolkit.Charts;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FastLineSeriesSample
{
    public class CustomFastLineChart : SfCartesianChart
    {
        #region Bindable Properties

        /// <summary>
        /// Identifies the ItemsSource bindable property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(CustomFastLineChart), null,
                propertyChanged: OnItemsSourcePropertyChanged);

        /// <summary>
        /// Identifies the XBindingPath bindable property.
        /// </summary>
        public static readonly BindableProperty XBindingPathProperty =
            BindableProperty.Create(nameof(XBindingPath), typeof(string), typeof(CustomFastLineChart), null,
                propertyChanged: OnBindingPathPropertyChanged);

        /// <summary>
        /// Identifies the YBindingPath bindable property.
        /// </summary>
        public static readonly BindableProperty YBindingPathProperty =
            BindableProperty.Create(nameof(YBindingPath), typeof(string), typeof(CustomFastLineChart), null,
                propertyChanged: OnBindingPathPropertyChanged);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the items source for the chart
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the XBindingPath for data points
        /// </summary>
        public string XBindingPath
        {
            get { return (string)GetValue(XBindingPathProperty); }
            set { SetValue(XBindingPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the YBindingPath for data points
        /// </summary>
        public string YBindingPath
        {
            get { return (string)GetValue(YBindingPathProperty); }
            set { SetValue(YBindingPathProperty, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CustomFastLineChart class.
        /// </summary>
        public CustomFastLineChart()
        {

        }

        #endregion

        #region Methods

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var chart = bindable as CustomFastLineChart;

            if (oldValue is INotifyCollectionChanged oldCollection && chart != null)
            {
                oldCollection.CollectionChanged -= chart.OnCollectionChanged;
            }

            if (newValue is INotifyCollectionChanged newCollection && chart != null)
            {
                newCollection.CollectionChanged += chart.OnCollectionChanged;
            }

            chart?.GenerateSeries();
        }

        /// <summary>
        /// Called when the XBindingPath or YBindingPath property changes.
        /// </summary>
        private static void OnBindingPathPropertyChanged(BindableObject bindable, object _, object __)
        {
            var chart = bindable as CustomFastLineChart;
            chart?.GenerateSeries();
        }

        /// <summary>
        /// Called when the collection contents change.
        /// </summary>
        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            GenerateSeries();
        }

        private void GenerateSeries()
        {
            if (ItemsSource == null || string.IsNullOrEmpty(XBindingPath) || string.IsNullOrEmpty(YBindingPath))
                return;

            Series.Clear();

            // Split the data into segments based on NaN values
            var segments = SplitDataByNaNValues();

            // Create FastLineSeries for each segment with valid values
            foreach (var segment in segments)
            {
                if (segment.Count > 0)
                {
                    var fastLineSeries = new FastLineSeries
                    {
                        ItemsSource = segment,
                        XBindingPath = XBindingPath,
                        YBindingPath = YBindingPath,
                        StrokeWidth = 2,
                        Fill = Colors.RoyalBlue
                    };

                    Series.Add(fastLineSeries);
                }
            }
        }

        /// <summary>
        /// Splits the data into segments based on NaN values.
        /// </summary>
        private List<ObservableCollection<object>> SplitDataByNaNValues()
        {
            var segments = new List<ObservableCollection<object>>();
            var currentSegment = new ObservableCollection<object>();

            foreach (var item in ItemsSource)
            {
                // Get the Y value using reflection
                var property = item.GetType().GetProperty(YBindingPath);
                var yValue = property?.GetValue(item);

                // Check if the value is NaN
                bool isNaN = yValue != null &&
                           yValue is double doubleValue &&
                           double.IsNaN(doubleValue);

                if (isNaN)
                {
                    // When we hit a NaN value, finalize the current segment if it has items
                    if (currentSegment.Count > 0)
                    {
                        segments.Add(currentSegment);
                        currentSegment = new ObservableCollection<object>();
                    }
                }
                else
                {
                    // Add the item to the current segment
                    currentSegment.Add(item);
                }
            }

            // Add the last segment if it has items
            if (currentSegment.Count > 0)
            {
                segments.Add(currentSegment);
            }

            return segments;
        }

        #endregion
    }
}
