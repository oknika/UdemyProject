using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace MyMemo.MnTools
{
    /// <summary>
    /// Interaction logic for WinLogin.xaml
    /// </summary>
    public partial class WinLogin : Window
    {
        string selectedUser;

        public WinLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUser.SelectedIndex < 0)
            {
                MessageBox.Show("Please select the username.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtPwd.Password))
            {
                MessageBox.Show("Please enter the password.");
                return;
            }

            //MessageBox.Show(Properties.Settings.Default[selectedUser + "_pwd"].ToString());
            if (txtPwd.Password != Properties.Settings.Default[selectedUser + "_pwd"].ToString())
            {
                MessageBox.Show("Password is incorrect!","Incorrect password",MessageBoxButton.OK,MessageBoxImage.Stop);
                return;
            }
            else
            {
                //MessageBox.Show($"Welcome {Properties.Settings.Default[selectedUser]}!");
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void cmbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = cmbUser.SelectedItem as ComboBoxItem;
            selectedUser = selectedItem?.Content.ToString();

            string fn = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Img", "ProfilePic", selectedUser + ".jpg");
            try
            {
                LoadImage(fn);
            }
            catch (FileNotFoundException)
            {
                imgUser.Source = null; // Clear image if file not found
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        private void LoadImage(string fileName)
        {
            BitmapImage bitmapPict = new BitmapImage();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            bitmapPict.BeginInit();
            bitmapPict.CacheOption = BitmapCacheOption.OnLoad;
            bitmapPict.StreamSource = fs;
            bitmapPict.EndInit();

            imgUser.Source = bitmapPict;
            bitmapPict = null;
            fs.Dispose();
        }
    }
}
