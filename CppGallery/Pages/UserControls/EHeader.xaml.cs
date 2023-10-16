// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.UserControls
{
    public sealed partial class EHeader : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",�@// Max �Ƃ������O�́c�c
            typeof(string),�@// int �^�� CLR �v���p�e�B���c�c
            typeof(EHeader), // �N���X�ɓo�^�����Ł\
            new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnTitleChanged)));

        public static readonly DependencyProperty SentenceProperty = DependencyProperty.Register(
            "Sentence",�@// Max �Ƃ������O�́c�c
            typeof(string),�@// int �^�� CLR �v���p�e�B���c�c
            typeof(EHeader), // �N���X�ɓo�^�����Ł\
            new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnSentenceChanged)));

        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register(
           "Icon", // Max �Ƃ������O�́c�c
           typeof(string), // int �^�� CLR �v���p�e�B���c�c
           typeof(EHeader), // �N���X�ɓo�^�����Ł\
           new PropertyMetadata("\uF158"));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Sentence
        {
            get { return (string)GetValue(SentenceProperty); }
            set { SetValue(SentenceProperty, value); }
        }

        public string Icon
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        //https://qiita.com/tricogimmick/items/62cd9f5deca365a83858
        // 3. �ˑ��v���p�e�B���ύX���ꂽ�Ƃ��Ă΂��R�[���o�b�N�֐��̒�`
        private static void OnTitleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            // �I�u�W�F�N�g���擾���ď�������
            var ctrl = obj as EHeader;
            if (ctrl != null)
            {
                ctrl.TitleTextBlock.Text = ctrl.Title.Replace("::", " : : ").Replace("[]", "[ ]");
            }
        }

        //https://qiita.com/tricogimmick/items/62cd9f5deca365a83858
        // 3. �ˑ��v���p�e�B���ύX���ꂽ�Ƃ��Ă΂��R�[���o�b�N�֐��̒�`
        private static void OnSentenceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            // �I�u�W�F�N�g���擾���ď�������
            EHeader ctrl = obj as EHeader;
            if (ctrl != null)
            {
                ctrl.SentenceTextBlock.Text = ctrl.Sentence.Replace("::", " : : ");
            }
        }

        public EHeader()
        {
            this.InitializeComponent();
            FIcon.Margin = new Thickness((Data.GalleryControlHeight - 24.0) / 2.0 - 16.0, 0, (Data.GalleryControlHeight - 24.0) / 2.0, 0);
        }
    }
}
