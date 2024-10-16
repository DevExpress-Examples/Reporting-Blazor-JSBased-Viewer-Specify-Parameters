﻿#region usings
using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Extensions;
using System.Web;
#endregion

namespace BlazorApp.Services {
    public class CustomReportStorageWebExtension : ReportStorageWebExtension {
        readonly string ReportDirectory;
        const string FileExtension = ".repx";
        public CustomReportStorageWebExtension(IWebHostEnvironment env) {
            ReportDirectory = Path.Combine(env.ContentRootPath, "Reports");
            if (!Directory.Exists(ReportDirectory)) {
                Directory.CreateDirectory(ReportDirectory);
            }
        }

        private bool IsWithinReportsFolder(string url, string folder) {
            var rootDirectory = new DirectoryInfo(folder);
            var fileInfo = new FileInfo(Path.Combine(folder, url));
            return fileInfo.Directory.FullName.ToLower().StartsWith(rootDirectory.FullName.ToLower());
        }

        public override bool CanSetData(string url) {
            // Determines whether it is possible to store a report by a given URL. 
            // For instance, make the CanSetData method return false for reports
            // that should be read-only in your storage. 
            // This method is called only for valid URLs
            // (if the IsValidUrl method returns true) before calling the SetData method.

            return true;
        }

        public override bool IsValidUrl(string url) {
            // Determines whether the URL passed to the current Report Storage is valid. 
            // For instance, implement your own logic to prohibit URLs that contain white spaces
            // or other special characters. 
            // This method is called before the CanSetData and GetData methods.

            return Path.GetFileName(url) == url;
        }

        #region parseUrlAndApplyParametersToReport
        public override byte[] GetData(string url) {
            try {
                // Parse the string with the report name and parameter values.
                string[] parts = url.Split('?');
                string reportName = parts[0];
                string parametersQueryString = parts.Length > 1 ? parts[1] : String.Empty;

                // Create a report instance.
                XtraReport report = null;

                if (Directory.EnumerateFiles(ReportDirectory).
                    Select(Path.GetFileNameWithoutExtension).Contains(reportName)) {
                    byte[] reportBytes = File.ReadAllBytes(Path.Combine(ReportDirectory, reportName + FileExtension));
                    using (MemoryStream ms = new MemoryStream(reportBytes))
                        report = XtraReport.FromStream(ms);
                }

                if (report != null) {
                    // Apply the parameter values to the report.
                    var parameters = HttpUtility.ParseQueryString(parametersQueryString);

                    foreach (string parameterName in parameters.AllKeys) {
                        report.Parameters[parameterName].Value = Convert.ChangeType(
                            parameters.Get(parameterName), report.Parameters[parameterName].Type);
                    }

                    // Disable the Visible property for all report parameters
                    // to hide the Parameters Panel in the viewer.
                    foreach (var parameter in report.Parameters) {
                        parameter.Visible = false;
                    }

                    // If you do not hide the panel, disable the report's RequestParameters property.
                    // report.RequestParameters = false;

                    using (MemoryStream ms = new MemoryStream()) {
                        report.SaveLayoutToXml(ms);
                        return ms.ToArray();
                    }
                }
            } catch (Exception ex) {
                throw new DevExpress.XtraReports.Web.ClientControls.FaultException(
                    "Could not get report data.", ex);
            }
            throw new DevExpress.XtraReports.Web.ClientControls.FaultException(
                string.Format("Could not find report '{0}'.", url));
        }
        #endregion

        public override Dictionary<string, string> GetUrls() {
            // Returns a dictionary of the existing report URLs and display names. 
            // This method is called when running the Report Designer, 
            // before the Open Report and Save Report dialogs are shown
            // and after a new report is saved to storage.

            return Directory.GetFiles(ReportDirectory, "*" + FileExtension)
                                    .Select(Path.GetFileNameWithoutExtension)
                                    .ToDictionary<string, string>(x => x);
        }

        public override void SetData(XtraReport report, string url) {
            // Stores the specified report to a Report Storage using the specified URL. 
            // This method is called only after the IsValidUrl and CanSetData methods are called.
            if (!IsWithinReportsFolder(url, ReportDirectory))
                throw new DevExpress.XtraReports.Web.ClientControls.FaultException(
                    "Invalid report name.");
            report.SaveLayoutToXml(Path.Combine(ReportDirectory, url + FileExtension));
        }

        public override string SetNewData(XtraReport report, string defaultUrl) {
            // Stores the specified report using a new URL. 
            // The IsValidUrl and CanSetData methods are never called before this method. 
            // You can validate and correct the specified URL directly
            // in the SetNewData method implementation 
            // and return the resulting URL used to save a report in your storage.
            SetData(report, defaultUrl);
            return defaultUrl;
        }
    }
}
