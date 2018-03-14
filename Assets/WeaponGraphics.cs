using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponGraphics :NetworkBehaviour {

	public ParticleSystem muzzleFlash;
	public GameObject hitEffect;

	[SerializeField]
	private ParticleSystem flare;

	void Update() {
		
		
	}

	void Flash(){
		flare.Play ();
	}
}
