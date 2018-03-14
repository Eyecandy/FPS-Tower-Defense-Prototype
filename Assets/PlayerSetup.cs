using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
	public Behaviour[] componentsToDisable;

	Camera sceneCamera;
	[SerializeField]
	string remoteLayer = "RemotePlayer";

	void Start() {
		if (!isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
			}

			gameObject.layer = LayerMask.NameToLayer (remoteLayer);
		} 
		else {
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				
				sceneCamera.gameObject.SetActive (false);
			}

		}
		string _ID = "Player " +GetComponent<NetworkIdentity>().netId;
		transform.name = _ID;
	}

	void OnDisable() {
		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive (true);
		}
	}
}
