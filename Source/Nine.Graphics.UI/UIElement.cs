#region License
/* The MIT License
 *
 * Copyright (c) 2011 Red Badger Consulting
 * Copyright (c) 2012 Yufei Huang
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
    using Microsoft.Xna.Framework;
    using Nine.Graphics.UI.Input;
    using Nine.Graphics.UI.Internal;
    using Microsoft.Xna.Framework.Graphics;
    using Nine.Graphics.UI.Media;

    using Nine.Graphics.Primitives;

    public abstract class UIElement : IContainer, IComponent
    {
        #region Properties

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        private bool visible = true;

        public BoundingRectangle? Clip
        {
            get { return this.clip; }
        }
        private BoundingRectangle? clip = null;

        public BoundingRectangle AbsoluteRenderTransform
        {
            get 
            {
                return new BoundingRectangle(AbsoluteVisualOffset.X, AbsoluteVisualOffset.Y, ActualWidth, ActualHeight);
            }
        }

        public BoundingRectangle RenderTransform
        {
            get { return new BoundingRectangle(VisualOffset.X, VisualOffset.Y, ActualWidth, ActualHeight); }
        }

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

        public Vector2 VisualOffset
        {
            get { return this.visualOffset; }
        }

        IContainer IComponent.Parent
        {
            get { return Parent; }
            set { Parent = value as UIElement; }
        }

        System.Collections.IList IContainer.Children
        {
            get { return GetChildren() as System.Collections.IList; }
        }

        #endregion 

        #region Fields

        internal Dictionary<string, object> ExternalProperties = new Dictionary<string, object>();

        internal bool isArrangeValid;
        internal bool isMeasureValid;

        public Brush Background = null;

        public float Width = float.NaN;
        public float Height = float.NaN;

        public float MaxHeight = float.PositiveInfinity;
        public float MaxWidth = float.PositiveInfinity;

        public float MinHeight { get; set; }
        public float MinWidth { get; set; }

        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }

        public Vector2 RenderSize { get; private set; }
        public Vector2 DesiredSize { get; private set; }

        public float ActualWidth { get { return RenderSize.X; } }
        public float ActualHeight { get { return RenderSize.Y; } }

        public object DataContext { get; set; }
        public bool IsMouseCaptured { get; set; }
        public Thickness Margin { get; set; }

        private Vector2 previousAvailableSize;
        private BoundingRectangle previousFinalRect;

        private bool isClippingRequired;
        private Vector2 unclippedSize;
        private Vector2 visualOffset;

        public UIElement Parent { get; internal set; }

        #endregion

        #region Methods

        internal void NotifyGesture(Gesture gesture)
        {
            OnNextGesture(gesture);
        }

        public bool CaptureMouse()
        {
            Window rootElement;
            if (!this.IsMouseCaptured && this.TryGetRootElement(out rootElement))
            {
                this.IsMouseCaptured = rootElement.CaptureMouse(this);
            }
            return this.IsMouseCaptured;
        }

        public void ReleaseMouseCapture()
        {
            Window rootElement;
            if (this.IsMouseCaptured && this.TryGetRootElement(out rootElement))
            {
                rootElement.ReleaseMouseCapture(this);
                this.IsMouseCaptured = false;
            }
        }

        public virtual void OnApplyTemplate() { }

        public virtual IList<UIElement> GetChildren() { return null; }

        public bool HitTest(Vector2 point)
        {
            Vector2 absoluteOffset = Vector2.Zero;
            UIElement currentElement = this;

            while (currentElement != null)
            {
                absoluteOffset += currentElement.VisualOffset;
                currentElement = currentElement.Parent;
            }

            var hitTestRect = new BoundingRectangle(absoluteOffset.X, absoluteOffset.Y, this.ActualWidth, this.ActualHeight);            
            return hitTestRect.Contains(point.X, point.Y) == ContainmentType.Contains;
        }

        protected internal virtual void OnRender(SpriteBatch spriteBatch) 
        {
            if (spriteBatch.GraphicsDevice.RasterizerState.ScissorTestEnable != isClippingRequired)
            {
                spriteBatch.GraphicsDevice.RasterizerState = isClippingRequired ? Window.WithClipping : Window.WithoutClipping;
            }
            if (isClippingRequired)
            {
                var ClippingRect = GetClippingRect(RenderSize);
                if (ClippingRect.HasValue)
                    spriteBatch.GraphicsDevice.ScissorRectangle = (BoundingRectangle)ClippingRect;
            }

            if (Background != null)
            {
                spriteBatch.Draw(AbsoluteRenderTransform, Background);
            }
        }
        protected internal virtual void OnDebugRender(DynamicPrimitive primitive)
        {
            primitive.AddRectangle(
                new Vector2(AbsoluteRenderTransform.X, AbsoluteRenderTransform.Y),
                new Vector2(AbsoluteRenderTransform.X + AbsoluteRenderTransform.Width,
                    AbsoluteRenderTransform.Y + AbsoluteRenderTransform.Height),
                null, Color.LightBlue, 2);

            var Children = GetChildren();
            if (Children != null)
                foreach (var Child in Children)
                    Child.OnDebugRender(primitive);
        }

        public bool TryGetRootElement(out Window rootElement)
        {
            if (Parent != null)
            {
                if (Parent is Window)
                {
                    rootElement = Parent as Window;
                    return true;
                }
                else
                    return Parent.TryGetRootElement(out rootElement);
            }
            rootElement = null;
            return false;
        }

        protected virtual BoundingRectangle? GetClippingRect(Vector2 finalSize)
        {
            if (!this.isClippingRequired)
                return null;

            var max = new MinMax(this);
            Vector2 renderSize = this.RenderSize;

            float maxWidth = float.IsPositiveInfinity(max.MaxWidth) ? renderSize.X : max.MaxWidth;
            float maxHeight = float.IsPositiveInfinity(max.MaxHeight) ? renderSize.Y : max.MaxHeight;

            bool isClippingRequiredDueToMaxSize = maxWidth.IsLessThan(renderSize.X) ||
                                                  maxHeight.IsLessThan(renderSize.Y);

            renderSize.X = Math.Min(renderSize.X, max.MaxWidth);
            renderSize.Y = Math.Min(renderSize.Y, max.MaxHeight);

            Thickness margin = this.Margin;
            float horizontalMargins = margin.Left + margin.Right;
            float verticalMargins = margin.Top + margin.Bottom;

            var clientSize = new Vector2(
                (finalSize.X - horizontalMargins).EnsurePositive(), 
                (finalSize.Y - verticalMargins).EnsurePositive());

            bool isClippingRequiredDueToClientSize = clientSize.X.IsLessThan(renderSize.X) ||
                                                     clientSize.Y.IsLessThan(renderSize.Y);

            if (isClippingRequiredDueToMaxSize && !isClippingRequiredDueToClientSize)
            {
                return new BoundingRectangle(0f, 0f, maxWidth, maxHeight);
            }

            if (!isClippingRequiredDueToClientSize)
            {
                // TODO: Clipping
                return BoundingRectangle.Empty;
            }

            Vector2 offset = this.ComputeAlignmentOffset(clientSize, renderSize);

            var clipRect = new BoundingRectangle(-offset.X, -offset.Y, clientSize.X, clientSize.Y);

            if (isClippingRequiredDueToMaxSize)
            {
                //clipRect.Contains(new BoundingRectangle(0f, 0f, maxWidth, maxHeight));
            }
            
            return clipRect;
        }

        protected virtual void OnNextGesture(Gesture gesture) { }

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
                    return vector;
                case VerticalAlignment.Bottom:
                    vector.Y = clientSize.Y - inkSize.Y;
                    return vector;
                case VerticalAlignment.Top:
                    vector.Y = 0;
                    break;
            }

            return vector;
        }

        private object GetNearestDataContext()
        {
            UIElement curentElement = this;
            object dataContext;

            do
            {
                dataContext = curentElement.DataContext;
                curentElement = curentElement.Parent;
            }
            while (dataContext == null && curentElement != null);

            return dataContext;
        }

        private void InvalidateMeasureOnDataContextInheritors()
        {
            IEnumerable<UIElement> children = this.GetChildren();
            if (children.Count() == 0)
            {
                this.InvalidateMeasure();
            }
            else
            {
                IEnumerable<UIElement> childrenInheritingDataContext =
                    children.OfType<UIElement>().Where(element => element.DataContext == null);

                foreach (UIElement element in childrenInheritingDataContext)
                {
                    element.InvalidateMeasureOnDataContextInheritors();
                }
            }
        }

        #region Measure and Arrange

        public void InvalidateArrange()
        {
            var visualParent = this;
            while (visualParent != null)
            {
                visualParent.isArrangeValid = false;
                visualParent = visualParent.Parent;
            }
        }

        public void InvalidateMeasure()
        {
            var visualParent = this;
            while (visualParent != null)
            {
                visualParent.isMeasureValid = false;
                visualParent.isArrangeValid = false;
                visualParent = visualParent.Parent;
            }
        }

        public void Measure(Vector2 availableSize)
        {
            if (float.IsNaN(availableSize.X) || float.IsNaN(availableSize.Y))
                throw new InvalidOperationException("AvailableSize X or Y cannot be NaN");

            if (!this.isMeasureValid || availableSize.IsDifferentFrom(this.previousAvailableSize))
            {
                Vector2 size = this.MeasureCore(availableSize);

                if (float.IsPositiveInfinity(size.X) || float.IsPositiveInfinity(size.Y))
                    throw new InvalidOperationException("The implementing element returned a PositiveInfinity");

                if (float.IsNaN(size.X) || float.IsNaN(size.Y))
                    throw new InvalidOperationException("The implementing element returned NaN");

                this.previousAvailableSize = availableSize;
                this.isMeasureValid = true;
                this.DesiredSize = size;
            }
        }

        public void Arrange(BoundingRectangle finalRect)
        {
            if (float.IsNaN(finalRect.Width) || float.IsNaN(finalRect.Height))
                throw new InvalidOperationException("X and Y must be numbers");

            if (float.IsPositiveInfinity(finalRect.Width) || float.IsPositiveInfinity(finalRect.Height))
                throw new InvalidOperationException("X and Y must be less than infinity");

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
            this.OnApplyTemplate();

            Thickness margin = this.Margin;
            Vector2 availableSizeWithoutMargins = availableSize.Deflate(margin);

            var minMax = new MinMax(this);

            availableSizeWithoutMargins.X = MathHelper.Clamp(availableSizeWithoutMargins.X, minMax.MinWidth, minMax.MaxWidth);
            availableSizeWithoutMargins.Y = MathHelper.Clamp(availableSizeWithoutMargins.Y, minMax.MinHeight, minMax.MaxHeight);

            Vector2 size = this.MeasureOverride(availableSizeWithoutMargins);
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

        #endregion
    }
}