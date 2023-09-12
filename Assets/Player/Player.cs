using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    PlayerInput input;

    private void OnEnable()
    {
        if (input == null)
        {
            input = new PlayerInput();
        }
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        input.GamePlay.Move.performed += Move_performed;
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        float input_value= obj.ReadValue<float>();
        Debug.Log($"Move Action Performed with value {input_value}.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
