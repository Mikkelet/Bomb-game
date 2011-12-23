using UnityEngine;
using System.Collections;

public class Bomb_Time : MonoBehaviour
{
    public bool exploded = false;
    public float timerEnd = 3;

    float radius = 70.0F;
    float power = 5000.0F;
    float timerStart = 0.0F;
    

    void Update()
    {
        if (exploded)
        {
            timerStart+=Time.deltaTime;
        }
        if (timerStart > timerEnd)
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
