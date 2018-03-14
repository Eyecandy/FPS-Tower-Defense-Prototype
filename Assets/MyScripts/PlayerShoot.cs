
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {
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
	[Client]
	void Shoot() {
		//a variable that stores information about what's hit and where etc.
		RaycastHit _hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit,weapon.range,mask)) {
			Debug.Log ("We hit: " + _hit.collider.name + " "+ _hit.collider.tag);

			if (_hit.collider.CompareTag ("Player")) {
				CmdPlayerShot (_hit.collider.name);
			}

		}
	}

	[Command]
	void CmdPlayerShot(string _ID) {
		
		Debug.Log (_ID + " has been shot");
		
	}
}
