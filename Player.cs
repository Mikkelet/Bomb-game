using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    float speed = 1.0F;

    float gravity = 100000.0F;
    bool isGrounded = false;

    bool isJumping = false;
    float jumpTime = 0;
    float maxJumpTime = Mathf.PI / 2;
    float jumpSpeedMultiplier = 4.0F;

    Vector3 currentPos;


	// Use this for initialization
	void Start () {
        Physics.gravity = new Vector3(0, 0, gravity);
        transform.rigidbody.mass = 100.0F;
        currentPos = transform.localPosition;
	}
	
	// Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * (speed * 100) * Time.deltaTime, 0, 0));

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (jumpTime == 0)
            {
                currentPos = transform.localPosition;
                isJumping = true;
            }
        }

        Jumping();


    }
    void Jumping()
    {
        if (isJumping)
        {
            if (jumpTime < maxJumpTime)
            {
                jumpTime += 1 * (Time.deltaTime * jumpSpeedMultiplier);
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    currentPos.y + (Mathf.Sin(jumpTime) * 200),
                    transform.localPosition.z);
            }
            else { jumpTime = 0; isJumping = false; }
        }
    }
}
