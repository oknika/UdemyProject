using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
    /// Interaction logic for WinUsers.xaml
    /// </summary>
    public partial class WinUsers : Window
    {
        public WinUsers()
        {
            InitializeComponent();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            int selIndex;
            selIndex = cmbUser.SelectedIndex;
            if (selIndex < 0)
            {
                MessageBox.Show("Please select a user to save changes.");
                return;
            }

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Username cannot be empty.");
                return;
            }

            if (string.IsNullOrEmpty(txtPwd.Password))
            {
                MessageBox.Show("Password cannot be empty.");
                return;
            }

            if (txtPwd.Password!= txtRepeatPwd.Password)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Save changes logic here
            var selectedItem = cmbUser.SelectedItem as ComboBoxItem;
            string selectedUser = selectedItem?.Content.ToString();
            MyMemo.Properties.Settings.Default[selectedUser] = txtUsername.Text;
            MyMemo.Properties.Settings.Default[selectedUser + "_pwd"] = txtPwd.Password;
            MyMemo.Properties.Settings.Default.Save();

            if (imgUser.Source != null)
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                BitmapSource bitmapSource = (BitmapSource)imgUser.Source;
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                string fn;
                fn = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Resources", "Img", "ProfilePic", selectedUser + ".jpg");
                using (FileStream fileStream = new FileStream(fn, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }

            txtRepeatPwd.Clear();
            MessageBox.Show("Changes saved successfully.");
        }

        private void cmbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtRepeatPwd.Clear();
            if (cmbUser.SelectedIndex < 0)
                return;

            var selectedItem = cmbUser.SelectedItem as ComboBoxItem;
            string selectedUser = selectedItem?.Content.ToString();
            txtUsername.Text = MyMemo.Properties.Settings.Default[selectedUser]?.ToString();
            txtPwd.Password = MyMemo.Properties.Settings.Default[selectedUser + "_pwd"]?.ToString();

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

        private void btnBrowseFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Jpg files|*.jpg";
            openFileDialog.Title = "Select a profile picture";
            openFileDialog.ShowDialog();
            
            if (openFileDialog.FileName == "")
            {
                return;
            }

            LoadImage(openFileDialog.FileName);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbUser.SelectedIndex = 0;
        }
    }
}
