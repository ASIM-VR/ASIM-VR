#Screens Distance from the ground

What: Calculates display screens distance from the ground

why: Information might be useful while simulating

How: When certain input is given with VR Controller or mouse(desktop) feature will count screens distance from the ground and
display it as "{Object} is x meters from the ground"


Similar functionality exist in XR Interaction Toolkit: no
{if similar functionality exist how is this different?}

## How to use

{how to use this feature (drag-and-drop prefab(s)/add component(s)/what goes where in serialized fields)}

DistanceText: TextMeshProUGUI text component
ControllerRay: RightHand Controller (XRRayinteractor)
Controller: RightHand Controller (XRController)

## Inputs  
Uses input: yes
Problems:
 - {list of problems with the current input(s) i.e. no VR implementation, requires extra states etc.}
Inputs:
 - After this feature is selected with radial menu, you can use trigger button while pointing at a screen and it will display the distance from the ground
 - With desktop using left click on screen element will display the screens distance fro mthe ground as meters.


## Other

{Other notices/problems with the feature}