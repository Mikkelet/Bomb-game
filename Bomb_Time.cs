using UnityEngine;
using System.Collections;

public class Bomb_Time : MonoBehaviour
{

    public Bomb bomb = new Bomb();

    // Use this for initialization
    void Start()
    {
        bomb.BombType = "Time Bomb";
        bomb.BombDescription = "This thing is a ticking bomb. literally.";
        bomb.Power = 5000;
        bomb.Radius = 70;
        bomb.Position = gameObject.transform.position;
        bomb.IsExploded = true;
        bomb.GameObj = gameObject;
    }
}

