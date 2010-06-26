﻿#region Copyright 2009 - 2010 (c) Nightin Games
//=============================================================================
//
//  Copyright 2009 - 2010 (c) Nightin Games. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace Nine.Graphics.Landscape
{
    /// <summary>
    /// A triangle mesh constructed from heightmap to represent game terrain. 
    /// The up axis of the terrain is Vector.UnitZ.
    /// </summary>    
    public sealed class Terrain : IDisposable, ISurface, IPickable
    {
        /// <summary>
        /// Gets the underlying geometry that contains height, normal, tangent data.
        /// </summary>
        public TerrainGeometry Geometry { get; private set; }

        /// <summary>
        /// Gets the effects used to draw the terrain.
        /// </summary>
        public TerrainEffectCollection Effects { get; private set; }

        /// <summary>
        /// Gets the underlying GraphicsDevice.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }

        /// <summary>
        /// Gets the patches that made up this terrain.
        /// </summary>
        public ReadOnlyCollection<TerrainPatch> Patches { get; private set; }

        /// <summary>
        /// Gets the level of tessellation of each patch.
        /// </summary>
        public int PatchTessellation { get; private set; }

        /// <summary>
        /// Gets or sets any user data.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Gets or sets the name of the terrain.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the center position of the terrain.
        /// </summary>
        public Vector3 Position
        {
            get { return position; }
            set { position = value; UpdatePatchPositions(); }
        }

        private Vector3 position;

        /// <summary>
        /// Gets or sets the scale of the texture coordinates of terrain vertices.
        /// </summary>
        public Vector2 TextureScale
        {
            get { return textureScale; }
            set { textureScale = value; UpdatePatchTextureScale(); }
        }

        private Vector2 textureScale = Vector2.One;
        
        /// <summary>
        /// Gets the axis aligned bounding box of this terrain.
        /// </summary>
        public BoundingBox BoundingBox
        {
            get
            {
                BoundingBox box;

                box.Min = Geometry.BoundingBox.Min + position;
                box.Max = Geometry.BoundingBox.Max + position;

                return box;
            }
        }

        /// <summary>
        /// Creates a new instance of Terrain.
        /// </summary>
        /// <param name="step">Size of the smallest square block that made up the terrain.</param>
        /// <param name="tessellationU">Number of the smallest square block in X axis, or heightmap texture U axis.</param>
        /// <param name="tessellationV">Number of the smallest square block in Y axis, or heightmap texture V axis.</param>
        /// <param name="patchTessellation">Number of the smallest square block that made up the terrain patch.</param>
        public Terrain(GraphicsDevice graphics, float step, int tessellationU, int tessellationV, int patchTessellation)
            : this(graphics, new TerrainGeometry(step, tessellationU, tessellationV), patchTessellation)
        { }

        /// <summary>
        /// Creates a new instance of Terrain.
        /// </summary>
        /// <param name="geometry">The heightmap geometry to create from.</param>
        /// <param name="patchTessellation">Number of the smallest square block that made up the terrain patch.</param>
        public Terrain(GraphicsDevice graphics, TerrainGeometry geometry, int patchTessellation)
        {
            if (graphics == null || geometry == null)
                throw new ArgumentNullException();

            if (patchTessellation < 2 ||
                geometry.TessellationU % patchTessellation != 0 ||
                geometry.TessellationV % patchTessellation != 0)
            {
                throw new ArgumentOutOfRangeException();
            }


            Effects = new TerrainEffectCollection(this);
            PatchTessellation = patchTessellation;
            GraphicsDevice = graphics;
            Geometry = geometry;

            Geometry.Invalidate += (a, b) => Invalidate();

            // Create patches
            int xPatchCount = geometry.TessellationU / patchTessellation;
            int yPatchCount = geometry.TessellationV / patchTessellation;

            TerrainPatch[] patches = new TerrainPatch[xPatchCount * yPatchCount];

            int i = 0;

            for (int y = 0; y < yPatchCount; y++)
            {
                for (int x = 0; x < xPatchCount; x++)
                {
                    patches[i] = new TerrainPatch(graphics, geometry, x, y, patchTessellation);

                    i++;
                }
            }

            Patches = new ReadOnlyCollection<TerrainPatch>(patches);

            // Store these values in case they change
            tu = Geometry.TessellationU;
            tv = Geometry.TessellationV;
        }

        private int tu, tv;

        /// <summary>
        /// Update internal vertex buffer, index buffer of this terrain.
        /// Call this when you changed the byte Mask of TerrainPatchPart.
        /// No need to call this when you reload heightmap using TerrainGeometry.LoadHeightmap.
        /// </summary>
        public void Invalidate()
        {
            // Make sure we don't change geometry tessellation
            if (Geometry.TessellationU != tu ||
                Geometry.TessellationV != tv)
            {
                throw new InvalidOperationException();
            }

            foreach (TerrainPatch patch in Patches)
            {
                patch.Invalidate();
            }
        }

        /// <summary>
        /// Gets the position of a point.
        /// </summary>
        public Vector3 GetPosition(TerrainPatch patch, TerrainPatchPart part, Point index)
        {
            return patch.GetPosition(part, index);
        }
        
        private void UpdatePatchPositions()
        {
            foreach (TerrainPatch patch in Patches)
            {
                patch.Position = position;
            }
        }

        private void UpdatePatchTextureScale()
        {
            foreach (TerrainPatch patch in Patches)
            {
                patch.TextureScale = textureScale;
            }

            Invalidate();
        }

        #region IDisposable Members
        /// <summary>
        /// Disposes any resources associated with this instance.
        /// </summary>
        public void Dispose()
        {
            foreach (TerrainPatch patch in Patches)
            {
                patch.Dispose();
            }
        }
        #endregion

        #region ISurface Members
        /// <summary>
        /// Gets the height and normal of the terrain at a given location.
        /// </summary>
        /// <returns>False if the location is outside the boundary of the terrain.</returns>
        public bool TryGetHeightAndNormal(Vector3 position, out float height, out Vector3 normal)
        {
            return Geometry.TryGetHeightAndNormal(position - Position, out height, out normal);
        }
        #endregion

        #region IPickable Members
        /// <summary>
        /// Points under the heightmap and are within the boundary are picked.
        /// </summary>
        /// <returns>This TerrainGeometry instance</returns>
        public object Pick(Vector3 point)
        {
            return Geometry.Pick(point - position) != null ? this : null;
        }
        
        /// <summary>
        /// Checks whether a ray intersects the terrain mesh.
        /// </summary>
        /// <returns>This TerrainGeometry instance</returns>
        public object Pick(Ray ray, out float? distance)
        {
            float minDistance = float.MaxValue;
            distance = null;
            int i = 0;
            Vector3[] vertices = new Vector3[3];                            

            foreach (TerrainPatch patch in Patches)
            {
                if (ray.Intersects(patch.BoundingBox).HasValue)
                {
                    foreach (TerrainPatchPart part in patch.PatchParts)
                    {
                        if (ray.Intersects(part.BoundingBox).HasValue)
                        {
                            i = 0;

                            foreach (Point pt in patch.PatchParts[i].Indices)
                            {
                                vertices[i++] = GetPosition(patch, part, pt);

                                if (i == 3)
                                {
                                    i = 0;

                                    RayExtensions.Intersects(ray, ref vertices[0],
                                                                  ref vertices[1],
                                                                  ref vertices[2],
                                                                  out distance);

                                    if (distance.HasValue && distance.Value < minDistance)
                                        minDistance = distance.Value;
                                }
                            }                    
                        }
                    }
                }
            }

            if (minDistance < float.MaxValue)
            {
                distance = minDistance;
                return this;
            }

            return null;
        }
        #endregion
    }
}