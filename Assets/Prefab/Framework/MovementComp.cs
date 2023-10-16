using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComp : MonoBehaviour

{

    [SerializeField] float MoveSpeed = 20f;
    [SerializeField] Vector3 MoveDir = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void setMoveDir(Vector3 dir)
    {
        MoveDir = dir;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+= MoveDir * MoveSpeed * Time.deltaTime;
    }
}
