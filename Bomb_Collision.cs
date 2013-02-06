using UnityEngine;
using System.Collections;

public class Bomb_Collision : MonoBehaviour
{

    public Bomb bomb = new Bomb();

    // Use this for initialization
    void Start()
    {
        bomb.BombType = "CollBomb";
        bomb.BombDescription = "The super new collision detector bomb!";
        bomb.Power = 2000;
        bomb.Radius = 70;
        bomb.Position = gameObject.transform.position;
        bomb.IsExploded = false;
        bomb.GameObj = gameObject;
        print("ini done");
    }

    void OnTriggerEnter(Collider collider)
    {
        print(bomb.IsExploded);
        if (collider.transform.name == "Cube")
        {
            if (bomb.IsExploded)
            {
                bomb.Explode();
            }
        }
    }

}

