using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace Music.Controls
{
    [TemplatePart(Name = PART_ButtonPartName, Type = typeof(DropDownControl))]
    [TemplatePart(Name = PART_PopupPartName, Type = typeof(DropDownControl))]
    public class DropDownControl : ContentControl
    {
        public const string PART_ButtonPartName = "PART_Button";
        public const string PART_PopupPartName = "PART_Popup";
        static DropDownControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownControl), new FrameworkPropertyMetadata(typeof(DropDownControl)));

        }

        ToggleButton button;
        Popup popup;

        public UIElement Child
        {
            get { return (UIElement)GetValue(ChildProperty); }
            set { SetValue(ChildProperty, value); }
        }

        public static readonly DependencyProperty ChildProperty =
            DependencyProperty.Register("Child", typeof(UIElement), typeof(DropDownControl));

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(DropDownControl), new PropertyMetadata(false, new PropertyChangedCallback(IsDropDownOpenPropertyChangedCallback)));

        static void IsDropDownOpenPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var DropDownControl = d as DropDownControl;
            if (DropDownControl == null) return;
            var btn = DropDownControl.button;
            if (btn == null) return;

            if (IsClickOpen)
            {
                IsClickOpen = false;
            }
            else
            {
                IsClickOpen = true;
            }
            var mouseP = Mouse.GetPosition(btn);
            if (!IsClickOpen && mouseP.X <= btn.ActualWidth && mouseP.X >= 0 && mouseP.Y <= btn.ActualHeight && mouseP.Y >= 0)
            {
                IsClickOpen = true;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            button = this.GetTemplateChild(PART_ButtonPartName) as ToggleButton;
            popup = this.GetTemplateChild(PART_PopupPartName) as Popup;

            button.Click -= ButtonClickHandler;
            button.Click += ButtonClickHandler;

        }
        static bool IsClickOpen = false;

        void ButtonClickHandler(object sender, RoutedEventArgs e)
        {
            if (popup == null) return;
            if (IsClickOpen) { IsClickOpen = false; return; }
            if (!IsClickOpen)
            {
                IsDropDownOpen = true;
            }
        }



        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(PlacementMode), typeof(DropDownControl), new PropertyMetadata(default(PlacementMode)));

        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(DropDownControl), new PropertyMetadata(0.0));

        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register("VerticalOffset", typeof(double), typeof(DropDownControl), new PropertyMetadata(0.0));


    }
}
