﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace Team3DesktopApp.View
{

    public class NavButton : Button
    {
        static NavButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton), new FrameworkPropertyMetadata(typeof(NavButton)));
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(NavButton), new PropertyMetadata(null));
        public static readonly DependencyProperty NavUriProperty = DependencyProperty.Register("NavUri", typeof(string), typeof(NavButton), new PropertyMetadata(null));


        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string NavUri
        {
            get => (string)GetValue(NavUriProperty);
            set => SetValue(NavUriProperty, value);
        }



    }
}