using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class GrammarPane : UserControl
    {
        public CodeLanguage CodeLanguage { get; set; } = CodeLanguage.Cpp;
        public string Folder {  get; set; }
        public UIElementCollection Children => _resultsPanel.Children;
        ResultsPanel _resultsPanel { get; } = new();

        private Expander Expander { get; } = new Expander
        {
            IsExpanded = true,
            MinHeight = Data.ControlGridHeight,
            Padding = new Thickness(Data.FunctionExpanderPadding),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Stretch,
        };

        public GrammarPane() 
        {
            this.Loaded += GrammarPane_Loaded;

            SymbolBlockPanel symbolBlockPanel = new SymbolBlockPanel
            {
                Symbol = PanelHeader.TemplateGrammer,
            };

            _resultsPanel.Children.Add(new TextBlock
            {
                Text = "構文",
                FontSize = Data.FunctionTitleTextSize,
                FontWeight = new Windows.UI.Text.FontWeight(700),
            });

            

            symbolBlockPanel.Children.Add(Expander);

            Content = symbolBlockPanel;
            Expander.Content = _resultsPanel;
        }

        private void GrammarPane_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Loaded -= GrammarPane_Loaded;

            //ヘッダー設定
            object createHeader()
            {
                Grid grid = new Grid();

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto)});
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                FontIcon icon = new FontIcon
                {
                    Margin = new Thickness((Data.GalleryControlHeight - 24.0) / 2.0 - 16.0, 0, (Data.GalleryControlHeight - 24.0) / 2.0, 0),
                    Glyph = "\uE7C3",
                };
                TextBlock textBlock = new TextBlock
                {
                    Text = "宣言",
                };

                Grid.SetColumn(textBlock, 1);

                grid.Children.Add(icon);
                grid.Children.Add(textBlock);

                return grid;
            }

            Expander.Header = createHeader();

            //テンプレート引数の説明があればタイトルを追加
            if(_resultsPanel.Children.Count > 1)
            {
                _resultsPanel.Children.Insert(1, new TextBlock
                {
                    Text = "テンプレート引数",
                    FontSize = Data.FunctionTitleTextSize,
                    FontWeight = new Windows.UI.Text.FontWeight(700),
                    Margin = new Thickness(0, Data.CompactOuterPanelSpacing, 0, 0),
                });
            }
            

            //ソースを読み込み
            string path = Data.DefaultSamplePath + Folder + "/Grm.txt";

            CLanCodeButton cLanCodeButton = CodeLanguage switch
            {
                CodeLanguage.C => new CCodeButton { Path = path },
                CodeLanguage.Cpp => new CodeButton { Path = path },
                CodeLanguage.CppWin32 => new Win32CodeButton { Path = path },
                CodeLanguage.CppWinRT => new WinRTCodeButton { Path = path },
                _ => throw new NotImplementedException(),
            };

            _resultsPanel.Children.Insert(1, cLanCodeButton);

            
        }
    }
}
