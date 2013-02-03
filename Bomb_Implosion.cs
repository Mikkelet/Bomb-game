using UnityEngine;
using System.Collections;

public class Bomb_Implosion : MonoBehaviour
{

    public Bomb bomb = new Bomb();

    // Use this for initialization
    void Start()
    {
        bomb.BombType = "Implosion Bomb";
        bomb.BombDescription = "Your standard, run-of-the-mill anti-explosives!";
        bomb.Power = -5000;
        bomb.Radius = 70;
        bomb.Position = gameObject.transform.position;
        bomb.IsExploded = true;
        bomb.GameObj = gameObject;
    }
}

