<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/387723757/23.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1020317)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Reporting for Blazor (JavaScript-Based Document Viewer) - Specify Parameter Values

This example demonstrates how to submit parameter values on a button click.

## How to Run the Example

1. Download the project and update NuGet packages.
2. Build and run the project.
3. Navigate a the page that contains the document viewer.
4. Enter a parameter value in the editor and click the *Submit* button.

![](Images/specify-parameter-values-in-blazor-app.png)

## Files to Review

* [DocumentViewer.razor](CS/BlazorApp/Pages/DocumentViewer.razor)
* Report name resolution services:
	- [CustomReportProvider.cs](CS/BlazorApp/Services/CustomReportProvider.cs) (default)
	- [CustomReportStorageWebExtension.cs](CS/BlazorApp/Services/CustomReportStorageWebExtension.cs#L47)
## Documentation

* [Create a Blazor Reporting (JavaScript-Based) Application](https://docs.devexpress.com/XtraReports/401677)
* [Specify Parameter Values in a Blazor Reporting (JavaScript-Based) Application](https://docs.devexpress.com/XtraReports/403243)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=Reporting-Blazor-JSBased-Viewer-Specify-Parameters&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=Reporting-Blazor-JSBased-Viewer-Specify-Parameters&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
