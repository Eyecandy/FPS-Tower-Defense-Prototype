using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshScript : MonoBehaviour {
	[SerializeField]
	private Transform target;

	NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();
		agent.SetDestination (target.transform.position);
	}

}
