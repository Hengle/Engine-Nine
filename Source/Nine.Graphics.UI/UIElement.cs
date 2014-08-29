#region License
/* The MIT License
 *
 * Copyright (c) 2013 Engine Nine
 * Copyright (c) 2011 Red Badger Consulting
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/
#endregion

namespace Nine.Graphics.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xaml;
    using Microsoft.Xna.Framework;
    using Nine.Graphics.UI.Internal;
    using Microsoft.Xna.Framework.Graphics;
    using Nine.Graphics.UI.Media;
    using Nine.Graphics.Primitives;
    using Nine.Graphics.UI.Renderer;

    /// <summary>
    /// UIElement is a base class for all the Controls.
    /// </summary>
    [Nine.Serialization.BinarySerializable]
    public abstract partial class UIElement : Nine.Object, IComponent
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public BoundingRectangle AbsoluteRenderTransform
        {
            get 
            {
                return new BoundingRectangle(AbsoluteVisualOffset.X, AbsoluteVisualOffset.Y, ActualWidth, ActualHeight);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BoundingRectangle RenderTransform
        {
            get { return new BoundingRectangle(VisualOffset.X, VisualOffset.Y, ActualWidth, ActualHeight); }
        }

        /// <summary>
        /// Absolute Position of the element.
        /// </summary>
        public Vector2 AbsoluteVisualOffset
        {
            get 
            {
                var Result = this.VisualOffset;
                if (Parent != null)
                {
                    Result += Parent.AbsoluteVisualOffset;
                }
                return Result; 
            }
        }

        /// <summary>
        /// Gets or sets the visibility of this element.
        /// </summary>
        public Visibility Visibility { get; set; }

        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        public Brush Background { get; set; }

        /// <summary>
        /// Local Position of the element.
        /// </summary>
        public Vector2 VisualOffset
        {
            get { return this.visualOffset; }
        }
        private Vector2 visualOffset;


        public float Width { get; set; }
        public float Height { get; set; }

        public float MaxWidth { get; set; }
        public float MaxHeight { get; set; }

        public float MinHeight { get; set; }
        public float MinWidth { get; set; }

        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }

        public Vector2 RenderSize { get; private set; }
        public Vector2 DesiredSize { get; private set; }

        public float ActualWidth { get { return RenderSize.X; } }
        public float ActualHeight { get { return RenderSize.Y; } }

        public Thickness Margin { get; set; }

        [Nine.Serialization.NotBinarySerializable]
        public UIElement Parent { get; internal set; }

        private bool needsClipping
        {
            get
            {
                if (Parent != null)
                {
                    if (Parent.clip.HasValue)
                        return true;
                }
                if (clip.HasValue)
                    return true;
                return false;
            }
        }
        private bool isClippingRequired;

        #endregion 

        #region Fields

        internal bool isArrangeValid;
        internal bool isMeasureValid;

        private Vector2 previousAvailableSize;
        private BoundingRectangle previousFinalRect;

        private Vector2 unclippedSize;

        internal BaseWindow Window;

        IContainer IComponent.Parent
        {
            get { return Parent as IContainer; }
            set { Parent = value as UIElement; }
        }

        internal BoundingRectangle? clip = null;

        #endregion

        #region Constructor

        protected UIElement()
        {
            this.Width = float.NaN;
            this.Height = float.NaN;
            this.MaxWidth = float.PositiveInfinity;
            this.MaxHeight = float.PositiveInfinity;

            this.Visibility = UI.Visibility.Visible;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootElement"></param>
        /// <returns></returns>
        public bool TryGetRootElement(out BaseWindow rootElement)
        {
            UIElement element = this;
            while ((rootElement = element.Window) == null)
            {
                if ((element = element.Parent as UIElement) == null)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Invalidates the size of the element.
        /// </summary>
        public void InvalidateMeasure()
        {
            var visualParent = this;
            while (visualParent != null)
            {
                visualParent.isMeasureValid = false;
                visualParent.isArrangeValid = false;
                visualParent = visualParent.Parent as UIElement;
            }
        }

        /// <summary>
        /// Invalidates the final rectangle of the element.
        /// </summary>
        public void InvalidateArrange()
        {
            var visualParent = this;
            while (visualParent != null)
            {
                visualParent.isArrangeValid = false;
                visualParent = visualParent.Parent as UIElement;
            }
        }

        private Vector2 ComputeAlignmentOffset(Vector2 clientSize, Vector2 inkSize)
        {
            var vector = new Vector2();
            HorizontalAlignment horizontalAlignment = this.HorizontalAlignment;
            VerticalAlignment verticalAlignment = this.VerticalAlignment;

            if (horizontalAlignment == HorizontalAlignment.Stretch && inkSize.X > clientSize.X)
            {
                horizontalAlignment = HorizontalAlignment.Left;
            }

            if (verticalAlignment == VerticalAlignment.Stretch && inkSize.Y > clientSize.Y)
            {
                verticalAlignment = VerticalAlignment.Top;
            }

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Center:
                case HorizontalAlignment.Stretch:
                    vector.X = (clientSize.X - inkSize.X) * 0.5f;
                    break;
                case HorizontalAlignment.Left:
                    vector.X = 0;
                    break;
                case HorizontalAlignment.Right:
                    vector.X = clientSize.X - inkSize.X;
                    break;
            }

            switch (verticalAlignment)
            {
                case VerticalAlignment.Center:
                case VerticalAlignment.Stretch:
                    vector.Y = (clientSize.Y - inkSize.Y) * 0.5f;
                    break;
                case VerticalAlignment.Bottom:
                    vector.Y = clientSize.Y - inkSize.Y;
                    break;
                case VerticalAlignment.Top:
                    vector.Y = 0;
                    break;
            }

            //vector.X += Margin.Left;
            //vector.Y += Margin.Top;

            return vector;
        }

        #endregion

        #region Measure and Arrange

        /// <summary>
        /// 
        /// </summary>
        /// <param name="availableSize"></param>
        public void Measure(Vector2 availableSize)
        {
            if (float.IsNaN(availableSize.X) || float.IsNaN(availableSize.Y))
                throw new InvalidOperationException("AvailableSize X or Y cannot be NaN.");

            if (!this.isMeasureValid || availableSize.IsDifferentFrom(this.previousAvailableSize))
            {
                Vector2 size = this.MeasureCore(availableSize);

                if (float.IsPositiveInfinity(size.X) || float.IsPositiveInfinity(size.Y))
                    throw new InvalidOperationException("The implementing element returned a PositiveInfinity.");

                if (float.IsNaN(size.X) || float.IsNaN(size.Y))
                    throw new InvalidOperationException("The implementing element returned NaN.");

                this.previousAvailableSize = availableSize;
                this.isMeasureValid = true;
                this.DesiredSize = size;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="finalRect"></param>
        public void Arrange(BoundingRectangle finalRect)
        {
            if (float.IsNaN(finalRect.Width) || float.IsNaN(finalRect.Height))
                throw new InvalidOperationException("FinalRect X and Y cannot be NaN.");

            if (float.IsPositiveInfinity(finalRect.Width) || float.IsPositiveInfinity(finalRect.Height))
                throw new InvalidOperationException("FinalRect X and Y cannot be Infinity.");

            if (!this.isArrangeValid || !finalRect.Equals(this.previousFinalRect))
            {
                this.ArrangeCore(finalRect);
                this.clip = this.GetClippingRect(finalRect.Size);

                this.previousFinalRect = finalRect;
                this.isArrangeValid = true;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, measures the size in layout required for child elements and determines a size for the UIElement-derived class.
        /// </summary>
        /// <param name = "availableSize">
        ///     The available size that this element can give to child elements.
        ///     Infinity can be specified as a value to indicate that the element will size to whatever content is available.
        /// </param>
        /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
        protected virtual Vector2 MeasureOverride(Vector2 availableSize)
        {
            return Vector2.Zero;
        }

        /// <summary>
        ///     When overridden in a derived class, positions child elements and determines a size for a UIElement derived class.
        /// </summary>
        /// <param name = "finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected virtual Vector2 ArrangeOverride(Vector2 finalSize)
        {
            return finalSize;
        }

        /// <summary>
        ///     Defines the template for core-level arrange layout definition.
        /// </summary>
        /// <remarks>
        ///     In WPF this method is defined on UIElement as protected virtual and has a base implementation.
        ///     FrameworkElement (which derrives from UIElement) creates a sealed implemention, similar to the below,
        ///     which discards UIElement's base implementation.
        /// </remarks>
        /// <param name = "finalRect">The final area within the parent that element should use to arrange itself and its child elements.</param>
        private void ArrangeCore(BoundingRectangle finalRect)
        {
            Thickness margin = this.Margin;

            Vector2 unclippedDesiredSize = isClippingRequired ? unclippedSize : DesiredSize.Deflate(margin);
            Vector2 finalSize = new Vector2(finalRect.Width, finalRect.Height);
            finalSize = finalSize.Deflate(margin);

            isClippingRequired = false;

            if (finalSize.X < unclippedDesiredSize.X)
            {
                isClippingRequired = true;
                finalSize.X = unclippedDesiredSize.X;
            }

            if (finalSize.Y < unclippedDesiredSize.Y)
            {
                isClippingRequired = true;
                finalSize.Y = unclippedDesiredSize.Y;
            }

            if (HorizontalAlignment != HorizontalAlignment.Stretch)
                finalSize.X = unclippedDesiredSize.X;

            if (VerticalAlignment != VerticalAlignment.Stretch)
                finalSize.Y = unclippedDesiredSize.Y;

            var minMax = new MinMax(this);

            float largestWidth = Math.Max(unclippedDesiredSize.X, minMax.MaxWidth);
            if (largestWidth < finalSize.X)
                finalSize.X = largestWidth;

            float largestHeight = Math.Max(unclippedDesiredSize.Y, minMax.MaxHeight);
            if (largestHeight < finalSize.Y)
                finalSize.Y = largestHeight;

            Vector2 renderSize = this.ArrangeOverride(finalSize);
            this.RenderSize = renderSize;

            Vector2 inkSize = new Vector2(
                Math.Min(renderSize.X, minMax.MaxWidth), Math.Min(renderSize.Y, minMax.MaxHeight));

            isClippingRequired |= inkSize.X < renderSize.X || inkSize.Y < renderSize.Y;

            Vector2 clientSize = finalRect.Size.Deflate(margin);

            isClippingRequired |= clientSize.X < inkSize.X || clientSize.Y < inkSize.Y;

            Vector2 offset = this.ComputeAlignmentOffset(clientSize, inkSize);
            offset.X += finalRect.X + margin.Left;
            offset.Y += finalRect.Y + margin.Top;

            this.visualOffset = offset;
        }

        /// <summary>
        ///     Implements basic measure-pass layout system behavior.
        /// </summary>
        /// <remarks>
        ///     In WPF this method is definded on UIElement as protected virtual and returns an empty Size.
        ///     FrameworkElement (which derrives from UIElement) then creates a sealed implementation similar to the below.
        ///     In XPF UIElement and FrameworkElement have been collapsed into a single class.
        /// </remarks>
        /// <param name = "availableSize">The available size that the parent element can give to the child elements.</param>
        /// <returns>The desired size of this element in layout.</returns>
        private Vector2 MeasureCore(Vector2 availableSize)
        {
            //this.ResolveDeferredBindings(this.GetNearestDataContext());
            //this.OnApplyTemplate();

            Thickness margin = this.Margin;
            Vector2 availableSizeWithoutMargins = availableSize.Deflate(margin);

            var minMax = new MinMax(this);

            availableSizeWithoutMargins.X = MathHelper.Clamp(availableSizeWithoutMargins.X, minMax.MinWidth, minMax.MaxWidth);
            availableSizeWithoutMargins.Y = MathHelper.Clamp(availableSizeWithoutMargins.Y, minMax.MinHeight, minMax.MaxHeight);

            Vector2 size = Visibility == Visibility.Collapsed ? 
                Vector2.Zero : this.MeasureOverride(availableSizeWithoutMargins);
            size.X = Math.Max(size.X, minMax.MinWidth);
            size.Y = Math.Max(size.Y, minMax.MinHeight);

            Vector2 unclippedSize = size;
            isClippingRequired = false;

            if (size.X > minMax.MaxWidth)
            {
                size.X = minMax.MaxWidth;
                isClippingRequired = true;
            }

            if (size.Y > minMax.MaxHeight)
            {
                size.Y = minMax.MaxHeight;
                isClippingRequired = true;
            }

            Vector2 desiredSizeWithMargins = size.Inflate(margin);

            if (desiredSizeWithMargins.X > availableSize.X)
            {
                desiredSizeWithMargins.X = availableSize.X;
                isClippingRequired = true;
            }

            if (desiredSizeWithMargins.Y > availableSize.Y)
            {
                desiredSizeWithMargins.Y = availableSize.Y;
                isClippingRequired = true;
            }

            this.unclippedSize = isClippingRequired ? unclippedSize : Vector2.Zero;
            return desiredSizeWithMargins;
        }

        #endregion

        // Move this
        internal UIElement ObjectToElement(object element)
        {
            if (element is UIElement)
                return element as UIElement;

            var isString = element as string;
            if (isString != null)
            {
                return new Nine.Graphics.UI.Controls.TextBlock
                {
                    Text = isString
                };
            }

            return null;
        }
    }
}
