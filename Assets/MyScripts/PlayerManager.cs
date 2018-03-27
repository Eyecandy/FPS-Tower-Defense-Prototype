using System.Collections;

using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour{
	[SyncVar]
	private bool _isDead = false;
	public bool isDead {
		get { return _isDead;}
		protected set { _isDead = value; }
	}

	[SerializeField]
	Behaviour[] disabledOnDeath;
	private bool[] wasEnabled;
	[SerializeField]
	private int maxHealth = 100;
	//current health needs to be synced across clients.
	[SyncVar] //this does that, pushes it out to all of the clients.
	private int currentHealth;

	public void Setup() {
		isDead = false;
		wasEnabled = new bool[disabledOnDeath.Length];
		for (int i = 0; i < wasEnabled.Length; i++) {
			wasEnabled [i] = disabledOnDeath [i].enabled;
		}
		SetDefaults ();
	}
	/*
	void Update() {
		if (isLocalPlayer) {
			if (Input.GetKeyDown (KeyCode.K)) {
				RpcTakeDamage (1000);

			}
		}
	}
	*/

	IEnumerator respawn() {
		yield return new WaitForSeconds (GameManager.singleton.matchSettings.respawnTime);
		SetDefaults ();
		Transform _spawnPoint = NetworkManager.singleton.GetStartPosition ();
		transform.position = _spawnPoint.position;
		transform.rotation = _spawnPoint.rotation;
	}

	public void SetDefaults() {
		Debug.Log ("SetDefaults");
		isDead = false;

		currentHealth = maxHealth;

		for (int i = 0; i < disabledOnDeath.Length; i++) {
			disabledOnDeath [i].enabled = wasEnabled [i];
		}

		Collider _collder = GetComponent<Collider> ();
		if (_collder != null) {
			_collder.enabled = true;
		}

	}
	[ClientRpc]
	public void RpcTakeDamage(int _amount) {
		if (!isDead) {
			currentHealth -= _amount;
			Debug.Log (transform.name + " now has " + currentHealth + "health");
			if (currentHealth <= 0) {
				Die ();
			}
		} 

	}

	private void Die(){
		isDead = true;

		Debug.Log (transform.name = "Dead");
		//call respawn method

		for (int i = 0; i < disabledOnDeath.Length; i++) {
			disabledOnDeath [i].enabled = false;
		}

		Collider _collder = GetComponent<Collider> ();
		if (_collder != null) {
			_collder.enabled = false;
		}

		StartCoroutine (respawn ());

	}
}
