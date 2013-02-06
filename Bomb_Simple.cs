using UnityEngine;
using System.Collections;

public class Bomb_Simple : MonoBehaviour {

    public Bomb bomb = new Bomb();

	// Use this for initialization
	void Start () {
        bomb.BombType= "SimpleBomb";
        bomb.BombDescription = "Your standard, run-of-the-mill explosives!";
        bomb.Power = 5000;
        bomb.Radius = 70;
        bomb.Position = gameObject.transform.position;
        bomb.IsExploded = true;
        bomb.GameObj = gameObject;
	}
}

