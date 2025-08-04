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
    /// Interaction logic for WinMemo.xaml
    /// </summary>
    public partial class WinMemo : Window
    {
        public WinMemo()
        {
            InitializeComponent();
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.AlignLeft.Execute(null, RtxBox);
        }

        private void btnCenter_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.AlignCenter.Execute(null, RtxBox);
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.AlignRight.Execute(null, RtxBox);
        }

        private void btnJustify_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.AlignJustify.Execute(null, RtxBox);
        }

        private void btnIndent_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.IncreaseIndentation.Execute(null, RtxBox);
        }

        private void btnOutdent_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.DecreaseIndentation.Execute(null, RtxBox);
        }

        private void btnBold_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleBold.Execute(null, RtxBox);
        }

        private void btnItalic_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleItalic.Execute(null, RtxBox);
        }

        private void btnUnderline_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleUnderline.Execute(null, RtxBox);
        }

        private void btnIncreaseFont_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.IncreaseFontSize.Execute(null, RtxBox);
        }

        private void btnDecreaseFont_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.DecreaseFontSize.Execute(null, RtxBox);
        }

        private void btnCut_Click(object sender, RoutedEventArgs e)
        {
            RtxBox.Cut();
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            RtxBox.Copy();
        }

        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            RtxBox.Paste();
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            RtxBox.Undo();
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            RtxBox.Redo();
        }

        private void btnAddMemo_Click(object sender, RoutedEventArgs e)
        {
            ResetForm(false);
            txtMemoID.Text = (Properties.Settings.Default.MemoLastID + 1).ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }

        private void ResetForm(bool AfterSave = true)
        {
            CntGrpBox.IsEnabled = !AfterSave;
            btnSaveMemo.IsEnabled = !AfterSave;
            btnAddMemo.IsEnabled = AfterSave;

            RtxBox.Document.Blocks.Clear();
            txtMemoTitle.Clear();
            txtMemoID.Clear();
            CmbDate.SelectedDate = DateTime.Now;
        }

        private void btnSaveMemo_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }
    }
}
