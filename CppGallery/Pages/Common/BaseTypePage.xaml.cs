using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Animation;
using CppGallery.Pages.UserControls;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BaseTypePage : Page
    {
        public BaseTypePage()
        {
            this.InitializeComponent();
        }

        private async void BasicTypePanel_Loaded(object sender, RoutedEventArgs e)
        {

            await CoAPI.ExecuteAsync(Data.DefaultSampleExePath + UserAPI.GetExeName(false).Replace("/", "") + " basictype basictype");

            
            using (var sr = new StreamReader(CoAPI.GetOutputFileName()))
            {
                var output = sr.ReadToEnd().Split(Environment.NewLine);
                //Children[0] ‚Í TextBlock
                for (int i = 1; i < BasicTypePanel.Children.Count; ++i)
                {
                    (BasicTypePanel.Children[i] as ItemGrid).Value = output[i - 1];
                }
            }
            
        }
    }
}
