#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace Nine.Graphics.Effects.Deferred
{
#if !WINDOWS_PHONE

    public partial class DeferredPointLight
    {
		private void OnCreated() { }
		private void OnClone(DeferredPointLight cloneSource) { }
		private void OnApplyChanges() { }
    }	

#endif
}
