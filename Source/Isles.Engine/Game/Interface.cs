//-----------------------------------------------------------------------------
//  Isles v1.0
//  
//  Copyright 2008 (c) Nightin Games. All Rights Reserved.
//-----------------------------------------------------------------------------

using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Isles.Graphics;

namespace Isles.Engine
{
    #region IScreen
    /// <summary>
    /// Represents a game screen
    /// </summary>
    public interface IScreen : IDisposable
    {
        /// <summary>
        /// Called when this screen is activated
        /// </summary>
        void Enter();

        /// <summary>
        /// Called when this screen is deactivated
        /// </summary>
        void Leave();

        /// <summary>
        /// Handle game updates
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Handle game draw event
        /// </summary>
        /// <param name="gameTime"></param>
        void Draw(GameTime gameTime);

        /// <summary>
        /// Load your graphics content.  If loadAllContent is true, you should
        /// load content from both ResourceManagementMode pools.  Otherwise, just
        /// load ResourceManagementMode.Manual content.
        /// </summary>
        /// <param name="loadAllContent">Which type of content to load.</param>
        void LoadContent();
        
        /// <summary>
        /// Unload your graphics content.  If unloadAllContent is true, you should
        /// unload content from both ResourceManagementMode pools.  Otherwise, just
        /// unload ResourceManagementMode.Manual content.  Manual content will get
        /// Disposed by the GraphicsDevice during a Reset.
        /// </summary>
        /// <param name="unloadAllContent">Which type of content to unload.</param>
        void UnloadContent();
    }
    #endregion

    #region ILoading
    /// <summary>
    /// Interface for tracking loading progress
    /// </summary>
    public interface ILoading
    {
        /// <summary>
        /// Gets or sets current message
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets current progress.
        /// </summary>
        float Progress { get; }

        void Refresh(float newProgress);

        /// <summary>
        /// Refresh the loading screen with the new progress and message
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        void Refresh(float newProgress, string newMessage);

        /// <summary>
        /// Begins a new loading procedure
        /// </summary>
        void Reset();
    }
    #endregion
    
    #region IWorldObject
    /// <summary>
    /// Interface for a class of object that can be load from a file
    /// and drawed on the scene.
    /// E.g., Fireballs, sparks, static rocks...
    /// </summary>
    public interface IWorldObject
    {
        /// <summary>
        /// Gets the position of the scene object
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// Gets the axis-aligned bounding box of the scene object
        /// </summary>
        BoundingBox BoundingBox { get; }

        /// <summary>
        /// Gets or sets whether the position or bounding box of the
        /// scene object has changed since last update.
        /// </summary>
        /// <remarks>
        /// By marking the IsDirty property of a scene object, the scene
        /// manager will be able to adjust its internal data structure
        /// to adopt to the change of transformation.
        /// </remarks>
        bool IsDirty { get; set; }

        /// <summary>
        /// Every world object belongs to a class represented by a string.
        /// The classID is not necessarily equal to the name of a class.
        /// This property is mainly used for serialization/deserialization.
        /// </summary>
        string ClassID { get; set; }

        /// <summary>
        /// Update the scene object
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw the scene object
        /// </summary>
        /// <param name="gameTime"></param>
        void Draw(GameTime gameTime);

        /// <summary>
        /// Write the scene object to an XML element
        /// </summary>
        /// <param name="writer"></param>
        void Serialize(XmlElement xml);

        /// <summary>
        /// Read and initialize the scene object from a set of attributes
        /// </summary>
        /// <param name="reader"></param>
        void Deserialize(XmlElement xml);
    }
    #endregion

    #region IGameUI
    /// <summary>
    /// Describe the type of a game message
    /// </summary>
    public enum MessageType
    {
        Message, Warning, Error, Congratulation
    }

    /// <summary>
    /// In game user interface
    /// </summary>
    public interface IGameUI
    {
        /// <summary>
        /// Called when an entity is selected. Game UI should refresh
        /// itself to match the new entities, e.g., status and spells.
        /// </summary>
        void Select(Entity entity);

        /// <summary>
        /// Called when multiple entities are selected.
        /// </summary>
        void SelectMultiple(IEnumerable<Entity> entities);

        /// <summary>
        /// Popup a message
        /// </summary>
        void ShowMessage(MessageType type, string message, Vector2 position, Color color);
    }
    #endregion

    #region ILevel
    /// <summary>
    /// Interface for a game level
    /// </summary>
    public interface ILevel
    {
        /// <summary>
        /// Load a game level
        /// </summary>
        void Load(GameWorld world, ILoading progress);

        /// <summary>
        /// Unload a game level
        /// </summary>
        void Unload();

        /// <summary>
        /// Update level stuff
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw level stuff
        /// </summary>
        /// <param name="gameTime"></param>
        void Draw(GameTime gameTime);
    }
    #endregion

    #region ILandscape
    /// <summary>
    /// Interface for a landscape.
    /// The landscape lays on the XY plane, Z value is used to represent the height.
    /// The position of the landscape is fixed at (0, 0).
    /// </summary>
    public interface ILandscape
    {
        /// <summary>
        /// Gets the size of the landscape
        /// </summary>
        Vector3 Size { get; }

        /// <summary>
        /// Gets the height (Z value) of a point (x, y) on the landscape
        /// </summary>
        float GetHeight(float x, float y);

        /// <summary>
        /// Gets the height of a grid. 
        /// </summary>
        float GetGridHeight(int x, int y);

        /// <summary>
        /// Gets the number of grids
        /// </summary>
        Point GridCount { get; }

        /// <summary>
        /// Gets the size of single grid
        /// </summary>
        Vector2 GridSize { get; }

        /// <summary>
        /// Gets the position of a grid
        /// </summary>
        Vector2 GridToPosition(int x, int y);

        /// <summary>
        /// Gets the grid occupying the point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        Point PositionToGrid(float x, float y);

        /// <summary>
        /// Performs a ray landscape intersection test, the intersection point
        /// is returned.
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        Vector3? Intersects(Ray ray);

        /// <summary>
        /// Update the landscape
        /// </summary>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw the landscape
        /// </summary
        void Draw(GameTime gameTime);

        /// <summary>
        /// Draw the given texture onto the landscape
        /// </summary>
        void DrawSurface(Texture2D texture, Vector2 position, Vector2 size);
    }
    #endregion
}