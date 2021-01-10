# Mouse XR Controller

XR Interaction toolkit does not support desktop input. Mouse XR Controller uses the test methods of XRController to simulate the controller inputs.

# How to use

 - Add MouseXRController to a game object

Serialized fields
| Field                 | Description                    |
| ---                   | ---                            |
| Right Hand Controller | XR Rig right hand XRController |
| Left Hand Controller  | XR Rig left hand XRController  |

# Inputs

| Button | Description                       |
| ---    | ---                               |
| F      | Invoke XRController.activateUsage |
| G      | Invoke XRController.selectUsage   |
| V      | Invoke XRController.uiPressUsage  |

## Other
 - This problem does not exist in the action based input in XR Interaction toolkit 0.10.0+ 