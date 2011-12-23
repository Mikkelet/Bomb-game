using UnityEngine;
using System.Collections;

public class DestroyPlane : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            Destroy(collision.gameObject);
        }
    }
}
