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

        private void ClearForm()
        {
            txtMemoID.Clear();
            txtMemoTitle.Clear();
            CmbDate.SelectedDate = DateTime.Now;
            RtxBox.Document.Blocks.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearForm();
            ListboxMemo.SelectedIndex = -1;

            int lastMemoID = Properties.Settings.Default.MemoLastID;

            string fn;
            string[] lines;
            int memoID = 0;
            string titleMemo;
            string dateMemo;
            for (int i = 1; i <= lastMemoID; i++)
            {
                fn = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Docs", "Header_" + i + ".xyz");
                if (System.IO.File.Exists(fn))
                {
                    lines = System.IO.File.ReadAllLines(fn);

                    memoID = Convert.ToInt32(lines[0].Replace("ID:", "").Trim());
                    titleMemo = lines[1].Replace("Title:", "").Trim();
                    dateMemo = lines[2].Replace("Date:", "").Trim();

                    ListBoxItem item = new ListBoxItem();
                    item.Content = titleMemo;
                    item.Tag = new MemoInfo
                    {
                        ID = memoID,
                        Title = titleMemo,
                        Date = dateMemo
                    };

                    ListboxMemo.Items.Add(item);
                }
            }
        }

        private void ListboxMemo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearForm();
            if (ListboxMemo.SelectedItem is ListBoxItem selected &&
                selected.Tag is MemoInfo memo)
            {
                txtMemoID.Text = memo.ID.ToString();
                txtMemoTitle.Text = memo.Title;
                CmbDate.Text = memo.Date;

                string fn = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Docs", "Rtf_" + memo.ID + ".xyz");
                if (System.IO.File.Exists(fn))
                {
                    try
                    {
                        using (System.IO.FileStream reader = new System.IO.FileStream(fn, System.IO.FileMode.Open))
                        {
                            TextRange range = new TextRange(RtxBox.Document.ContentStart, RtxBox.Document.ContentEnd);
                            range.Load(reader, DataFormats.Rtf);
                            reader.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading memo content: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}
