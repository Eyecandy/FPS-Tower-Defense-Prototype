using UnityEngine;
using UnityEngine.Networking;
[RequireComponent(typeof(PlayerManager))]
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

		GetComponent<PlayerManager>().Setup ();

	}
	public override void OnStartClient ()
	{
		base.OnStartClient ();

		string _netId = GetComponent<NetworkIdentity> ().netId.ToString();

		PlayerManager _player = GetComponent<PlayerManager> ();
	
		GameManager.RegisterPlayer (_netId, _player);
	}

	void OnDisable() {
		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive (true);
		}
		GameManager.UnRegisterPlayer (transform.name);
	}
}
