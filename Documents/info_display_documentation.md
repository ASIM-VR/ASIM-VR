# Info Display

Component that uses canvas text to show infomation to user.

## How to use

Add `Assets/ASIM-VR/Prefabs/InfoDisplay/InfoDisplay` prefab to your scene.

To set text:
```C#
InfoDisplay.Instance.SetText("Example text!");
```
Set multiline text 
```C#
InfoDisplay.Instance.SetText("Example text, first line!", "Example text, 2nd line!");
```
Clear text
```C#
InfoDisplay.Instance.ClearText();
```

# Other

 - Only one InfoDisplay should exist in the scene at once.