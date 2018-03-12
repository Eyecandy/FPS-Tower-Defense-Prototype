using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public float hp = 10f;

	// Use this for initialization
	public void ReduceHealth(float dmg) {
		hp = hp -  dmg;
		if (hp <= 0) {
			Destroy (gameObject);
		}
	}
}
