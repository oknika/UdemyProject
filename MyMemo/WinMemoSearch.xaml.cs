using System;
using System.Collections.Generic;
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

namespace MyMemo
{
    /// <summary>
    /// Interaction logic for WinMemoSearch.xaml
    /// </summary>
    public partial class WinMemoSearch : Window
    {
        public WinMemoSearch()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int lastMemoID = Properties.Settings.Default.MemoLastID;

            string fn;
            for (int i = 1; i <= lastMemoID; i++)
            {
                fn = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Docs", "Header_" + txtMemoID.Text + ".xyz");
                ListBoxItem lb = new ListBoxItem();
            }
        }
    }
}
