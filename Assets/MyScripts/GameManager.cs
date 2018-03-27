using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager singleton;
	public MatchSettings matchSettings;

	private const string PLAYER_PREFIX = "Player ";
	private static Dictionary<string,PlayerManager> players = new Dictionary<string,PlayerManager>();
	void Awake() {
		if (singleton != null) 
		{
			Debug.Log("More than one game Manager");
		}
		else 
		{
			singleton = this;
		}
	}


	public static void RegisterPlayer(string _netId,PlayerManager _playerManager) {
		string _playerId = PLAYER_PREFIX + _netId;
		Debug.Log (_playerId);
		players.Add (_playerId, _playerManager);
		_playerManager.transform.name = _playerId;
	}

	public static void UnRegisterPlayer(string _netId) {
		string _playerID = PLAYER_PREFIX + _netId;
		players.Remove (_playerID);
	}

	public static PlayerManager GetPlayer(string _playerID) {
		return players [_playerID];
	}
	/*
	void OnGUI() {
		GUILayout.BeginArea (new Rect (200, 200, 200, 200));
		GUILayout.BeginVertical ();

		foreach (string _playerID in players.Keys) {
			GUILayout.Label (_playerID + "   -    " + players [_playerID]);
		}
		GUILayout.EndVertical ();
		GUILayout.EndArea ();

		
	}
	*/


}
