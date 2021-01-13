# Grab Manager

`GrabManager` provides custom grabbing for game objects with `GrabTarget` component. 

| Style      | Description                                                                        |
| ---        | ---                                                                                |
| Grab       | Match position and rotation to the controller                                      |
| GrabOffset | Match position and rotation to the controller while maintaining the initial offset |
| Laser      | Match position and rotation to the pointed surface                                 | 

# How to use

 - Add `TextTimer` component to the scene
 - Add a `GrabManager` component to game objects with `XRController` component
 - For each grab manager, add a reference to the text timer 
 - Add a `GrabTarget` component to the game objects you want to grab
 - Update Rigidbody values if required  

# Inputs

| Button                    | Description                                    |
| ---                       | ---                                            |
| Button.Grip               | Try to grab object with `GrabTarget` component |
| Button.Primary2DAxisClick | Change grab style                              |
| Button.PrimaryAxis2DUp    | Move grabbed object forward                    |
| Button.PrimaryAxis2DDown  | Move grabbed object backward                   |

## Other
 - `XRGrabInteractable` provides matching functionaly to the `Grab` grabbing style