#region Copyright 2009 - 2011 (c) Engine Nine
//=============================================================================
//
//  Copyright 2009 - 2011 (c) Engine Nine. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Directives
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Markup;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace Nine
{
    #region IComponent
    /// <summary>
    /// Defines the protocal for game objects to interact with each other.
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Gets or sets the parent of this game object. 
        /// </summary>
        /// <remarks>
        /// After this game object is added to the parent container object, 
        /// the parent object is responsable for setting the Parent property
        /// of the child game object. 
        /// This property should never be modified elsewhere.
        /// You can always trust a valid parent is set when implementing a 
        /// game object.
        /// </remarks>
        WorldObject Parent { get; set; }
    }
    #endregion

    #region Component
    /// <summary>
    /// Defines a basic game component that can be added to a parent game object.
    /// </summary>
    [RuntimeNameProperty("Name")]
    [DictionaryKeyProperty("Name")]
    public class Component : IComponent
    {
        /// <summary>
        /// Gets or sets the name of this game object.
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Gets the parent of this game object.
        /// </summary>
        [ContentSerializerIgnore]
        public WorldObject Parent
        {
            get { return parent; }
        }

        #region Parent
        WorldObject IComponent.Parent
        {
            get { return parent; }
            set
            {
                if (parent != value)
                {
                    if (value != null)
                    {
                        if (parent != null)
                        {
                            throw new InvalidOperationException("This game object already belongs to a container");
                        }
                        parent = value;
                        OnAdded(parent);
                    }
                    else
                    {
                        if (parent == null)
                        {
                            throw new InvalidOperationException("This game object does not belongs to the specified container");
                        }
                        OnRemoved(parent);
                        parent = null;
                    }
                }
            }
        }
        WorldObject parent;
        #endregion

        /// <summary>
        /// Called when this game component is added to a parent game object.
        /// </summary>
        protected virtual void OnAdded(WorldObject parent) { }

        /// <summary>
        /// Called when this game component is removed from a parent game object.
        /// </summary>
        protected virtual void OnRemoved(WorldObject parent) { }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Name != null ? Name.ToString() : base.ToString();
        }
    }
    #endregion
}