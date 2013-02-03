using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Bomb_Simple2 : MonoBehaviour
{

    public bool exploded = false;

    float radius = 70.0F;
    float power = 5000.0F;

    AnimationCurve aniCurve;

    void Awake()
    {
        aniCurve = new AnimationCurve();
        animation.wrapMode = WrapMode.Once;
    }

    void Update()
    {

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        if (exploded)
        {
            animation.Play();
            StartCoroutine(Destroy());
            foreach (Collider hit in colliders)
            {
                if (hit.rigidbody)
                    hit.rigidbody.AddExplosionForce(power, explosionPos, radius, 3.0F, ForceMode.Force);

                MainCamera.score += 10;
            }

            exploded = false;
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(animation.clip.length);
        Destroy(gameObject);
    }
}

/*using UnityEngine;
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
*/

/*using UnityEngine;
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

*/