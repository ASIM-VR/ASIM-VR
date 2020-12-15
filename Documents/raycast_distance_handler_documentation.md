# Raycast Distance Handler

Tool for calculating, visualizing and displaying distance between two points on trigger press.

## How to use

- Add RaycastDistanceHandler to gameObject.
- Add XRcontroller that contains. XRRayInteractor to  "Controller Raycast"
- Add a Line Drawer to RaycastDistanceHandler.
- Make sure either "Right Hand" or "Left Hand" is selected in "Hand" menu.

## Inputs  
| Button | Description |
| --- | --- |
| Button.Trigger | 1st press: Set point 1. 2nd press: Set point 2. Calculates and displays distance between these two points. 3rd press: Reset these points and the displayed text. |