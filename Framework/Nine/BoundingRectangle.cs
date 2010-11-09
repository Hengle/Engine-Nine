#region Copyright 2009 - 2010 (c) Engine Nine
//=============================================================================
//
//  Copyright 2009 - 2010 (c) Engine Nine. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Directives
using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
#endregion

namespace Nine
{
    /// <summary>
    /// Defines an axis-aligned rectangle-shaped 2D volume.
    /// </summary>
#if WINDOWS
    [Serializable()]
#endif
    public struct BoundingRectangle : IEquatable<BoundingRectangle>
    {
        /// <summary>
        /// Gets or sets the min value.
        /// </summary>
        public Vector2 Min;

        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        public Vector2 Max;

        /// <summary>
        /// Create a new instance of BoundingRectangle object.
        /// </summary>
        public BoundingRectangle(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Create a new instance of BoundingRectangle object.
        /// </summary>
        public BoundingRectangle(Rectangle rectangle)
        {
            Min = new Vector2(rectangle.X, rectangle.Y);
            Max = new Vector2(rectangle.Right, rectangle.Bottom);
        }

        /// <summary>
        /// Tests whether the BoundingRectangle contains a point.
        /// </summary>
        public ContainmentType Contains(float x, float y)
        {
            return Math2D.PointInRectangle(new Vector2(x, y), Min, Max)
                ? ContainmentType.Contains : ContainmentType.Disjoint;
        }

        /// <summary>
        /// Tests whether the BoundingRectangle contains a point.
        /// </summary>
        public ContainmentType Contains(Vector2 point)
        {
            return Math2D.PointInRectangle(point, Min, Max)
                ? ContainmentType.Contains : ContainmentType.Disjoint;
        }

        /// <summary>
        /// Tests whether the BoundingRectangle contains another rectangle.
        /// </summary>
        public ContainmentType Contains(BoundingRectangle rectangle)
        {
            return Math2D.RectangleIntersects(
                Min, Max, Vector2.Zero, 0,
                rectangle.Min, rectangle.Max, Vector2.Zero, 0);
        }
        
        /// <summary>
        /// Determines if a Range<T> object equals to another Range<T> object
        /// </summary>
        public bool Equals(BoundingRectangle other)
        {
            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        public override string ToString()
        {
            return Min.ToString() + " ~ " + Max.ToString();
        }
    }
}