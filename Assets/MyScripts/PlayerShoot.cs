using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
	public PlayerWeapon weapon;
	[SerializeField]
	private Camera cam;
	[SerializeField]
	private LayerMask mask; //it helps us control what will be hit. i.e other players, stuff with collider bodies.
	[SerializeField]
	private ParticleSystem flare;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			Debug.LogError ("No cam found");
			this.enabled = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			Shoot ();
			flare.Play ();
		}
	}

	void Shoot() {
		//a variable that stores information about what's hit and where etc.
		RaycastHit _hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit,weapon.range,mask)) {
			Debug.Log ("We hit: " + _hit.collider.name);
			if (_hit.collider.CompareTag ("Enemy")) {
				Health healthEnemy = _hit.rigidbody.GetComponentInParent<Health> ();
				healthEnemy.ReduceHealth (1);
					
			}

		}
	}
}
