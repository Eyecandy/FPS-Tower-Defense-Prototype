using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.tag);
		if (other.CompareTag("Enemy")){
			Debug.Log ("Enemy in da house");
			Destroy (other.gameObject);
		}
	}
}
