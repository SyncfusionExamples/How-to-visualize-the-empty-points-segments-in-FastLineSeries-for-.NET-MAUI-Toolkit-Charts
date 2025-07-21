# How to visualize the empty points segments in FastLineSeries for .NET MAUI Toolkit Charts

This article explains how to visualize gaps for empty points in the [FastLineSeries](https://help.syncfusion.com/maui-toolkit/cartesian-charts/fastline) of the .NET MAUI Toolkit [SfCartesianChart](https://www.syncfusion.com/maui-controls/maui-cartesian-charts).

By default, [FastLineSeries](https://help.syncfusion.com/cr/maui-toolkit/Syncfusion.Maui.Toolkit.Charts.FastLineSeries.html) excludes null or double.NaN values, as built-in support for [Empty Points](https://help.syncfusion.com/maui-toolkit/cartesian-charts/emptypoints) is limited in fast-rendering series types to maintain performance. As a result, representing visual breaks in fast line series data can be challenging.

Follow the step-by-step instructions to effectively address the limitation, ensuring both high rendering performance and visual clarity when plotting fast line charts with missing values.

**Step 1**: Create a **CustomFastLineChart** class by extending the [SfCartesianChart](https://help.syncfusion.com/cr/maui-toolkit/Syncfusion.Maui.Toolkit.Charts.SfCartesianChart.html) control.

**Step 2**: Define the required bindable properties: ItemsSource, XBindingPath, and YBindingPath, along with their corresponding property wrappers and property changed handlers. These properties allow the chart to bind dynamically to external data sources and respond to changes in the dataset or binding paths.

**Step 3**: Implement the **GenerateSeries** method to process the data source and render valid segments as individual [FastLineSeries](https://help.syncfusion.com/cr/maui-toolkit/Syncfusion.Maui.Toolkit.Charts.FastLineSeries.html) instances by adding them to the [Series](https://help.syncfusion.com/cr/maui-toolkit/Syncfusion.Maui.Toolkit.Charts.SfCartesianChart.html#Syncfusion_Maui_Toolkit_Charts_SfCartesianChart_Series) collection of the SfCartesianChart. This method is invoked whenever the ItemsSource, XBindingPath, or YBindingPath properties change.

**Step 4**: Construct the **SplitDataByNaNValues** method to split the data based on NaN values and generate valid individual segments, which are used within the GenerateSeries method to create separate FastLineSeries instances, as described in Step 3.

**Step 5**: Configure the CustomFastLineChart with chart axes in XAML by binding it to the data source and specifying the appropriate binding paths. For more detailed guidance on initializing a .NET MAUI Cartesian chart, refer to the official getting started [documentation](https://help.syncfusion.com/maui-toolkit/cartesian-charts/getting-started).

**Output**
<img width="1919" height="947" alt="Sample screenshot showing a fast line series with gaps for empty points" src="https://github.com/user-attachments/assets/e84dbe8b-94c3-4986-9eb9-4ba8d48db970" />

## Troubleshooting

### Path Too Long Exception

If you are facing a "Path too long" exception when building this example project, close Visual Studio and rename the repository to a shorter name before building the project.

Refer to the KB article on [how to visualize the empty points segments in FastLineSeries for .NET MAUI Toolkit Charts](https://support.syncfusion.com/kb/article/20659/how-to-visualize-the-empty-points-segments-in-fastlineseries-for-net-maui-toolkit-charts) for better understanding along with relevant code snippets.


