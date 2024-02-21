using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

namespace Music.Controls
{
    public class NotifyIcon : FrameworkElement, IDisposable
    {
        /// <summary>
        /// 内存释放标志
        /// </summary>
        private bool _isDisposed;
        /// <summary>
        /// 是否鼠标悬浮
        /// </summary>
        private bool _isMouseOver;
        
        private bool _added;

        /// <summary>
        /// 内容模式 容器 pop
        /// </summary>
        private Popup _contextContent;

        private readonly object _syncObj = new object();
        /// <summary>
        /// 图标闪烁定时器
        /// </summary>
        private DispatcherTimer _dispatcherTimerBlink;
        /// <summary>
        /// 鼠标位置定时器
        /// </summary>
        private DispatcherTimer _dispatcherTimerPos;

        private readonly int _id;

        private static int NextId;
        static NotifyIcon()
        {
            VisibilityProperty.OverrideMetadata(typeof(NotifyIcon), new PropertyMetadata(Visibility.Visible, OnVisibilityChanged));
            DataContextProperty.OverrideMetadata(typeof(NotifyIcon), new FrameworkPropertyMetadata(DataContextPropertyChanged));
            ContextMenuProperty.OverrideMetadata(typeof(NotifyIcon), new FrameworkPropertyMetadata(ContextMenuPropertyChanged));
        }
        /// <summary>
        /// VisibilityProperty 依赖属性值改变触发的回调
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //TODO
            //var ctl = (NotifyIcon)d;
            //var v = (Visibility)e.NewValue;

            //if (v == Visibility.Visible)
            //{
            //    if (ctl._iconCurrentHandle == IntPtr.Zero)
            //    {
            //        ctl.OnIconChanged();
            //    }
            //    ctl.UpdateIcon(true);
            //}
            //else if (ctl._iconCurrentHandle != IntPtr.Zero)
            //{
            //    ctl.UpdateIcon(false);
            //}
        }
        /// <summary>
        /// DataContextProperty依赖属性值改变触发的回调
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void DataContextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NotifyIcon)d).OnDataContextPropertyChanged(e);
        }

        private void OnDataContextPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateDataContext(_contextContent, e.OldValue, e.NewValue);//修改pop DataContext
            UpdateDataContext(ContextMenu, e.OldValue, e.NewValue);//修改ContextMenu DataContext
        }

        private void UpdateDataContext(FrameworkElement target, object oldValue, object newValue)
        {
            if (target == null || BindingOperations.GetBindingExpression(target, DataContextProperty) != null) return;
            if (ReferenceEquals(this, target.DataContext) || Equals(oldValue, target.DataContext))
            {
                target.DataContext = newValue ?? this;
            }
        }

        private static void ContextMenuPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (NotifyIcon)d;
            ctl.OnContextMenuPropertyChanged(e);
        }

        private void OnContextMenuPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateDataContext((ContextMenu)e.NewValue, null, DataContext);
        }

        public NotifyIcon()
        {
            //_id = ++NextId;
            //_callback = Callback;

            //Loaded += (s, e) => Init();

            if (Application.Current != null) Application.Current.Exit += (s, e) => Dispose();
        }

        /// <summary>
        /// 析构函数 释放托管资源
        /// </summary>
        ~NotifyIcon() => Dispose(false);
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing) //TODO
            {
                //if (_dispatcherTimerBlink != null && IsBlink)
                //{
                //    _dispatcherTimerBlink.Stop();
                //}
                //UpdateIcon(false);
            }

            _isDisposed = true;
        }
    }
}
