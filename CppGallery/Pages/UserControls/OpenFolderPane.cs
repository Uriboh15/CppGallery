using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRT.Interop;

namespace CppGallery.Pages.UserControls
{
    public class OpenFolderPane : ControlGrid
    {
        private Button OpenButton { get; } = new Button
        {
            Content = "開く",
            Width = 65,
        };

        private bool Working = false;

        public OpenFolderPane()
        {
            this.Content = OpenButton;
            this.Message = "フォルダーを開く";
            OpenButton.Click += OpenButton_Click;
        }

        private async void OpenButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (Working) return;

            Working = true;
            Windows.Storage.Pickers.FolderPicker Picker = new Windows.Storage.Pickers.FolderPicker();
            Picker.FileTypeFilter.Add("*");
            InitializeWithWindow.Initialize(Picker, WindowNative.GetWindowHandle(MainWindow.Handle));

            Windows.Storage.StorageFolder folder = await Picker.PickSingleFolderAsync();

            if (folder != null)
            {
                FunctionExpander.Execute(this, folder.Path, "プログラムの応答時間が長すぎるため、結果を表示できませんでした");
            }
            Working = false;
        }
    }
}
