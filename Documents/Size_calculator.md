# Size Calculator

What: calculates gameObject size that is selected.

why: In simulation that information might be needed.

How: When certain input is given with VR Controller or mouse(desktop) feature will count object size and
display it as (x,y,z) x,y and z being meters.

Similar functionality exist in XR Interaction Toolkit: no
{if similar functionality exist how is this different?}

## How to use

{how to use this feature (drag-and-drop prefab(s)/add component(s)/what goes where in serialized fields)}

WidthText: TextMeshProUGUI text component
LengthText: TextMeshProUGUI text component
DepthText: TextMeshProUGUI text component 
ObjectName: TextMeshProUGUI text component
ControllerRay: RightHand Controller (XRRayinteractor)
Controller: RightHand Controller (XRController)

## Inputs  
Uses input: yes
Problems:
 - {list of problems with the current input(s) i.e. no VR implementation, requires extra states etc.}
Inputs:
 - With VR controller user first need to select this feature in the radial menu and then can use triggerButton while raycast is on certain object
   then it will show object's sizes to the user.
 - {desktop input}: Left click on object with right Tag will show its sizes (x,y,z)
    and right click after that 


## Other

{Other notices/problems with the feature}