using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 1;
    public float jumpHeight = 1;
    public int jumpDuration = 10;
    private int currJumpDuration = 0;
    public int gravity = -1;
    // Start is called before the first frame update
    void Start()
    {
       characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
    }
    private void handleMovement()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetAxis("Jump") > 0 && characterController.isGrounded && currJumpDuration==0)
        {
            currJumpDuration = jumpDuration;
        }
        if (currJumpDuration > 0)
        {
            moveVector.y = jumpHeight;
            currJumpDuration--;
        }else if (!characterController.isGrounded)
        {
            moveVector.y = gravity;
        }

        
        moveVector.Normalize();
        characterController.Move(moveVector * Time.deltaTime * speed);
        
        
        //rotation
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - Camera.main.transform.position).magnitude * 1;
        Vector3 viewDirection = mouseRay.origin + mouseRay.direction * midPoint;
        viewDirection.y = transform.position.y;
        transform.LookAt(viewDirection);

    }
}
