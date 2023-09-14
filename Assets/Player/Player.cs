using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    PlayerInput input;

    Vector3 destination;

    [SerializeField] Transform[] lane_transforms;
    [SerializeField] float MoveSpeed = 20.0f;
    [SerializeField] float JumpingHeight=2.5f;
    [SerializeField] Transform GroundCheckTransform;
    [SerializeField][Range(0, 1)] float GroundCheckRadius = 0.2f;
    [SerializeField] LayerMask GroundCheckMask;

    int current_lane_index;

    // refernce for the animator 
    Animator animator;


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
        input.GamePlay.Move.performed += MovePerformed;
        input.GamePlay.Jump.performed += JumpPerformed;

        for (int i = 0; i < lane_transforms.Length; i++)
        {
            if (lane_transforms[i].position == transform.position)
                {

                destination= lane_transforms[i].position;
                current_lane_index = i;
            }
        }

        animator = GetComponent<Animator>();
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (IsOnGround())
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                float jump_vel = MathF.Sqrt(Physics.gravity.magnitude * JumpingHeight * 2);
                rigidbody.AddForce(new Vector3(0.0f, jump_vel, 0.0f), ForceMode.VelocityChange);
            }
        }
    }

    private void MovePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        float input_value= obj.ReadValue<float>();
        
        if (input_value < 0.0f) { MoveRight(); }
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
        if (!IsOnGround())
        {
            Debug.Log("We are not on the ground");
            animator.SetBool("isOnGround", false);
        }
        else
        {
            Debug.Log("We are on the ground!");
            animator.SetBool("isOnGround", true);
        }
        float tramsform_x = Mathf.Lerp(transform.position.x, destination.x, Time.deltaTime * MoveSpeed);
        transform.position = new Vector3(tramsform_x, transform.position.y, transform.position.z);
    }

    bool IsOnGround()
    {
       return Physics.CheckSphere(GroundCheckTransform.position, GroundCheckRadius, GroundCheckMask);

    }
}
