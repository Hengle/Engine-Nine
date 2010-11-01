#region Copyright 2009 (c) Nightin Games
//=============================================================================
//
//  Copyright 2009 (c) Nightin Games. All Rights Reserved.
//
//=============================================================================
#endregion


#region Using Directives
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion


namespace Isles
{
    public class SurfaceContainer : Collection<ISurface>, ISurface
    {
        public float ObjectHeight { get; set; }

        public bool TryGetHeightAndNormal(Vector3 position, out float height, out Vector3 normal)
        {
            // TODO: Include object height

            Vector3 v;
            float h;
            float min = float.MaxValue;
            bool result = false;

            height = 0;
            normal = Vector3.Zero;

            foreach (ISurface surface in this)
            {
                if (surface != null &&
                    surface.TryGetHeightAndNormal(position, out h, out v))
                {
                    if (Math.Abs(position.Z - h) < min)
                    {
                        height = h;
                        normal = v;

                        result = true;
                    }
                }
            }

            return result;
        }
    }
}