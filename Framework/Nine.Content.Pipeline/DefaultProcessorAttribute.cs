﻿#region Copyright 2011 (c) Engine Nine
//=============================================================================
//
//  Copyright 2011 (c) Engine Nine. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Directives
using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Nine.Content.Pipeline
{
    /// <summary>
    /// Specified the default process for a content type. The default processor will be used to 
    /// process the content when processing using DefaultContentProcessor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple=false)]
    public class DefaultProcessorAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the assembly quanlified name of the default processor.
        /// </summary>
        public string DefaultProcessor { get; set; }

        public DefaultProcessorAttribute() { }
        public DefaultProcessorAttribute(string defaultProcessor) { DefaultProcessor = defaultProcessor; }
        public DefaultProcessorAttribute(Type defaultProcessorType) { DefaultProcessor = defaultProcessorType.AssemblyQualifiedName; }
    }
}
