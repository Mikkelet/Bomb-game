using UnityEngine;
using System.Collections;

public class Bomb_Implosion : MonoBehaviour
{

    float radius = 70.0F;
    float power = -5000.0F;
    public bool exploded = false;

    void Update()
    {
        if (exploded && !animation.isPlaying)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
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
