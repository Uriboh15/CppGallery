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
        FunctionAndMacro,
        FunctionObject,
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
                this.HeadText += " (宣言のみ)";
            }
        }

        

        private void ApplyHead()
        {
            switch (this.Header)
            {
                case PanelHeader.BaseClass:
                    HeadText = "基底クラス";
                    break;

                case PanelHeader.Class:
                    HeadText = "クラス";
                    break;

                case PanelHeader.Concepts:
                    HeadText = "Concepts";
                    break;

                case PanelHeader.Constant:
                    HeadText = "定数値";
                    break;

                case PanelHeader.Constructor:
                    HeadText = "コンストラクター";
                    break;

                case PanelHeader.DerivedClass:
                    HeadText = "派生クラス";
                    break;

                case PanelHeader.Enum:
                    HeadText = "列挙型";
                    break;

                case PanelHeader.ErrorReporting:
                    HeadText = "エラー報告";
                    break;

                case PanelHeader.Exception:
                    HeadText = "例外クラス";
                    break;

                case PanelHeader.Function:
                    HeadText = "関数";
                    break;

                case PanelHeader.FunctionAndMacro:
                    HeadText = "関数 ・ 関数マクロ";
                    break;

                case PanelHeader.FunctionObject:
                    HeadText = "関数オブジェクト";
                    break;

                case PanelHeader.GlobalstreamOjbect:
                    HeadText = "グローバルストリームオブジェクト";
                    break;

                case PanelHeader.Included:
                    HeadText = "このヘッダーを include しているライブラリ";
                    break;

                case PanelHeader.Including:
                    HeadText = "このヘッダーに含まれるライブラリ";
                    break;

                case PanelHeader.Literal:
                    HeadText = "リテラル";
                    break;

                case PanelHeader.Macro:
                    HeadText = "マクロ";
                    break;

                case PanelHeader.Manipulator:
                    HeadText = "マニピュレーター";
                    break;

                case PanelHeader.Method:
                    HeadText = "メンバー関数";
                    break;

                case PanelHeader.Object:
                    HeadText = "オブジェクト";
                    break;

                case PanelHeader.Operator:
                    HeadText = "演算子";
                    break;

                case PanelHeader.ReferenceURL:
                    HeadText = "リファレンス";
                    break;

                case PanelHeader.Specialization:
                    HeadText = "特殊化";
                    break;

                case PanelHeader.StaticMethod:
                    HeadText = "static メンバー関数";
                    break;

                case PanelHeader.Struct:
                    HeadText = "構造体";
                    break;

                case PanelHeader.Typedefs:
                    HeadText = "型名";
                    break;
            }

        }
    }
}
