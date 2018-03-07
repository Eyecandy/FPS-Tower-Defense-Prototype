using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
	
	Vector3 velocity = Vector3.zero;
	private Rigidbody rb;
	private Vector3 rotation = Vector3.zero;
	private Vector3 cameraRotation = Vector3.zero;
	[SerializeField]
	private Camera cam;

	void Start() {
		rb = GetComponent<Rigidbody> ();

	}
	//gets a movement vector
	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}
	//gets a rotational vector
	public void Rotate(Vector3 _rotation) {
		rotation = _rotation;
	}
	//gets a rotational vector for the camera
	public void CameraRotate(Vector3 _cameraRotation) {
		cameraRotation = _cameraRotation;
	}

	void FixedUpdate() {
		PerformMovement ();
		PerformRotation ();
	}

	//perform movement based on velocity vector
	void PerformMovement() {
		if (velocity != Vector3.zero) {
			rb.MovePosition (rb.position+ velocity * Time.deltaTime);
		}

	}
	//perform rotation based
	void PerformRotation() {
		rb.MoveRotation (transform.rotation * Quaternion.Euler(rotation));
		if (cam != null) {
			cam.transform.Rotate (cameraRotation);
		}
	}
}