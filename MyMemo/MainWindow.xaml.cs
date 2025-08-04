using MyMemo.MnTools;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyMemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartTimer();
            UpdateCalendar();
        }

        //udpate calendar
        private void UpdateCalendar()
        {
            lblYear.Content = DateTime.Now.Year.ToString();
            lblMonth.Content = DateTime.Now.ToString("MMM");
            lblDay.Content = DateTime.Now.Day.ToString();
            lblDayNm.Content= DateTime.Now.ToString("dddd");
        }

        // timer
        private void StartTimer()
        {
            txtTime.Text = DateTime.Now.ToString("HH:mm:ss");

            DispatcherTimer dsTime = new DispatcherTimer();
            dsTime.Interval = TimeSpan.FromSeconds(1);
            dsTime.Tick += (s, e) =>
            {
                txtTime.Text = DateTime.Now.ToString("HH:mm:ss");
            };
            dsTime.Start();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rslt = MessageBox.Show("Are you sure you want to close the application?", "Confirm Close", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rslt == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void cmbBackground_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadBackgroundImage();
        }

        private void LoadBackgroundImage()
        {
            if (cmbBackground.SelectedItem is string fileName)
            {
                string imgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Img", fileName);
                Uri imgUri = new Uri(imgPath);
                ImageBrush imageBrush = new ImageBrush
                {
                    ImageSource = new BitmapImage(imgUri),
                };

                imageBrush.Stretch = rbFill.IsChecked == true ? Stretch.Fill :
                     rbUniform.IsChecked == true ? Stretch.Uniform :
                     Stretch.None;

                this.Background = imageBrush;

                Properties.Settings.Default.BgWind = fileName; // Save the selected background to settings
                Properties.Settings.Default.Save(); // Ensure settings are saved
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cnvAbout.Visibility = Visibility.Hidden;
            cnvTool.Visibility = Visibility.Hidden;
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Img");
            if (Directory.Exists(folderPath))
            {
                var imageFiles = Directory.GetFiles(folderPath, "*.jpg")
                                          .Select(f => System.IO.Path.GetFileName(f))
                                          .ToList();
                cmbBackground.ItemsSource = imageFiles;
            }
        
            string strDefaultBackground = Properties.Settings.Default.BgWind;
            //cmbBackground.SelectedIndex = 2; // Set default background
            cmbBackground.SelectedItem = strDefaultBackground; // Set default background from settings
            rbFill.IsChecked = true; // Set default stretch mode

            WinLogin nW = new WinLogin();
            nW.Owner = this;
            nW.ShowDialog();
            if (nW.DialogResult == true)
            {
               //
            }
            else
            {
                // User cancelled login, close the application
                this.Close();
            }
        }

        private void rbFill_Checked(object sender, RoutedEventArgs e)
        {
            LoadBackgroundImage();
        }

        private void rbUniform_Checked(object sender, RoutedEventArgs e)
        {
            LoadBackgroundImage();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            cnvTool.Visibility = Visibility.Hidden;
            if (cnvAbout.Visibility == Visibility.Hidden)
            {
                cnvAbout.Visibility = Visibility.Visible;
            }
            else
            {
                cnvAbout.Visibility = Visibility.Hidden;
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnTools_Click(object sender, RoutedEventArgs e)
        {
            cnvAbout.Visibility = Visibility.Hidden; 
            if (cnvTool.Visibility == Visibility.Hidden)
            {
                cnvTool.Visibility = Visibility.Visible;
            }
            else
            {
                cnvTool.Visibility = Visibility.Hidden;
            }
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            cnvTool.Visibility = Visibility.Hidden;
            MyMemo.MnTools.WinUsers nW = new MyMemo.MnTools.WinUsers();
            nW.Owner = this;
            nW.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            cnvTool.Visibility = Visibility.Hidden;
            MyMemo.MnTools.WinLogin nW = new MyMemo.MnTools.WinLogin();
            nW.Owner = this;
            nW.ShowDialog();
        }

        private void btnAboutSub_Click(object sender, RoutedEventArgs e)
        {
            cnvAbout.Visibility = Visibility.Hidden;
            MyMemo.MnAbout.WinAbout nW = new MyMemo.MnAbout.WinAbout();
            nW.Owner = this;
            nW.ShowDialog();
        }
    }
}