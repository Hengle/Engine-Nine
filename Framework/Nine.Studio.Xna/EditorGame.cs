﻿#region Copyright 2009 - 2011 (c) Engine Nine
//=============================================================================
//
//  Copyright 2009 - 2011 (c) Engine Nine. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Directives
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Nine.Studio.Extensibility;
using System.ComponentModel;
using Nine.Studio.Controls;
using Nine.Studio.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Nine.Studio
{
    public class EditorGame<T> : EditorGame<T, T> { }

    public class EditorGame<TContent, TRunTime> : Game
    {
        public TContent Editable { get; internal set; }
        public TRunTime Drawable { get; internal set; }

        protected internal virtual TRunTime CreateRuntimeObject(GraphicsDevice graphics, TContent content)
        {
            if (typeof(TRunTime) == typeof(TContent))
                return (TRunTime)(object)content;

            PipelineBuilder<TContent> builder = new PipelineBuilder<TContent>();
            return builder.BuildAndLoad<TRunTime>(graphics, content);
        }
    }
}
