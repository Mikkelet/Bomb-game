using UnityEngine;
using System.Collections;

public class Bomb_Collision : MonoBehaviour {

    public bool exploded = false;

    float radius = 70.0F;
    float power = 5000.0F;

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.name == "Cube")
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            if (exploded && !animation.isPlaying)
            {
                foreach (Collider hit in colliders)
                {
                    if (hit.rigidbody)
                        hit.rigidbody.AddExplosionForce(power, explosionPos, radius, 3.0F, ForceMode.Force);
                    MainCamera.score += 10;
                    exploded = false;
                }
                Destroy(gameObject);
            }
        }

    }

    
}
