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

namespace MyNotepad
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private string? _currentFilePath; // Holds the path of the currently opened file
        public Main()
        {
            InitializeComponent();
            smnSave.IsEnabled = false; // Initially disable the Save menu item
        }

        private void smnAddNew_Click(object sender, RoutedEventArgs e)
        {
            // Clear the current text box and reset the file path
            _currentFilePath = null;
            mainTxtbox.Clear();
            smnSave.IsEnabled = false;
        }

        private void smnExit_Click(object sender, RoutedEventArgs e)
        {
            // Confirm exit
            App.Current.Shutdown();
        }

        private void smnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            // Open a SaveFileDialog to save the current text
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Save file as",
                AddExtension = true,
                Filter = "Text Documents|*.txt",
                DefaultExt = ".txt"
            };
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, mainTxtbox.Text,Encoding.UTF8);
                // Update the current file path and enable the Save menu item
                _currentFilePath = saveFileDialog.FileName;
                smnSave.IsEnabled = !string.IsNullOrEmpty(_currentFilePath);
            }
        }

        private void smnOpen_Click(object sender, RoutedEventArgs e)
        {
            // Open a OpenFileDialog to select a file to open
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Open file",
                Filter = "Text Documents|*.txt",
                DefaultExt = ".txt"
            };
            openFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                try
                {
                    // Read the file content and update the text box
                    _currentFilePath = openFileDialog.FileName;
                    mainTxtbox.Text = System.IO.File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                    // Enable the Save menu item since a file is now open
                    smnSave.IsEnabled = !string.IsNullOrEmpty(_currentFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void smnSave_Click(object sender, RoutedEventArgs e)
        {
            // Save the current text to the file path
            System.IO.File.WriteAllText(_currentFilePath, mainTxtbox.Text, Encoding.UTF8);
        }

        private void smnUndo_Click(object sender, RoutedEventArgs e)
        {
            // Undo the last action in the text box
            if (mainTxtbox.CanUndo)
            {
                mainTxtbox.Undo();
            }
        }

        private void smnRedo_Click(object sender, RoutedEventArgs e)
        {
            // Redo the last undone action in the text box
            if (mainTxtbox.CanRedo)
            {
                mainTxtbox.Redo();
            }
        }

        private void smnCopy_Click(object sender, RoutedEventArgs e)
        {
            // Copy the selected text to the clipboard
            //if (!string.IsNullOrEmpty(mainTxtbox.SelectedText))
            //{
            //    Clipboard.SetText(mainTxtbox.SelectedText);
            //}
            mainTxtbox.Copy();
        }

        private void smnCut_Click(object sender, RoutedEventArgs e)
        {
            // Cut the selected text and copy it to the clipboard
            //if (!string.IsNullOrEmpty(mainTxtbox.SelectedText))
            //{
            //    Clipboard.SetText(mainTxtbox.SelectedText);
            //    mainTxtbox.SelectedText = string.Empty; // Remove the selected text from the text box
            //}
            mainTxtbox.Cut();
        }

        private void smnPaste_Click(object sender, RoutedEventArgs e)
        {
            // Paste the text from the clipboard into the text box
            //if (Clipboard.ContainsText())
            //{
            //    string clipboardText = Clipboard.GetText();
            //    int selectionStart = mainTxtbox.SelectionStart;
            //    mainTxtbox.Text = mainTxtbox.Text.Insert(selectionStart, clipboardText);
            //    mainTxtbox.SelectionStart = selectionStart + clipboardText.Length; // Move the cursor after the pasted text
            //}
            mainTxtbox.Paste();
        }

        private void smnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            // Select all text in the text box
            mainTxtbox.SelectAll();
        }

        private void smnWordWrap_Click(object sender, RoutedEventArgs e)
        {
            // Toggle word wrap in the text box
            if (smnWordWrap.IsChecked == true)
            {
                mainTxtbox.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                mainTxtbox.TextWrapping = TextWrapping.NoWrap;
            }
        }

        private void smnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            // Increase the font size of the text box
            if (mainTxtbox.FontSize < 72) // Prevent font size from going above 72
            {
                mainTxtbox.FontSize += 2; // Increase font size by 2 points
            }
        }

        private void smnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            // Decrease the font size of the text box
            if (mainTxtbox.FontSize > 2) // Prevent font size from going below 2
            {
                mainTxtbox.FontSize -= 2; // Decrease font size by 2 points
            }
        }

        private void smnZoomDef_Click(object sender, RoutedEventArgs e)
        {
            mainTxtbox.FontSize = 12; // Reset font size to default
        }

        private void MnAbout_Click(object sender, RoutedEventArgs e)
        {
            MyNotepad.AboutWind aboutWindow = new AboutWind();
            aboutWindow.ShowDialog();
        }
    }
}
