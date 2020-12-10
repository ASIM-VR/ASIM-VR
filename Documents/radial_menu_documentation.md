# Radial Menu
With Radial menu tools can be easily selected in VR with controller's touchpad/joystick. 

## How to use
- Add `Assets/ASIM-VR/Prefabs/RadialMenu` prefab under a XRRig controller that uses the menu
- Make sure that the `hand` value in the prefabs radial menu component in `Menu` game object matches the parent XR controllers `controllerNode` value
- Update radial menus content by modifiying the `RadialMenu` component in the prefabs `Menu` game object

On Radial Menu's inspector under header **Events** there are 4 different sections **Top**, **Right**, **Bottom** and **Left** where the functionality can be put. 
Drag the script that implements functionality to each section. For every section select preferred function from drop-down list.

## Inputs
#### RMInputManager
| Button | Description |
| --- | --- |
| Button.Primary2DAxisTouch | Move and active item under radial menu cursor |

#### RadialMenuMockInput
| Button | Description |
| :-: | --- |
| 1 | Move radial menu cursor up |
| 2 | Move radial menu cursor right |
| 3 | Move radial menu cursor down |
| 4 | Move radial menu cursor left |
| e | Select active item |