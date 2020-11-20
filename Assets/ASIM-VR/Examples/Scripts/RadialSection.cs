using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class RadialSection
{
    public Sprite icon = null;
    public SpriteRenderer iconRenderer = null;
    //For hooking up functionality in editor
    public UnityEvent onPress = new UnityEvent();
}
