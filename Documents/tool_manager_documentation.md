
# Tool Manager

Automatically collects and deactivates game objects that contain a component inheriting `Tool` class. These tools can be activated by calling a public method implemented by the manager. This way only one type of tool can be active at once.

## How to use

Activate a tool by calling a public methods implemented by the ToolManager component.

```C#
ActivateAddRemove();
ActivateTapeMeasure(); 
ActivateObjectSize();
DeactivateTools();
```

To add a new tool:
 - Add tool type to `AsimTool` enum
 - Create new public method `ActivateToolType()`, where ToolType is replaced with the new type
 - The new method should call `ActivateTool(AsimTool.ToolType);`, where ToolType is replaced with the new type

## Other

- Radial Menu calls these public methods using UnityEvents