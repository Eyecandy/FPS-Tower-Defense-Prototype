using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))] //No error checking required when using GetComponent<PlayerMotor>();
public class PlayerController : MonoBehaviour {

	[SerializeField]// private field becomes visible in inspector
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 5f;
	[SerializeField]
	private float thrusterForce = 1000f;


	private PlayerMotor motor;

	void Start() {
		motor = GetComponent<PlayerMotor> ();
	}
	void Update(){
		//Calculate movement velocity as 3 vector
		float _xMov = Input.GetAxis("Horizontal");
		float _zMov = Input.GetAxis("Vertical");
		Vector3 _moveHor = transform.right * _xMov; //transform.right = vector(1,0,0)
		Vector3 _moveVer = transform.forward * _zMov; //transform.forward = vector(0,0,1)
		//final movement vector
		Vector3 _velocity = (_moveHor + _moveVer).normalized * speed;
		//apply movement vector
		motor.Move (_velocity);

		//Calculate rotation as 3D vector (turning around)
		float _yRot = Input.GetAxis ("Mouse X");//x val of mouse, we want to rotate around y. (turning)
		Vector3 _rotation = new Vector3 (0f, _yRot, 0f) * lookSensitivity;
		//apply rotation vector to player
		motor.Rotate(_rotation);

		//Calculate camera rotation
		float _xRot = Input.GetAxis ("Mouse Y");//x val of mouse, we want to rotate around y. (turning)

		float _cameraRotationX = _xRot * lookSensitivity;

		//apply rotation vector to player
		motor.CameraRotate(_cameraRotationX);

		//Apply thruster force
		Vector3 _thrusterForce = Vector3.zero;

		if (Input.GetButton("Jump")) {
			_thrusterForce = Vector3.up * thrusterForce;

		}
		motor.ApplyThruster (_thrusterForce);

	
	}
}