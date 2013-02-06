using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System;
using System.Collections;

public class Bomb
{
    private string bombType;
    private string bombDesciption;
    private bool isExploded;
    private int power;
    private int radius;
    private Vector3 position;
    private GameObject gameObj;

    #region Properties
    public string BombType
    {
        get { return bombType; }
        set { bombType = value; }
    }

    public string BombDescription
    {
        get { return bombDesciption; }
        set { bombDesciption = value; }
    }

    public bool IsExploded
    {
        get { return isExploded; }
        set { isExploded = value; }
    }

    public int Power
    {
        get { return power; }
        set { power = value; }
    }

    public int Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    public GameObject GameObj
    {
        get { return gameObj; }
        set { gameObj = value; }
    }
    #endregion
    #region methods

    public void Explode()
    {
        isExploded = true;
        if (isExploded)
        {
            
            Collider[] colliders = Physics.OverlapSphere(position, radius);
            gameObj.animation.Play();
            gameObj.AddComponent<MonoBehaviour>().StartCoroutine(WaitToRemove(gameObj.animation.clip.length, gameObj));
            foreach (Collider hit in colliders)
            {
                if (hit.rigidbody)
                    hit.rigidbody.AddExplosionForce(power, position, radius, 3.0F, ForceMode.Force);
                MainCamera.score += 10;
            }

            isExploded = false;
        }
    }

    public void ExplodeOnCollision()
    {
        if (isExploded)
        {
            
        }
    }

    // WAITS

    private IEnumerator WaitToRemove(float time, GameObject GObj)
    {
        yield return new WaitForSeconds(time);
        Object.Destroy(GObj);
    }

    public IEnumerator WaitToExplode(float time)
    {
        yield return new WaitForSeconds(time);
        Explode();
    }

    #endregion
    /* content
         * VARIABLES
            * BOOL ifExploded
            * FLOAT Power
            * FLOAT Radius
            * Animation?
            *
         * FUNCTIONS
            * Explode function
                * Foreach
                * check colliders
                * Add points
            * wait destroy (IEnumerator)
            * Wait to explode (time bomb)
         */
}
