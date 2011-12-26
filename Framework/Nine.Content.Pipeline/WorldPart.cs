#region Copyright 2011 (c) Engine Nine
//=============================================================================
//
//  Copyright 2011 (c) Engine Nine. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Directives
using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Xaml;
#endregion

namespace Nine.Content.Pipeline
{
    /// <summary>
    /// Enables authoring world from several world parts using Xaml.
    /// </summary>    
    public static class WorldPart
    {
        /// <summary>
        /// Gets the external include file names of this world.
        /// </summary>
        public static List<string> GetIncludes(World target)
        {
            return new List<string>();
        }

        /// <summary>
        /// Sets the external include file names of this world.
        /// </summary>
        public static void SetIncludes(World target, List<string> value)
        {
            if (value != null)
            {
                var worldObjects = new List<WorldObject>();
                worldObjects.AddRange(target.WorldObjects);

                foreach (var include in value)
                {
                    var worldPart = XamlServices.Load(include) as World;
                    if (worldPart != null)
                    {
                        worldObjects.AddRange(worldPart.WorldObjects);
                    }
                }
                
                // Merge world object with the same name         
                target.WorldObjects.Clear();
                target.WorldObjects.AddRange(worldObjects.Where(x => !(x is WorldObject)));
                target.WorldObjects.AddRange(worldObjects.OfType<WorldObject>().Where(x => string.IsNullOrEmpty(x.Name)));
                target.WorldObjects.AddRange(worldObjects.OfType<WorldObject>().Where(x => !string.IsNullOrEmpty(x.Name))
                                                         .GroupBy(x => x.Name).Select(g => 
                                                             new WorldObject(g.Key, g.SelectMany(x => x.Components))));
            }
        }
    }
}