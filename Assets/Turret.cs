using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	public Transform target;
	[Header("Attributes")]
	[SerializeField]
	private float range = 7f;
	[SerializeField]
	private  float fireRate = 1f;
	[SerializeField]
	private float fireCountDown = 1f;

	[Header("Unity Setup Fields")]
	[SerializeField]
	private  string enemyTag = "Enemy";
	[SerializeField]
	private Transform partToRotate;
	[SerializeField]
	private  float turnSpeed = 10f;
	[SerializeField]
	private Transform gunPoint;
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private ParticleSystem flare;



	// Use this for initialization
	void Start () {
		InvokeRepeating ("UpdateTarget" , 0f, 0.5f);
	}

	void UpdateTarget() {
		float shortestDistanceSoFar = Mathf.Infinity;
		GameObject nearestEnemyFound = null;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);

		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistanceSoFar) {
				shortestDistanceSoFar = distanceToEnemy;
				nearestEnemyFound = enemy;
			}
		}
		if (nearestEnemyFound != null && shortestDistanceSoFar <= range) {
			target = nearestEnemyFound.transform;
		} else {
			target = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			return;
		}

		Rotate ();
		if (fireCountDown <= 0) {
			TurretShoot ();
			fireCountDown = 1f;
		}
		fireCountDown -= Time.deltaTime;
	}
	//

	void Rotate() {
		
		Vector3 dir = target.position - transform.position;
		Quaternion lookRot = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRot,Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);


	}

	void TurretShoot() {
		flare.Play();
		GameObject _bulletGO = Instantiate (bulletPrefab, gunPoint.position,gunPoint.rotation);
		Debug.Log (_bulletGO.name);

		Bullet _bullet = _bulletGO.GetComponent<Bullet> ();
		if (_bullet != null) {
			_bullet.Seek (target);
		} 
	}
		
	void OnDrawGizmosSelected ()	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
