using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
	
	Transform myTransform;
	public static float movementSpeed = 12;
	float diagMovementSpeed;
	public static float currentMove;
	int mouseSensitivity = 5;
	int jumpSpeed = 6;

	float verticalRotation = 0;
	public float upDownRange = 90;
	
	float verticalVelocity = 0;
	
	CharacterController characterController;
	
	// Use this for initialization
	void Start () {

		myTransform = transform;
		characterController = GetComponent<CharacterController>();

		//Locking the cursor
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		//Keeping diagonal speed consisent with changing movement speeds
		diagMovementSpeed = movementSpeed * 2 / 3;
		currentMove = movementSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation

		//Horizontal rotation
		float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
		myTransform.Rotate(0, rotLeftRight, 0);

		//Vertical rotation
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * currentMove;
		float sideSpeed = Input.GetAxis("Horizontal") * currentMove;

		//Fixing diagonal movement
		if (Input.GetAxisRaw ("Horizontal") > 0 && Input.GetAxisRaw ("Vertical") > 0) {
			currentMove = diagMovementSpeed;
		} else if (Input.GetAxisRaw ("Horizontal") > 0 && Input.GetAxisRaw ("Vertical") < 0) {
			currentMove = diagMovementSpeed;
		} else if (Input.GetAxisRaw ("Horizontal") < 0 && Input.GetAxisRaw ("Vertical") < 0) {
			currentMove = diagMovementSpeed;
		} else if (Input.GetAxisRaw ("Horizontal") < 0 && Input.GetAxisRaw ("Vertical") > 0) {
			currentMove = diagMovementSpeed;
		} else {
			currentMove = movementSpeed;
		}

		//Applying gravity to the controller
		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		//Jumping
		if(characterController.isGrounded && Input.GetButton("Jump") ) {
			verticalVelocity = jumpSpeed;
		}

		//Moving the player
		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
		speed = myTransform.rotation * speed;
		characterController.Move(speed * Time.deltaTime );
	}
}
