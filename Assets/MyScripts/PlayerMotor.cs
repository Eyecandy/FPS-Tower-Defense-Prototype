using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
	
	Vector3 velocity = Vector3.zero;
	private Rigidbody rb;
	private Vector3 rotation = Vector3.zero;
	private float cameraRotationX = 0f;
	private float currentCameraRorationX = 0f;
	[SerializeField]
	private Camera cam;

	[SerializeField]
	private float cameraRotationLimit = 85f;



	private Vector3 thrusterForce = Vector3.zero;

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
	public void CameraRotate(float _cameraRotationX) {
		cameraRotationX = _cameraRotationX;
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
		if (thrusterForce != Vector3.zero) {
			rb.AddForce (thrusterForce * Time.deltaTime,ForceMode.Acceleration);
		}

	}
	//perform rotation based
	void PerformRotation() {
		rb.MoveRotation (transform.rotation * Quaternion.Euler(rotation));

		if (cam != null) {
			//Set new rotation and clamp it.
			currentCameraRorationX -= cameraRotationX;
			currentCameraRorationX = Mathf.Clamp (currentCameraRorationX, -cameraRotationLimit, cameraRotationLimit);
			cam.transform.localEulerAngles = new Vector3 (currentCameraRorationX, 0f, 0f);
			
		}
	}

	//get force  vector3 for our thrusters
	public void ApplyThruster (Vector3 _thrusterForce) {
		thrusterForce = _thrusterForce;
	}


}