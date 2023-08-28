using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRT.Interop;

namespace CppGallery.Pages.UserControls
{
    public class SaveButtonPane : ControlGrid
    {
        private bool Working = false;

        public SaveButtonPane()
        {
            this.Message = "ファイルを保存";
            var button = new Button
            {
                Content = "保存",
                Width = 65,
            };
            button.Click += SaveButton_Click;
            this.Content = button;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Working) return;
            Working = true;
            var savepicker = new Windows.Storage.Pickers.FileSavePicker();
            savepicker.SuggestedFileName = "sample";

            savepicker.FileTypeChoices.Add("テキストファイル", new List<string>() { ".txt" });

            InitializeWithWindow.Initialize(savepicker, WindowNative.GetWindowHandle(MainWindow.Handle));

            Windows.Storage.StorageFile savefile = await savepicker.PickSaveFileAsync();
            if (savefile != null)
            {
                FunctionExpander.Execute(this, savefile.Path);
            }
            Working = false;
        }
    }
}
