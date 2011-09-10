#region Copyright 2009 - 2010 (c) Engine Nine
//=============================================================================
//
//  Copyright 2009 - 2010 (c) Engine Nine. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Nine.Graphics
{
    public interface IAmbientLight
    {
        /// <summary>
        /// Gets or sets the ambient light color.
        /// </summary>
        Vector3 AmbientLightColor { get; set; }
    }

    public interface IDirectionalLight
    {
        Vector3 Direction { get; set; }
        Vector3 DiffuseColor { get; set; }
        Vector3 SpecularColor { get; set; }
    }

    public interface IPointLight
    {
        Vector3 Position { get; set; }
        Vector3 DiffuseColor { get; set; }
        Vector3 SpecularColor { get; set; }
        float Range { get; set; }
        float Attenuation { get; set; }
    }

    public interface ISpotLight
    {
        Vector3 Position { get; set; }
        Vector3 Direction { get; set; }
        Vector3 DiffuseColor { get; set; }
        Vector3 SpecularColor { get; set; }

        float Range { get; set; }
        float Attenuation { get; set; }
        float InnerAngle { get; set; }
        float OuterAngle { get; set; }
        float Falloff { get; set; }
    }
}