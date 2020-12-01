# Radial Menu
With Radial menu tools can be easily selected in VR with controller's touchpad/joystick. 

## How to use
- Create a game object and add InputManager script to it
- Drag Radial Menu prefab into the scene under preferred controller
_To use Radial Menu there needs to be a script that implements functionality. Look for MaterialColorChange-script_

On Radial Menu's inspector under header **Events** there are 4 different sections **Top**, **Right**, **Bottom** and **Left** where the functionality can be put. 
Drag the script that implements functionality to each section. For every section select preferred function from drop-down list.

## Inputs  
Uses input: {yes}
Problems:
 - {list of problems with the current input(s) i.e. no VR implementation, requires extra states etc.}
Inputs:
 - {hand} {touchpad/joystick, touchpad/joystick press}: {Whenever touchpad/joystick is touched, Radial Menu is shown and vice versa. With touchpad/joystick press section's functionality can be activated}
 - {desktop input}: {No desktop input}


## Other

{Other notices/problems with the feature}
- Code is lightly commented
- Input should be implemented with the use of XRController (now XRNode)