//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Inputs/Control Inputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ControlInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Control Inputs"",
    ""maps"": [
        {
            ""name"": ""DFJK"",
            ""id"": ""af46467b-7f48-4bc3-a101-6a9129cda019"",
            ""actions"": [
                {
                    ""name"": ""D"",
                    ""type"": ""Button"",
                    ""id"": ""5703deb2-48c2-45a7-b172-34c420e0fe97"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""F"",
                    ""type"": ""Button"",
                    ""id"": ""e02d7b07-d7c8-4101-890f-0b672f763e98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""J"",
                    ""type"": ""Button"",
                    ""id"": ""ac5d8498-62fd-412c-929a-19fa3fa74173"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""K"",
                    ""type"": ""Button"",
                    ""id"": ""70eb9ba0-c34a-48fc-91ba-157df50651aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""050456b2-b229-4d05-8a9a-1a62eb743555"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eac720db-75a2-49de-bf7b-27820d784194"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83def6ae-0582-4a91-9d7a-35109834135c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d5faf08-6ca3-42f4-9982-4657187bca1c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2194fd31-f8b8-4431-96d3-abfd87711d63"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""J"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c79de4cd-156b-413e-a4cb-cab96b8ee116"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""J"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a1c0215-8cb3-4fb9-a255-aaa6096db34d"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""K"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f377053b-bc84-4d0f-9cbf-aebce8f2340d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""K"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DFJK
        m_DFJK = asset.FindActionMap("DFJK", throwIfNotFound: true);
        m_DFJK_D = m_DFJK.FindAction("D", throwIfNotFound: true);
        m_DFJK_F = m_DFJK.FindAction("F", throwIfNotFound: true);
        m_DFJK_J = m_DFJK.FindAction("J", throwIfNotFound: true);
        m_DFJK_K = m_DFJK.FindAction("K", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // DFJK
    private readonly InputActionMap m_DFJK;
    private List<IDFJKActions> m_DFJKActionsCallbackInterfaces = new List<IDFJKActions>();
    private readonly InputAction m_DFJK_D;
    private readonly InputAction m_DFJK_F;
    private readonly InputAction m_DFJK_J;
    private readonly InputAction m_DFJK_K;
    public struct DFJKActions
    {
        private @ControlInputs m_Wrapper;
        public DFJKActions(@ControlInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @D => m_Wrapper.m_DFJK_D;
        public InputAction @F => m_Wrapper.m_DFJK_F;
        public InputAction @J => m_Wrapper.m_DFJK_J;
        public InputAction @K => m_Wrapper.m_DFJK_K;
        public InputActionMap Get() { return m_Wrapper.m_DFJK; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DFJKActions set) { return set.Get(); }
        public void AddCallbacks(IDFJKActions instance)
        {
            if (instance == null || m_Wrapper.m_DFJKActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DFJKActionsCallbackInterfaces.Add(instance);
            @D.started += instance.OnD;
            @D.performed += instance.OnD;
            @D.canceled += instance.OnD;
            @F.started += instance.OnF;
            @F.performed += instance.OnF;
            @F.canceled += instance.OnF;
            @J.started += instance.OnJ;
            @J.performed += instance.OnJ;
            @J.canceled += instance.OnJ;
            @K.started += instance.OnK;
            @K.performed += instance.OnK;
            @K.canceled += instance.OnK;
        }

        private void UnregisterCallbacks(IDFJKActions instance)
        {
            @D.started -= instance.OnD;
            @D.performed -= instance.OnD;
            @D.canceled -= instance.OnD;
            @F.started -= instance.OnF;
            @F.performed -= instance.OnF;
            @F.canceled -= instance.OnF;
            @J.started -= instance.OnJ;
            @J.performed -= instance.OnJ;
            @J.canceled -= instance.OnJ;
            @K.started -= instance.OnK;
            @K.performed -= instance.OnK;
            @K.canceled -= instance.OnK;
        }

        public void RemoveCallbacks(IDFJKActions instance)
        {
            if (m_Wrapper.m_DFJKActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDFJKActions instance)
        {
            foreach (var item in m_Wrapper.m_DFJKActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DFJKActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DFJKActions @DFJK => new DFJKActions(this);
    public interface IDFJKActions
    {
        void OnD(InputAction.CallbackContext context);
        void OnF(InputAction.CallbackContext context);
        void OnJ(InputAction.CallbackContext context);
        void OnK(InputAction.CallbackContext context);
    }
}