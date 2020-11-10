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
        AsimInput.Instance.AddListener(AsimTrigger.Primary, AsimState.Down, HelloWorld);
    }

    private void OnDisable()
    {
        AsimInput.Instance.RemoveListener(AsimTrigger.Primary, AsimState.Down, HelloWorld);
    }

    private void HelloWorld(XRNode node, XRRayInteractor interactor)
    {
        Debug.Log("Hello World!");
    }
}
```