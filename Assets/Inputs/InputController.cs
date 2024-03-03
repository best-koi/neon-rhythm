using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System; //Required for actions

public class InputController : MonoBehaviour
{
    public ControlInputs playerInputs;

    public static event Action onDInput;
    public static event Action onFInput;
    public static event Action onJInput;
    public static event Action onKInput;
    public static event Action onDRelease;
    public static event Action onFRelease;
    public static event Action onJRelease;
    public static event Action onKRelease;
    
    public InputAction dInput;
    public InputAction fInput;
    public InputAction jInput;
    public InputAction kInput;

    private void Awake()  //initializes input object
    {
        playerInputs = new ControlInputs();
    }

    private void OnEnable() //Initializes input objects (names had to be D, F, J, and K b/c those are the action names in the Input Action asset)
    {
        dInput = playerInputs.DFJK.D;
        dInput.Enable();
        dInput.started += D;
        dInput.canceled += DRelease;

        fInput = playerInputs.DFJK.F; 
        fInput.Enable();
        fInput.started += F;
        fInput.canceled += FRelease;
        
        jInput = playerInputs.DFJK.J;
        jInput.Enable();
        jInput.started += J;
        jInput.canceled += JRelease;

        kInput = playerInputs.DFJK.K;
        kInput.Enable();
        kInput.started += K;
        kInput.canceled += KRelease;
    }

    private void OnDisable() //Disables for compile reasons
    {
        dInput.Disable();
        fInput.Disable();
        jInput.Disable();
        kInput.Disable();
    }

    //The following functions invoke the respective Actions for each of the button inputs to be used in JSONRead.cs
    private void D(InputAction.CallbackContext context) //Function for when D is pressed
    {
        onDInput?.Invoke();
    }

    private void F(InputAction.CallbackContext context) //Function for when F is pressed
    {
        onFInput?.Invoke();
    }

    private void J(InputAction.CallbackContext context) //Function for when J is pressed
    {
        onJInput?.Invoke();
    }

    private void K(InputAction.CallbackContext context) //Function for when K is pressed
    {
        onKInput?.Invoke();
    }

    private void DRelease(InputAction.CallbackContext context)
    {
        onDRelease?.Invoke();
    }

    private void FRelease(InputAction.CallbackContext context)
    {
        onFRelease?.Invoke();
    }

    private void JRelease(InputAction.CallbackContext context)
    {
        onJRelease?.Invoke();
    }

    private void KRelease(InputAction.CallbackContext context)
    {
        onKRelease?.Invoke();
    }
}
