using CppGallery.Pages.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.WindowsHeader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProcessEnvPage : Page
    {
        bool SearchPathWorking = false;
        public ProcessEnvPage()
        {
            this.InitializeComponent();
        }

        private async void SearchPathButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchPathWorking) return;
            SearchPathWorking = true;

            Windows.Storage.Pickers.FolderPicker Picker = new Windows.Storage.Pickers.FolderPicker();
            Picker.FileTypeFilter.Add("*");
            InitializeWithWindow.Initialize(Picker, WindowNative.GetWindowHandle(MainWindow.GetParentMainWindow(this)));

            Windows.Storage.StorageFolder folder = await Picker.PickSingleFolderAsync();

            if (folder != null)
            {
                SearchPathTextBlock.Text = folder.Path;
            }

            SearchPathWorking = false;
        }

        private void GetEnvironmentVariableStartButton_Click(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { SearchPathTextBlock.Text, SearchPathFileNameTextBox.Text, SearchPathExtensionTextBox.Text });
        }
    }
}
