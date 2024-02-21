using Music.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Music.Controls
{
    //public class VirtualizingWrapPanel : VirtualizingPanel, IScrollInfo
    //{

    //    public VirtualizingWrapPanel()
    //    {
    //        this.RenderTransform = _trans;
    //    }
    //    private TranslateTransform _trans = new TranslateTransform();

    //    public bool CanVerticallyScroll { get; set; }
    //    public bool CanHorizontallyScroll { get; set; }

    //    private Size _extent = new Size(0, 0);
    //    public double ExtentWidth => _extent.Width;

    //    public double ExtentHeight => _extent.Height;

    //    private Size _viewPort = new Size(0, 0);
    //    public double ViewportWidth => _viewPort.Width;

    //    public double ViewportHeight => _viewPort.Height;

    //    private Point _offset;

    //    public double HorizontalOffset => _offset.X;

    //    public double VerticalOffset => _offset.Y;

    //    public ScrollViewer ScrollOwner { get; set; }

    //    public void LineDown()
    //    {
    //        this.SetVerticalOffset(this.VerticalOffset + this.ScrollOffset);
    //    }

    //    public void LineLeft()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void LineRight()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void LineUp()
    //    {
    //        this.SetVerticalOffset(this.VerticalOffset - this.ScrollOffset);
    //    }

    //    public Rect MakeVisible(Visual visual, Rect rectangle)
    //    {
    //        return new Rect();
    //    }

    //    public void MouseWheelDown()
    //    {
    //        this.SetVerticalOffset(this.VerticalOffset + this.ScrollOffset);
    //    }

    //    public void MouseWheelLeft()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void MouseWheelRight()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void MouseWheelUp()
    //    {
    //        this.SetVerticalOffset(this.VerticalOffset - this.ScrollOffset);
    //    }

    //    public void PageDown()
    //    {
    //        this.SetVerticalOffset(this.VerticalOffset + this._viewPort.Height);
    //    }

    //    public void PageLeft()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void PageRight()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void PageUp()
    //    {
    //        this.SetVerticalOffset(this.VerticalOffset - this._viewPort.Height);
    //    }

    //    public void SetHorizontalOffset(double offset)
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public void SetVerticalOffset(double offset)
    //    {
    //        if (offset < 0 || this._viewPort.Height >= this._extent.Height)
    //            offset = 0;
    //        else
    //            if (offset + this._viewPort.Height >= this._extent.Height)
    //            offset = this._extent.Height - this._viewPort.Height;

    //        this._offset.Y = offset;
    //        this.ScrollOwner?.InvalidateScrollInfo();
    //        this._trans.Y = -offset;
    //        this.InvalidateMeasure();
    //        //接下来会触发MeasureOverride()
    //    }

    //    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    //    {
    //        base.OnRenderSizeChanged(sizeInfo);
    //        this.SetVerticalOffset(this.VerticalOffset);
    //    }

    //    protected override void OnClearChildren()
    //    {
    //        base.OnClearChildren();
    //        this.SetVerticalOffset(0);
    //    }

    //    protected override void BringIndexIntoView(int index)
    //    {
    //        if (index < 0 || index >= Children.Count)
    //            throw new ArgumentOutOfRangeException();
    //        var rows = _rowIndexRangeDic.Where(a => a.Value.Contains(index));
    //        if (rows.Count() == 1)
    //        {
    //            int row = rows.First().Key;
    //            if (_rowHeightDic.ContainsKey(row))
    //            {
    //                double vHeight = _rowHeightDic[row];
    //                SetVerticalOffset(vHeight);
    //            }
    //        }
    //    }

    //    private struct UVSize
    //    {
    //        internal UVSize(Orientation orientation, double width, double height)
    //        {
    //            U = V = 0d;
    //            _orientation = orientation;
    //            Width = width;
    //            Height = height;
    //        }

    //        internal UVSize(Orientation orientation)
    //        {
    //            U = V = 0d;
    //            _orientation = orientation;
    //        }

    //        internal double U;
    //        internal double V;
    //        private Orientation _orientation;

    //        internal double Width
    //        {
    //            get { return (_orientation == Orientation.Horizontal ? U : V); }
    //            set { if (_orientation == Orientation.Horizontal) U = value; else V = value; }
    //        }
    //        internal double Height
    //        {
    //            get { return (_orientation == Orientation.Horizontal ? V : U); }
    //            set { if (_orientation == Orientation.Horizontal) V = value; else U = value; }
    //        }
    //    }

    //    private Dictionary<int, List<int>> _rowIndexRangeDic;
    //    private Dictionary<int, double> _rowHeightDic;

    //    public static readonly DependencyProperty OrientationProperty =
    //           StackPanel.OrientationProperty.AddOwner(
    //                   typeof(VirtualizingWrapPanel),
    //                   new FrameworkPropertyMetadata(
    //                           Orientation.Horizontal,
    //                           FrameworkPropertyMetadataOptions.AffectsMeasure,
    //                           new PropertyChangedCallback(OnOrientationChanged)));
    //    /// <summary>
    //    /// Specifies dimension of children positioning in absence of wrapping.
    //    /// Wrapping occurs in orthogonal direction. For example, if Orientation is Horizontal, 
    //    /// the items try to form horizontal rows first and if needed are wrapped and form vertical stack of rows.
    //    /// If Orientation is Vertical, items first positioned in a vertical column, and if there is
    //    /// not enough space - wrapping creates additional columns in horizontal dimension.
    //    /// </summary>
    //    public Orientation Orientation
    //    {
    //        get { return _orientation; }
    //        set { SetValue(OrientationProperty, value); }
    //    }

    //    /// <summary>
    //    /// <see cref="PropertyMetadata.PropertyChangedCallback"/>
    //    /// </summary>
    //    private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        VirtualizingWrapPanel p = (VirtualizingWrapPanel)d;
    //        p._orientation = (Orientation)e.NewValue;
    //    }

    //    private Orientation _orientation;

    //    //鼠标每一次滚动 UI上的偏移
    //    public static readonly DependencyProperty ScrollOffsetProperty = DependencyProperty.RegisterAttached("ScrollOffset", typeof(int), typeof(VirtualizingWrapPanel), new PropertyMetadata(10));

    //    public int ScrollOffset
    //    {
    //        get { return Convert.ToInt32(GetValue(ScrollOffsetProperty)); }
    //        set { SetValue(ScrollOffsetProperty, value); }
    //    }

    //    int GetItemCount(DependencyObject element)
    //    {
    //        var itemsControl = ItemsControl.GetItemsOwner(element);
    //        return itemsControl.HasItems ? itemsControl.Items.Count : 0;
    //    }

    //    /// <summary>
    //    /// 更新滚动条
    //    /// </summary>
    //    /// <param name="availableSize"></param>
    //    void UpdateScrollInfo(Size availableSize, Size needSize)
    //    {
    //        //var extent = CalculateExtent(availableSize, GetItemCount(this));//extent 自己实际需要
    //        if (needSize != this._extent)
    //        {
    //            this._extent = needSize;
    //            this.ScrollOwner.InvalidateScrollInfo();
    //        }
    //        if (availableSize != this._viewPort)
    //        {
    //            this._viewPort = availableSize;
    //            this.ScrollOwner.InvalidateScrollInfo();
    //        }
    //    }


    //    /// <summary>
    //    /// 获取所有item，在可视区域内第一个item和最后一个item的索引
    //    /// </summary>
    //    /// <param name="firstIndex"></param>
    //    /// <param name="lastIndex"></param>
    //    void GetVisiableRange(ref int firstIndex, ref int lastIndex)
    //    {
    //        if (_rowHeightDic.Count == 0)
    //        {
    //            firstIndex = -1;
    //            lastIndex = -1;
    //        }
    //        else
    //        {

    //            for (int i = 0; i < _rowHeightDic.Count; i++)
    //            {
    //                if (i == 0)
    //                {
    //                    if (0 <= _offset.Y && _rowHeightDic[i] > _offset.Y)
    //                    {
    //                        firstIndex = _rowIndexRangeDic[i].First();
    //                        break;
    //                    }

    //                }
    //                else
    //                {
    //                    if (_rowHeightDic[i - 1] <= _offset.Y && _rowHeightDic[i] > _offset.Y)
    //                    {
    //                        firstIndex = _rowIndexRangeDic[i].First();
    //                        break;
    //                    }

    //                }
    //            }

    //            for (int i = 0; i < _rowHeightDic.Count; i++)
    //            {
    //                if (i == 0)
    //                {
    //                    if (0 <= _offset.Y + this._viewPort.Height && _rowHeightDic[i] > _offset.Y)
    //                    {
    //                        lastIndex = _rowIndexRangeDic[i].Last();
    //                        break;
    //                    }

    //                }
    //                else
    //                {
    //                    if (_rowHeightDic[i - 1] <= _offset.Y + this._viewPort.Height && _rowHeightDic[i] > _offset.Y)
    //                    {
    //                        firstIndex = _rowIndexRangeDic[i].Last();
    //                        break;
    //                    }

    //                }
    //            }
    //        }
    //        int itemsCount = GetItemCount(this);
    //        if (lastIndex >= itemsCount)
    //            lastIndex = itemsCount - 1;

    //    }


    //    /// <summary>
    //    /// 将不在可视区域内的item 移除
    //    /// </summary>
    //    /// <param name="startIndex">可视区域开始索引</param>
    //    /// <param name="endIndex">可视区域结束索引</param>
    //    void CleanUpItems(int startIndex, int endIndex)
    //    {
    //        var children = this.InternalChildren;
    //        var generator = this.ItemContainerGenerator;
    //        for (int i = children.Count - 1; i >= 0; i--)
    //        {
    //            var childGeneratorPosi = new GeneratorPosition(i, 0);
    //            int itemIndex = generator.IndexFromGeneratorPosition(childGeneratorPosi);

    //            if (itemIndex < startIndex || itemIndex > endIndex)
    //            {

    //                generator.Remove(childGeneratorPosi, 1);
    //                RemoveInternalChildRange(i, 1);
    //            }
    //        }
    //    }

    //    Size _needSize;

    //    protected override Size MeasureOverride(Size availableSize)
    //    {
    //        _rowIndexRangeDic = new Dictionary<int, List<int>>();
    //        _rowHeightDic = new Dictionary<int, double>();

    //        if (availableSize.IsEmpty || double.IsInfinity(availableSize.Height) || double.IsInfinity(availableSize.Width))
    //        {
    //            throw new Exception(string.Format("VirtualizingWrapPanel不支持该尺寸下的布局, availableSize:{0}", availableSize));
    //        }
    //        UVSize curLineSize = new UVSize(Orientation);//每行的size
    //        UVSize panelSize = new UVSize(Orientation);//最终需要的size大小
    //        UVSize uvConstraint = new UVSize(Orientation, availableSize.Width, availableSize.Height);//可用的空间size大小

    //        Size childConstraint = new Size(
    //          (availableSize.Width),
    //          (availableSize.Height));

    //        UIElementCollection children = InternalChildren;
    //        if (children.Count == 0)
    //        {
    //            return new Size(double.IsInfinity(availableSize.Width) ? 0 : availableSize.Width, double.IsInfinity(availableSize.Height) ? 0 : availableSize.Height);
    //        }
    //        int row = 0;
    //        List<int> rowRange = new List<int>();
    //        for (int i = 0, count = children.Count; i < count; i++)
    //        {
    //            UIElement child = children[i] as UIElement;
    //            if (child == null) continue;

    //            child.Measure(childConstraint);

    //            UVSize sz = new UVSize(  //子控件的size
    //                Orientation,
    //                (child.DesiredSize.Width),
    //                (child.DesiredSize.Height));

    //            if (DoubleUtil.GreaterThan(curLineSize.U + sz.U, uvConstraint.U)) //need to switch to another line
    //            {


    //                panelSize.U = Math.Max(curLineSize.U, panelSize.U);
    //                panelSize.V += curLineSize.V;
    //                curLineSize = sz;

    //                if (DoubleUtil.GreaterThan(sz.U, uvConstraint.U)) //the element is wider then the constrint - give it a separate line                    
    //                {
    //                    panelSize.U = Math.Max(sz.U, panelSize.U);
    //                    panelSize.V += sz.V;
    //                    curLineSize = new UVSize(Orientation);
    //                }

    //                _rowIndexRangeDic.Add(row, rowRange);
    //                rowRange = new List<int>();
    //                rowRange.Add(i);

    //                if (_rowHeightDic.ContainsKey(row))
    //                {
    //                    _rowHeightDic[row] = panelSize.Height;
    //                }
    //                else
    //                {
    //                    _rowHeightDic.Add(row, panelSize.Height);
    //                }

    //                row++;
    //            }
    //            else //continue to accumulate a line
    //            {
    //                rowRange.Add(i);

    //                curLineSize.U += sz.U;
    //                curLineSize.V = Math.Max(sz.V, curLineSize.V);
    //            }

    //            if (!_rowIndexRangeDic.ContainsKey(row))//最后一行
    //            {
    //                _rowIndexRangeDic.Add(row, rowRange);
    //            }

    //            if (!_rowHeightDic.ContainsKey(row))//最后一行
    //            {
    //                _rowHeightDic.Add(row, panelSize.Height);
    //            }
    //        }
    //        _needSize = new Size(panelSize.Width, panelSize.Height);
    //        UpdateScrollInfo(availableSize, _needSize);

    //        int firstVisiableIndex = 0, lastVisiableIndex = 0;
    //        GetVisiableRange(ref firstVisiableIndex, ref lastVisiableIndex);

    //        IItemContainerGenerator generator = this.ItemContainerGenerator;
    //        //获得第一个可被显示的item的位置
    //        GeneratorPosition startPosi = generator.GeneratorPositionFromIndex(firstVisiableIndex);
    //        int childIndex = (startPosi.Offset == 0) ? startPosi.Index : startPosi.Index + 1;//startPosi在chilren中的索引
    //        using (generator.StartAt(startPosi, GeneratorDirection.Forward, true))
    //        {
    //            int itemIndex = firstVisiableIndex;
    //            while (itemIndex <= lastVisiableIndex)//生成lastVisiableIndex-firstVisiableIndex个item
    //            {
    //                bool newlyRealized = false;
    //                var child = generator.GenerateNext(out newlyRealized) as UIElement;
    //                if (newlyRealized)
    //                {
    //                    if (childIndex >= children.Count)
    //                        base.AddInternalChild(child);
    //                    else
    //                    {
    //                        base.InsertInternalChild(childIndex, child);
    //                    }
    //                    generator.PrepareItemContainer(child);
    //                }
    //                else
    //                {
    //                    //处理 正在显示的child被移除了这种情况
    //                    if (!child.Equals(children[childIndex]))
    //                    {
    //                        base.RemoveInternalChildRange(childIndex, 1);
    //                    }
    //                }
    //                //child.DesiredSize//child想要的size
    //                itemIndex++;
    //                childIndex++;
    //            }
    //        }
    //        CleanUpItems(firstVisiableIndex, lastVisiableIndex);

    //        return new Size(double.IsInfinity(availableSize.Width) ? 0 : availableSize.Width, double.IsInfinity(availableSize.Height) ? 0 : availableSize.Height);
    //    }

    //    protected override Size ArrangeOverride(Size finalSize)
    //    {
    //        var generator = this.ItemContainerGenerator;
    //        UpdateScrollInfo(finalSize, _needSize);

    //        int firstInLine = 0;
    //        double accumulatedV = 0;

    //        UVSize curLineSize = new UVSize(Orientation);
    //        UVSize uvFinalSize = new UVSize(Orientation, finalSize.Width, finalSize.Height);

    //        UIElementCollection children = InternalChildren;

    //        for (int i = 0, count = children.Count; i < count; i++)
    //        {
    //            UIElement child = children[i] as UIElement;
    //            if (child == null) continue;

    //            UVSize sz = new UVSize(
    //                Orientation,
    //                (child.DesiredSize.Width),
    //                (child.DesiredSize.Height));

    //            if (DoubleUtil.GreaterThan(curLineSize.U + sz.U, uvFinalSize.U)) //need to switch to another line
    //            {
    //                arrangeLine(accumulatedV, curLineSize.V, firstInLine, i);

    //                accumulatedV += curLineSize.V;
    //                curLineSize = sz;

    //                if (DoubleUtil.GreaterThan(sz.U, uvFinalSize.U)) //the element is wider then the constraint - give it a separate line                    
    //                {
    //                    //switch to next line which only contain one element
    //                    arrangeLine(accumulatedV, sz.V, i, ++i);

    //                    accumulatedV += sz.V;
    //                    curLineSize = new UVSize(Orientation);
    //                }
    //                firstInLine = i;
    //            }
    //            else //continue to accumulate a line
    //            {
    //                curLineSize.U += sz.U;
    //                curLineSize.V = Math.Max(sz.V, curLineSize.V);
    //            }
    //        }

    //        //arrange the last line, if any
    //        if (firstInLine < children.Count)
    //        {
    //            arrangeLine(accumulatedV, curLineSize.V, firstInLine, children.Count);
    //        }

    //        return finalSize;
    //    }

    //    private void arrangeLine(double v, double lineV, int start, int end)
    //    {
    //        double u = 0;
    //        bool isHorizontal = (Orientation == Orientation.Horizontal);

    //        UIElementCollection children = InternalChildren;
    //        for (int i = start; i < end; i++)
    //        {
    //            UIElement child = children[i] as UIElement;
    //            if (child != null)
    //            {
    //                UVSize childSize = new UVSize(Orientation, child.DesiredSize.Width, child.DesiredSize.Height);
    //                double layoutSlotU = childSize.U;
    //                child.Arrange(new Rect(
    //                    (isHorizontal ? u : v),
    //                    (isHorizontal ? v : u),
    //                    (isHorizontal ? layoutSlotU : lineV),
    //                    (isHorizontal ? lineV : layoutSlotU)));
    //                u += layoutSlotU;
    //            }
    //        }
    //    }
    //}

    public class VirtualizingWrapPanel : VirtualizingPanel, IScrollInfo
    {
        private TranslateTransform trans = new TranslateTransform();


     
        // Using a DependencyProperty as the backing store for SelectedChangedCommand.  This enables animation, styling, binding, etc...
       
        public VirtualizingWrapPanel()
        {
            this.RenderTransform = trans;
        }

        #region DependencyProperties
        public static readonly DependencyProperty ChildWidthProperty = DependencyProperty.RegisterAttached("ChildWidth", typeof(double), typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty ChildHeightProperty = DependencyProperty.RegisterAttached("ChildHeight", typeof(double), typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        //鼠标每一次滚动 UI上的偏移
        public static readonly DependencyProperty ScrollOffsetProperty = DependencyProperty.RegisterAttached("ScrollOffset", typeof(int), typeof(VirtualizingWrapPanel), new PropertyMetadata(10));
       
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsScrollEndProperty =  DependencyProperty.Register("IsScrollEnd", typeof(bool), typeof(VirtualizingWrapPanel), new PropertyMetadata(false));

        public static readonly DependencyProperty ScrollEndCommandProperty = DependencyProperty.Register("ScrollEndCommand", typeof(ICommand), typeof(VirtualizingWrapPanel), new PropertyMetadata(null, new PropertyChangedCallback(PropertyChangedCallback)));

        static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        public static ICommand GetScrollEndCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ScrollEndCommandProperty);
        }

        public static void SetScrollEndCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ScrollEndCommandProperty, value);
        }



        public bool IsScrollEnd
        {
            get { return (bool)GetValue(IsScrollEndProperty); }
            set { SetValue(IsScrollEndProperty, value); }
        }
        public int ScrollOffset
        {
            get { return Convert.ToInt32(GetValue(ScrollOffsetProperty)); }
            set { SetValue(ScrollOffsetProperty, value); }
        }
        public double ChildWidth
        {
            get => Convert.ToDouble(GetValue(ChildWidthProperty));
            set => SetValue(ChildWidthProperty, value);
        }
        public double ChildHeight
        {
            get => Convert.ToDouble(GetValue(ChildHeightProperty));
            set => SetValue(ChildHeightProperty, value);
        }
        #endregion

        int GetItemCount(DependencyObject element)
        {
            var itemsControl = ItemsControl.GetItemsOwner(element);
            return itemsControl.HasItems ? itemsControl.Items.Count : 0;
        }
        int CalculateChildrenPerRow(Size availableSize)
        {
            int childPerRow = 0;
            if (availableSize.Width == double.PositiveInfinity)
                childPerRow = this.Children.Count;
            else
                childPerRow = Math.Max(1, Convert.ToInt32(Math.Floor(availableSize.Width / this.ChildWidth)));
            return childPerRow;
        }
        /// <summary>
        /// width不超过availableSize的情况下，自身实际需要的Size(高度可能会超出availableSize)
        /// </summary>
        /// <param name="availableSize"></param>
        /// <param name="itemsCount"></param>
        /// <returns></returns>
        Size CalculateExtent(Size availableSize, int itemsCount)
        {
            int childPerRow = CalculateChildrenPerRow(availableSize);//现有宽度下 一行可以最多容纳多少个
            return new Size(childPerRow * this.ChildWidth, this.ChildHeight * Math.Ceiling(Convert.ToDouble(itemsCount) / childPerRow));
        }
        /// <summary>
        /// 更新滚动条
        /// </summary>
        /// <param name="availableSize"></param>
        void UpdateScrollInfo(Size availableSize)
        {
            var extent = CalculateExtent(availableSize, GetItemCount(this));//extent 自己实际需要
            if (extent != this.extent)
            {
                this.extent = extent;
                this.ScrollOwner.InvalidateScrollInfo();
            }
            if (availableSize != this.viewPort)
            {
                this.viewPort = availableSize;
                this.ScrollOwner.InvalidateScrollInfo();
            }
        }
        /// <summary>
        /// 获取所有item，在可视区域内第一个item和最后一个item的索引
        /// </summary>
        /// <param name="firstIndex"></param>
        /// <param name="lastIndex"></param>
        void GetVisiableRange(ref int firstIndex, ref int lastIndex)
        {
            int childPerRow = CalculateChildrenPerRow(this.extent);
            firstIndex = Convert.ToInt32(Math.Floor(this.offset.Y / this.ChildHeight)) * childPerRow;
            lastIndex = Convert.ToInt32(Math.Ceiling((this.offset.Y + this.viewPort.Height) / this.ChildHeight)) * childPerRow - 1;
            int itemsCount = GetItemCount(this);
            if (lastIndex >= itemsCount)
                lastIndex = itemsCount - 1;

        }
        /// <summary>
        /// 将不在可视区域内的item 移除
        /// </summary>
        /// <param name="startIndex">可视区域开始索引</param>
        /// <param name="endIndex">可视区域结束索引</param>
        void CleanUpItems(int startIndex, int endIndex)
        {
            var children = this.InternalChildren;
            var generator = this.ItemContainerGenerator;
            for (int i = children.Count - 1; i >= 0; i--)
            {
                var childGeneratorPosi = new GeneratorPosition(i, 0);
                int itemIndex = generator.IndexFromGeneratorPosition(childGeneratorPosi);

                if (itemIndex < startIndex || itemIndex > endIndex)
                {

                    generator.Remove(childGeneratorPosi, 1);
                    RemoveInternalChildRange(i, 1);
                }
            }
        }
        /// <summary>
        /// scroll/availableSize/添加删除元素 改变都会触发  edit元素不会改变
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (availableSize.IsEmpty || double.IsInfinity(availableSize.Height) || double.IsInfinity(availableSize.Width))
            {
                //throw new Exception(string.Format("VirtualizingWrapPanel不支持该尺寸下的布局, availableSize:{0}", availableSize));
                return new Size(0,0);
            }
            this.UpdateScrollInfo(availableSize);//availableSize更新后，更新滚动条
            int firstVisiableIndex = 0, lastVisiableIndex = 0;
            GetVisiableRange(ref firstVisiableIndex, ref lastVisiableIndex);//availableSize更新后，获取当前viewport内可放置的item的开始和结束索引  firstIdnex-lastIndex之间的item可能部分在viewport中也可能都不在viewport中。

            UIElementCollection children = this.InternalChildren;//因为配置了虚拟化，所以children的个数一直是viewport区域内的个数,如果没有虚拟化则是ItemSource的整个的个数
            IItemContainerGenerator generator = this.ItemContainerGenerator;
            //获得第一个可被显示的item的位置
            GeneratorPosition startPosi = generator.GeneratorPositionFromIndex(firstVisiableIndex);
            int childIndex = (startPosi.Offset == 0) ? startPosi.Index : startPosi.Index + 1;//startPosi在chilren中的索引
            using (generator.StartAt(startPosi, GeneratorDirection.Forward, true))
            {
                int itemIndex = firstVisiableIndex;
                while (itemIndex <= lastVisiableIndex)//生成lastVisiableIndex-firstVisiableIndex个item
                {
                    bool newlyRealized = false;
                    var child = generator.GenerateNext(out newlyRealized) as UIElement;
                    if (newlyRealized)
                    {
                        if (childIndex >= children.Count)
                            base.AddInternalChild(child);
                        else
                        {
                            base.InsertInternalChild(childIndex, child);
                        }
                        generator.PrepareItemContainer(child);
                    }
                    else
                    {
                        //处理 正在显示的child被移除了这种情况
                        if (!child.Equals(children[childIndex]))
                        {
                            base.RemoveInternalChildRange(childIndex, 1);
                        }
                    }
                    child.Measure(new Size(this.ChildWidth, this.ChildHeight));
                    //child.DesiredSize//child想要的size
                    itemIndex++;
                    childIndex++;
                }
            }
            CleanUpItems(firstVisiableIndex, lastVisiableIndex);
            return new Size(double.IsInfinity(availableSize.Width) ? 0 : availableSize.Width, double.IsInfinity(availableSize.Height) ? 0 : availableSize.Height);//自身想要的size
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            //Debug.WriteLine("----ArrangeOverride");
            var generator = this.ItemContainerGenerator;
            UpdateScrollInfo(finalSize);
            int childPerRow = CalculateChildrenPerRow(finalSize);
            double availableItemWidth = finalSize.Width / childPerRow;
            for (int i = 0; i <= this.Children.Count - 1; i++)
            {
                var child = this.Children[i];
                int itemIndex = generator.IndexFromGeneratorPosition(new GeneratorPosition(i, 0));
                int row = itemIndex / childPerRow;//current row
                int column = itemIndex % childPerRow;
                double xCorrdForItem = 0;

                xCorrdForItem = column * availableItemWidth + (availableItemWidth - this.ChildWidth) / 2;

                Rect rec = new Rect(xCorrdForItem, row * this.ChildHeight, this.ChildWidth, this.ChildHeight);
                child.Arrange(rec);
            }
            return finalSize;
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.SetVerticalOffset(this.VerticalOffset);
        }
        protected override void OnClearChildren()
        {
            base.OnClearChildren();
            this.SetVerticalOffset(0);
        }
        protected override void BringIndexIntoView(int index)
        {
            if (index < 0 || index >= Children.Count)
                throw new ArgumentOutOfRangeException();
            int row = index / CalculateChildrenPerRow(RenderSize);
            SetVerticalOffset(row * this.ChildHeight);
        }
        #region IScrollInfo Interface
        public bool CanVerticallyScroll { get; set; }
        public bool CanHorizontallyScroll { get; set; }

        private Size extent = new Size(0, 0);
        public double ExtentWidth => this.extent.Width;

        public double ExtentHeight => this.extent.Height;

        private Size viewPort = new Size(0, 0);
        public double ViewportWidth => this.viewPort.Width;

        public double ViewportHeight => this.viewPort.Height;

        private Point offset;
        public double HorizontalOffset => this.offset.X;

        public double VerticalOffset => this.offset.Y;

        public ScrollViewer ScrollOwner { get; set; }

        public void LineDown()
        {
            if (this.VerticalOffset == extent.Height - viewPort.Height)
            {
                IsScrollEnd = true;
                GetScrollEndCommand(this)?.Execute(null);
            }
            else
            {
                IsScrollEnd = false;
            }
            this.SetVerticalOffset(this.VerticalOffset + this.ScrollOffset);
        }

        public void LineLeft()
        {
            throw new NotImplementedException();
        }

        public void LineRight()
        {
            throw new NotImplementedException();
        }

        public void LineUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.ScrollOffset);
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            return new Rect();
        }

        public void MouseWheelDown()
        {
            if (this.VerticalOffset == extent.Height - viewPort.Height)
            {
                IsScrollEnd = true;
                GetScrollEndCommand(this)?.Execute(null);
            }
            else
            {
                IsScrollEnd = false;
            }
            this.SetVerticalOffset(this.VerticalOffset + this.ScrollOffset);
        }

        public void MouseWheelLeft()
        {
            throw new NotImplementedException();
        }

        public void MouseWheelRight()
        {
            throw new NotImplementedException();
        }

        public void MouseWheelUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.ScrollOffset);
        }

        public void PageDown()
        {
            if (this.VerticalOffset == extent.Height - viewPort.Height)
            {
                IsScrollEnd = true;
                GetScrollEndCommand(this)?.Execute(null);
            }
            else
            {
                IsScrollEnd = false;
            }
            this.SetVerticalOffset(this.VerticalOffset + this.viewPort.Height);
        }

        public void PageLeft()
        {
            throw new NotImplementedException();
        }

        public void PageRight()
        {
            throw new NotImplementedException();
        }

        public void PageUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.viewPort.Height);
        }

        public void SetHorizontalOffset(double offset)
        {
            throw new NotImplementedException();
        }

        public void SetVerticalOffset(double offset)
        {
            if (offset < 0 || this.viewPort.Height >= this.extent.Height)
                offset = 0;
            else
                if (offset + this.viewPort.Height >= this.extent.Height)
                offset = this.extent.Height - this.viewPort.Height;

            this.offset.Y = offset;
            this.ScrollOwner?.InvalidateScrollInfo();
            this.trans.Y = -offset;
            this.InvalidateMeasure();
            //接下来会触发MeasureOverride()
        }
        #endregion
    }
}
