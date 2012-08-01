namespace Nine.Graphics.ObjectModel
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    /// <summary>
    /// Binds the transform from the source object to the target object. Once a transform binding
    /// is set, the source object will be transformed based on the target object.
    /// </summary>
    public class TransformBinding
    {
        /// <summary>
        /// Gets or sets the source object that is bound to the target object.
        /// </summary>
        public Transformable Source { get; internal set; }

        /// <summary>
        /// Gets or sets the target object to be bound.
        /// </summary>
        public Transformable Target { get; internal set; }

        /// <summary>
        /// Gets or sets the bias transformation matrix for the binding.
        /// </summary>
        public Matrix? Transform { get; set; }

        /// <summary>
        /// Gets or sets the bone name if the target is a model.
        /// </summary>
        public string TargetBone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether source object is scaled 
        /// according to the target bone.
        /// </summary>
        public bool UseBoneScale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the source model will 
        /// use the target skeleton.
        /// </summary>
        public bool ShareSkeleton { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformBinding"/> class.
        /// </summary>
        public TransformBinding(Transformable source, Transformable target)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == target)
                throw new InvalidOperationException("souce and target must be different");

            this.Source = source;
            this.Target = target;
        }

        /// <summary>
        /// For serialization
        /// </summary>
        internal TransformBinding() { }

        [ContentSerializer(SharedResource = true, ElementName="Source")]
        internal string SourceName;

        [ContentSerializer(SharedResource = true, ElementName = "Target")]
        internal string TargetName;

        internal int TargetBoneIndex = -1;
    }
}