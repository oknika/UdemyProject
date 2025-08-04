using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyMemo.MnAbout
{
    /// <summary>
    /// Interaction logic for WinAbout.xaml
    /// </summary>
    public partial class WinAbout : Window
    {
        public WinAbout()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            lblCompany.Content = versionInfo.CompanyName ?? "MyMemo Company";
            lblProductName.Content = versionInfo.ProductName ?? "MyMemo Product";
            lblVersion.Content = versionInfo.FileVersion ?? "2025.1.0";
            lblAuthors.Content = "MyMemo Team";

            string description = versionInfo.Comments ?? "MyMemo Application for note-taking and organization.";
            var tb = new TextBlock
            {
                Text = description,
                TextWrapping = TextWrapping.Wrap
            };
            lblDescription.Content = tb;

            lblCopyright.Content = versionInfo.LegalCopyright ?? "© 2025 MyMemo Team. All rights reserved.";

            var info = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            System.Diagnostics.Debug.WriteLine($"FileVersion: " + info.FileVersion);
            System.Diagnostics.Debug.WriteLine($"ProductVersion: " + info.ProductVersion);

            var attr = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            System.Diagnostics.Debug.WriteLine($"InformationalVersion: " + attr);

            //lblCompany.Content = Attribute.GetCustomAttribute(
            //    Assembly.GetExecutingAssembly(),
            //    typeof(AssemblyCompanyAttribute)) is AssemblyCompanyAttribute companyAttribute
            //    ? companyAttribute.Company
            //    : "MyMemo Company";
            //lblProductName.Content = Attribute.GetCustomAttribute(
            //    Assembly.GetExecutingAssembly(),
            //    typeof(AssemblyProductAttribute)) is AssemblyProductAttribute productAttribute
            //    ? productAttribute.Product
            //    : "MyMemo Product";
            //lblVersion.Content = $"{Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

            //string authorContent = Attribute.GetCustomAttribute(
            //    Assembly.GetExecutingAssembly(),
            //    typeof(AssemblyDescriptionAttribute)) is AssemblyDescriptionAttribute descriptionAttribute
            //    ? descriptionAttribute.Description
            //    : "MyMemo Application for note-taking and organization.";
            //var tb = new TextBlock
            //{
            //    Text = authorContent,
            //    TextWrapping = TextWrapping.Wrap
            //};
            //lblDescription.Content = tb;

            //lblCopyright.Content = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(),
            //    typeof(AssemblyCopyrightAttribute)) is AssemblyCopyrightAttribute copyrightAttribute
            //    ? copyrightAttribute.Copyright
            //    : "© 2025 MyMemo Team. All rights reserved.";
        }
    }
}
