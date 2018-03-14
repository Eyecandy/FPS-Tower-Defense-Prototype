using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
	public Transform target;

	[SerializeField]
	private float speed = 10f;

	public void Seek(Transform _target) {
		target = _target;
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.tag);
		if (other.CompareTag ("Enemy")) {
			Health enemyHealth = other.gameObject.GetComponent<Health> ();
			enemyHealth.ReduceHealth (1);
		} 
		else {
			Destroy (gameObject);
		}
	}

	public void Update() {
		if (target == null) {
			Destroy (gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = Time.deltaTime * speed;
		transform.Translate (dir.normalized * distanceThisFrame, Space.World);

		/*
		if (dir.magnitude <= distanceThisFrame) {
			HitTarget ();
			return;
		}
		Debug.Log ("Translate");

		*/
	}

	void HitTarget() {
		Destroy (gameObject);
	}

}
