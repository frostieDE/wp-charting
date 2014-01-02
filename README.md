# WP Charting

Two simple controls: pie chart and donut chart.

Currently this control is available for Windows Phone 8 apps only. I do not plan to add a Windows Phone 7 version, but feel free to create one. This project is compatible with Visual Studio 2013.

# How to use

1. Get the control - I recommend using NuGet: https://www.nuget.org/packages/SidebarWP8
2. Import the namespace in your XAML: `xmlns:chart="clr-namespace:WPCharting.Controls;assembly=WPCharting"`
3. Include the control in the content area of your page: `<chart:PieChart ItemsSource="{Binding YourData}" Height="400" Width="400" CaptionMemberPath="Name" ValueMemberPath="Value" LegendItemTemplate="{StaticResource YourTemplate}" />`
	
You may take a look at the sample project which is included in the repository.

# Contribute
Feel free to contribute to this project: add issues or create pull request.

# Changelog
## 1.0.0:
* Initial implementation