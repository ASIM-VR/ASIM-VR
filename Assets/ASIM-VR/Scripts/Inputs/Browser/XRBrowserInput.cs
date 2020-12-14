using System;
using UnityEngine.XR;

#if EmbeddedBrowser

using ZenFulcrum.EmbeddedBrowser.VR;

#endif

namespace AsimVr.Demo
{
    /// <summary>
    /// This script is extension for EmbeddedBrowser.
    /// </summary>
#if EmbeddedBrowser

    public class XRBrowserInput : VRInput
    {
        public override float GetAxis(XRNodeState node, InputAxis axis)
        {
            throw new NotImplementedException();
        }
    }

#else

    public class XRBrowserInput
    {
    }

#endif
}