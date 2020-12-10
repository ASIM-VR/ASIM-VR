using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;

namespace AsimVr
{
    /// <summary>
    /// ASIM-VR Attribute to mark a class as MockHMD only, meaning it will be
    /// automatically disabled if MockHMD is not the loaded device.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MockHMDOnlyAttribute : Attribute
    {
        /// <summary>
        /// Automatically enable/disable components marked with the attribute.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeMockHDMOnlyComponents()
        {
            var active = XRSettings.loadedDeviceName == "MockHMD" ||
                         XRSettings.loadedDeviceName == "MockHMD Display";
            foreach(var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if(type.GetCustomAttributes(typeof(MockHMDOnlyAttribute), false).Length > 0)
                {
                    foreach(var result in UnityEngine.Object.FindObjectsOfType(type))
                    {
                        if(result is MonoBehaviour mono)
                        {
                            mono.enabled = active;
                        }
                    }
                }
            }
        }
    }
}