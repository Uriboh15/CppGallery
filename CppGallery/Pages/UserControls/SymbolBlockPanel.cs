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
        AliasTemplates,
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
        FunctionMacro,
        FunctionObject,
        GlobalstreamOjbect,
        Literal,
        Macro,
        Manipulator,
        Method,
        NonMemberFunction,
        Object,
        Operator,
        ReferenceURL,
        Specialization,
        StaticMethod,
        Struct,
        Typedefs,
    }

    public class SymbolBlockPanel : BasicBlockPanel
    {
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
           "Symbol", // Max という名前の……
           typeof(PanelHeader), // int 型の CLR プロパティを……
           typeof(SymbolBlockPanel), // クラスに登録するやで―
           new PropertyMetadata(PanelHeader.Function, new PropertyChangedCallback(OnSymbolChanged)));

        public static readonly DependencyProperty IsDeclarationOnlyProperty = DependencyProperty.Register(
           "IsDeclarationOnly", // Max という名前の……
           typeof(bool), // int 型の CLR プロパティを……
           typeof(SymbolBlockPanel), // クラスに登録するやで―
           new PropertyMetadata(false, new PropertyChangedCallback(OnIsDeclarationOnlyChanged)));

        public bool IsDeclarationOnly
        {
            get { return (bool)GetValue(IsDeclarationOnlyProperty); }
            set { SetValue(IsDeclarationOnlyProperty, value); }
        }

        public PanelHeader Symbol
        {
            get { return (PanelHeader)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        private const string DeclarationOnlyString = " (宣言のみ)";

        //https://qiita.com/tricogimmick/items/62cd9f5deca365a83858
        // 3. 依存プロパティが変更されたとき呼ばれるコールバック関数の定義
        private static void OnSymbolChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            // オブジェクトを取得して処理する
            SymbolBlockPanel ctrl = obj as SymbolBlockPanel;
            ctrl?.ApplyHead();
        }

        //https://qiita.com/tricogimmick/items/62cd9f5deca365a83858
        // 3. 依存プロパティが変更されたとき呼ばれるコールバック関数の定義
        private static void OnIsDeclarationOnlyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            // オブジェクトを取得して処理する
            SymbolBlockPanel ctrl = obj as SymbolBlockPanel;
            if (ctrl != null)
            {
                ctrl.ApplyHead();
                if (ctrl.IsDeclarationOnly)
                {
                    ctrl.HeadText += DeclarationOnlyString;
                }
            }
        }

        public SymbolBlockPanel()
        {
            
        }



        private void ApplyHead()
        {
            HeadText = this.Symbol switch
            {
                PanelHeader.AliasTemplates => "エイリアステンプレート",
                PanelHeader.BaseClass => "基底クラス",
                PanelHeader.Class => "クラス",
                PanelHeader.Concepts => "Concepts",
                PanelHeader.Constant => "定数値",
                PanelHeader.Constructor => "コンストラクター",
                PanelHeader.DerivedClass => "派生クラス",
                PanelHeader.Enum => "列挙型",
                PanelHeader.ErrorReporting => "エラー報告",
                PanelHeader.Exception => "例外クラス",
                PanelHeader.Function => "関数",
                PanelHeader.FunctionMacro => "関数マクロ",
                PanelHeader.FunctionAndMacro => "関数 ・ 関数マクロ",
                PanelHeader.FunctionObject => "関数オブジェクト",
                PanelHeader.GlobalstreamOjbect => "グローバルストリームオブジェクト",
                PanelHeader.Literal => "リテラル",
                PanelHeader.Macro => "マクロ",
                PanelHeader.Manipulator => "マニピュレーター",
                PanelHeader.Method => "メンバー関数",
                PanelHeader.NonMemberFunction => "非メンバー関数",
                PanelHeader.Object => "オブジェクト",
                PanelHeader.Operator => "演算子",
                PanelHeader.ReferenceURL => "リファレンス",
                PanelHeader.Specialization => "特殊化",
                PanelHeader.StaticMethod => "static メンバー関数",
                PanelHeader.Struct => "構造体",
                PanelHeader.Typedefs => "型名",
                _ => throw new NotImplementedException(),
            };

        }
    }
}
