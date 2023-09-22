using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRT.Interop;

namespace CppGallery.Pages.UserControls
{
    public class OpenButtonPane : ControlGrid
    {
        private bool Working = false;
        private Button Button { get; } = new Button
        {
            Content = "開く",
            Width = 65,
        };

        public string ButtonText
        {
            get { return Button.Content as string; }
            set { Button.Content = value; }
        }

        public OpenButtonPane()
        {
            this.Message = "ファイルを開く";

            Button.Click += OpenButton_Click;

            this.Content = Button;
        }

        private async void OpenButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (Working) return;

            Working = true;

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add("*");

            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(MainWindow.GetParentMainWindow(this)));

            Windows.Storage.StorageFile savefile = null;

            try
            {
                savefile = await picker.PickSingleFileAsync();
            }
            catch (InvalidCastException)
            {

            }
            
            if (savefile != null)
            {
                FunctionExpander.Execute(this, savefile.Path);
            }
            Working = false;
        }
    }
}
