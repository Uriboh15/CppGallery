using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Animation;

namespace CppGallery.Pages.UserControls
{
    internal class MacroPanel : BlockPanel
    {
        public string Folder { get; set; } = string.Empty;

        public MacroPanel()
        {
            Header = PanelHeader.Macro;
            Loaded += MacroPanel_Loaded;
        }

        private async void MacroPanel_Loaded(object sender, RoutedEventArgs e)
        {
            await UserAPI.GetMacroValue(Children, Folder);

            
            if (Children.Count == 1)
            {
                
                var parent = Parent;

                var outerPanel = Parent as Panel;

                outerPanel.Children.Remove(this);

                if(outerPanel.Children.Count == 0)
                {
                    while (parent is not Page)
                    {
                        parent = (parent as FrameworkElement).Parent;
                    }
                    var page = parent as Page;

                    string message = "C" + (App.UseCppInCSample ? "++20" : "17") + " では何も定義されていません";

                    if (App.UseCppInCSample)
                    {
                        var resultPanel = new ResultsPanel
                        {
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                        };
                        resultPanel.Children.Add(new TextBlock
                        {
                            Text = message,
                            HorizontalAlignment = HorizontalAlignment.Center,
                        });
                        var button = new Button
                        {
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Content = "設定へ移動",
                        };
                        button.Click += (s, e) =>
                        {
                            MainWindow.Handle.OpenSettings();
                        };

                        resultPanel.Children.Add(button);

                        page.Content = resultPanel;
                    }
                    else
                    {
                        

                        page.Content = new TextBlock
                        {
                            Text = message,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                        };
                    }
                    

                }

                


            }

            //throw new Exception();
        }
    }
}
