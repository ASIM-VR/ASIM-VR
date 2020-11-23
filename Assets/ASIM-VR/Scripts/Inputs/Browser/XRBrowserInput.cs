using System;
using UnityEngine.XR;
using ZenFulcrum.EmbeddedBrowser.VR;

namespace AsimVr.Demo
{
    /// <summary>
    /// This script is extension for EmbeddedBrowser.
    /// </summary>
    public class XRBrowserInput : VRInput
    {
        public override float GetAxis(XRNodeState node, InputAxis axis)
        {
            throw new NotImplementedException();
        }
    }
}