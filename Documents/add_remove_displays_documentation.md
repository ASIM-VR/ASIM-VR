# Add / Remove Displays

Tool for instantiating and destroying a given prefab. The prefab should contain a `Destroy` component so it can be destroyed.

## How to use

Serialized fields
| Field          | Description                                                       |
| ---            | ---                                                               |
| Display        | Prefab instantiated on add                                        |
| Controller Ray | Controller that is used for pointing target display when removing |

## Inputs  
| Button                 | Description                           |
| ---                    | ---                                   |
| Button.Trigger         | Add Display                           |
| Button.SecondaryButton | Remove target display                 |
| Button.Grip            | Use both controllers to resize object |