# MouseXRController
[MouseXRController](../../Scripts/Inputs/MouseXRController.cs) provides limited support for XR Interaction toolkit components by using XRControllers test methods. However, it **does not** trigger feature values. Right hand controller is used by default, hold left control to use left hand controller.  

# AsimInput
[AsimInput](../../Scripts/Inputs/AsimInput.cs) provides a singleton that can be used to listen for XR controller inputs.

To use AsimInput you have to have a game object with the AsimInput component with the right and left hand controller references set in the inspector. The component will try to automatically find the controllers on reset.

```C#
//Access the current instance
AsimInput.Instance
//Use AddListener to listen for a trigger
AsimInput.Instance.AddListener(AsimTrigger, AsimState, TriggerAction)
//Use RemoveListener to remove a listener
AsimInput.Instance.RemoveListener(AsimTrigger, AsimState, TriggerAction)
```

### Input Implementation

You can implement custom input implementation by using the `IAsimInput` interface. The active implementation is set on `AsimInput.InitializeInput()` based on the current `XRSettings.loadedDeviceName`.

### Example 
In this simple example primary trigger listener is added on enable and removed on disable. When a primary trigger is pressed down, HelloWorld method is invoked. The HelloWorld method will receive a XRNode and XRRayInteractor as a parameter, these are from the controller that pressed the trigger. In this example the node and ray interactor are not used. However, they could be used to limit a method to a single hand by comparing the node.

```C#
public class InputExample : MonoBehaviour
{
    private void OnEnable()
	{
		AsimInput.Instance.AddListener(InputHelpers.Button.Trigger, AsimState.Down, HelloWorld);
	}

	private void OnDisable()
	{
		AsimInput.Instance.RemoveListener(InputHelpers.Button.Trigger, AsimState.Down, HelloWorld);
	}

	private void HelloWorld(XRController controller, XRRayInteractor interactor)
	{
		Debug.Log("Hello World!");
	}
}
```

## Embedded Browser
The primary goal of AsimInput is to make interaction between Embedded Browser and Unity XR possible. This is achieved by using reflections to inject values in to the original implementaion. This allows for a modification of the implementation without having to upload any part of the asset.  
By having a AsimInput component in the scene and adding [XRBrowserHand](../../Scripts/Inputs/Browser/XRBrowserHand.cs) component to XR Rig hand it can be used to interact with Embedded browser.