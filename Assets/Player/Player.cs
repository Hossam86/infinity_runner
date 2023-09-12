using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    PlayerInput input;

    Vector3 destination;

    [SerializeField] Transform[] lane_transforms;
    [SerializeField] float move_speed = 2.0f;
    int current_lane_index;

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

        for (int i = 0; i < lane_transforms.Length; i++)
        {
            if (lane_transforms[i].position == transform.position)
                {

                destination= lane_transforms[i].position;
                current_lane_index = i;
            }
        }
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        float input_value= obj.ReadValue<float>();
        
        if (input_value < 0f) { MoveRight(); }
        else { MoveLeft(); }
    }

    private void MoveLeft()
    {
       if (current_lane_index==0) { return; }
        current_lane_index--;
        destination= lane_transforms[current_lane_index].position;
    }

    private void MoveRight()
    {
        if(current_lane_index==lane_transforms.Length-1) { return; }
        current_lane_index++;
        destination = lane_transforms[current_lane_index].position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * move_speed); ;
    }
}
