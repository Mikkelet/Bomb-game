using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.gravity = new Vector3(0, -100, 0);
        rigidbody.mass = 0.1F;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collider.name == "Cube" && MainCamera.score != 0 && MainCamera.won == true)
        {
            MainCamera.score += 5;
        }
    }
}
