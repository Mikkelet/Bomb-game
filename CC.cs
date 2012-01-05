using UnityEngine;
using System.Collections;

public class CC : MonoBehaviour {

    CharacterController controller;
    int speed = 100;
    public float jumpSpeed = 800.0F;
    public float gravity = 2.0F;
    private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
    void Update()
    {
        //controller.SimpleMove(new Vector3(speed * Input.GetAxis("Horizontal"), 0));
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
