using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public enum PanelHeader
    {
        BaseClass,
        Class,
        Concepts,
        Constant,
        Constructor,
        DerivedClass,
        Enum,
        ErrorReporting,
        Exception,
        Function,
        GlobalstreamOjbect,
        Included,
        Including,
        Literal,
        Macro,
        Manipulator,
        Method,
        Object,
        Operator,
        ReferenceURL,
        Specialization,
        StaticMethod,
        Struct,
        Typedefs,
    }

    public class BlockPanel : BasicBlockPanel
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
           "Header", // Max という名前の……
           typeof(PanelHeader), // int 型の CLR プロパティを……
           typeof(BlockPanel), // クラスに登録するやで―
           new PropertyMetadata(PanelHeader.Function));

        public static readonly DependencyProperty IsDeclarationOnlyProperty = DependencyProperty.Register(
           "IsDeclarationOnly", // Max という名前の……
           typeof(bool), // int 型の CLR プロパティを……
           typeof(BlockPanel), // クラスに登録するやで―
           new PropertyMetadata(false));

        public bool IsDeclarationOnly
        {
            get { return (bool)GetValue(IsDeclarationOnlyProperty); }
            set { SetValue(IsDeclarationOnlyProperty, value); }
        }

        public PanelHeader Header
        {
            get { return (PanelHeader)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        

        public BlockPanel()
        {
            
            this.Loaded += BlockPanel_Loaded;
        }

        private void BlockPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.Header == PanelHeader.Included || this.Header == PanelHeader.Including)
            {
                (this.Parent as StackPanel).Children.Remove(this);
                return;
            }

            ApplyHead();


            if (this.IsDeclarationOnly)
            {
                this._headText.Text += " (宣言のみ)";
            }
        }

        

        private void ApplyHead()
        {
            switch (this.Header)
            {
                case PanelHeader.BaseClass:
                    _headText.Text = "基底クラス";
                    break;

                case PanelHeader.Class:
                    _headText.Text = "クラス";
                    break;

                case PanelHeader.Concepts:
                    _headText.Text = "Concepts";
                    break;

                case PanelHeader.Constant:
                    _headText.Text = "定数値";
                    break;

                case PanelHeader.Constructor:
                    _headText.Text = "コンストラクター";
                    break;

                case PanelHeader.DerivedClass:
                    _headText.Text = "派生クラス";
                    break;

                case PanelHeader.Enum:
                    _headText.Text = "列挙型";
                    break;

                case PanelHeader.ErrorReporting:
                    _headText.Text = "エラー報告";
                    break;

                case PanelHeader.Exception:
                    _headText.Text = "例外クラス";
                    break;

                case PanelHeader.Function:
                    _headText.Text = "関数";
                    break;

                case PanelHeader.GlobalstreamOjbect:
                    _headText.Text = "グローバルストリームオブジェクト";
                    break;

                case PanelHeader.Included:
                    _headText.Text = "このヘッダーを include しているライブラリ";
                    break;

                case PanelHeader.Including:
                    _headText.Text = "このヘッダーに含まれるライブラリ";
                    break;

                case PanelHeader.Literal:
                    _headText.Text = "リテラル";
                    break;

                case PanelHeader.Macro:
                    _headText.Text = "マクロ";
                    break;

                case PanelHeader.Manipulator:
                    _headText.Text = "マニピュレーター";
                    break;

                case PanelHeader.Method:
                    _headText.Text = "メンバー関数";
                    break;

                case PanelHeader.Object:
                    _headText.Text = "オブジェクト";
                    break;

                case PanelHeader.Operator:
                    _headText.Text = "演算子";
                    break;

                case PanelHeader.ReferenceURL:
                    _headText.Text = "リファレンス";
                    break;

                case PanelHeader.Specialization:
                    _headText.Text = "特殊化";
                    break;

                case PanelHeader.StaticMethod:
                    _headText.Text = "static メンバー関数";
                    break;

                case PanelHeader.Struct:
                    _headText.Text = "構造体";
                    break;

                case PanelHeader.Typedefs:
                    _headText.Text = "型名";
                    break;
            }

        }
    }
}
