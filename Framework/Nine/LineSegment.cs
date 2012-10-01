namespace Nine
{
    using System;
    using Microsoft.Xna.Framework;


    /// <summary>
    /// Defines a line segment in 2D space.
    /// </summary>
#if WINDOWS
    [Serializable()]
#endif
    public struct LineSegment : IEquatable<LineSegment>
    {
        /// <summary>
        /// Gets or sets the start point of this <see cref="LineSegment"/>.
        /// </summary>
        public Vector2 Start;

        /// <summary>
        /// Gets or sets the end point of this <see cref="LineSegment"/>.
        /// </summary>
        public Vector2 End;

        /// <summary>
        /// Gets the normal of this <see cref="LineSegment"/>.
        /// </summary>
        public Vector2 Normal
        {
            get
            {
                var result = new Vector2();
                result.X = Start.Y - End.Y;
                result.Y = End.X - Start.X;
                result.Normalize();
                return result;
            }
        }
        
        /// <summary>
        /// Gets the center of this <see cref="LineSegment"/>.
        /// </summary>
        public Vector2 Center
        {
            get
            {
                var result = new Vector2();
                result.X = 0.5f * (Start.X + End.X);
                result.Y = 0.5f * (Start.Y + End.Y);
                return result;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> struct.
        /// </summary>
        public LineSegment(Vector2 start, Vector2 end)
        {
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Gets the length of this <see cref="LineSegment"/>.
        /// </summary>
        public float Length()
        {
            var xx = Start.X - End.X;
            var yy = Start.Y - End.Y;
            return (float)Math.Sqrt(xx * xx + yy * yy);
        }

        /// <summary>
        /// Gets the squared length of this <see cref="LineSegment"/>.
        /// </summary>
        public float LengthSquared()
        {
            var xx = Start.X - End.X;
            var yy = Start.Y - End.Y;
            return xx * xx + yy * yy;
        }

        /// <summary>
        /// Moves this <see cref="LineSegment"/> along its normal for the specified length.
        /// </summary>
        public void Offset(float length)
        {
            var normal = Normal;

            Start += normal * length;
            End += normal * length;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        public bool Equals(LineSegment other)
        {
            return Start == other.Start && End == other.End;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is LineSegment)
                return Equals((LineSegment)obj);

            return false;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        public static bool operator ==(LineSegment value1, LineSegment value2)
        {
            return ((value1.Start == value2.Start) && (value1.End == value2.End));
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        public static bool operator !=(LineSegment value1, LineSegment value2)
        {
            return !(value1.Start == value2.Start && value1.End == value2.End);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return Start.GetHashCode() ^ End.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Start.ToString() + " - " + End.ToString();
        }
    }
}