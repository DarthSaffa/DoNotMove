using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	Transform myTransform;
	public static float movementSpeed = 30;
	float diagMovementSpeed;
	public static float currentMove;
	float mouseSensitivity = 7.5f;
	int jumpSpeed = 20;

	float verticalRotation = 0;
	public float upDownRange = 90;
	
	float verticalVelocity = 0;
	
	CharacterController characterController;

	public string p1orp2;

	GameObject myCam;

    int gravityScale = 4;
	
	// Use this for initialization
	void Start () {

		myTransform = transform;
		characterController = GetComponent<CharacterController>();
		myCam = GameObject.Find (p1orp2 + "Camera");

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
		float rotLeftRight = Input.GetAxis(p1orp2 + "Mouse X") * mouseSensitivity;
		myTransform.Rotate(0, rotLeftRight, 0);

		//Vertical rotation
		verticalRotation -= Input.GetAxis(p1orp2 + "Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		myCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

		// Movement
		float forwardSpeed = Input.GetAxis(p1orp2 + "Vertical") * currentMove;
		float sideSpeed = Input.GetAxis(p1orp2 + "Horizontal") * currentMove;

		//Fixing diagonal movement
		if (Input.GetAxisRaw (p1orp2 + "Horizontal") > 0 && Input.GetAxisRaw (p1orp2 + "Vertical") > 0) {
			currentMove = diagMovementSpeed;
		} else if (Input.GetAxisRaw (p1orp2 + "Horizontal") > 0 && Input.GetAxisRaw (p1orp2 + "Vertical") < 0) {
			currentMove = diagMovementSpeed;
		} else if (Input.GetAxisRaw (p1orp2 + "Horizontal") < 0 && Input.GetAxisRaw (p1orp2 + "Vertical") < 0) {
			currentMove = diagMovementSpeed;
		} else if (Input.GetAxisRaw (p1orp2 + "Horizontal") < 0 && Input.GetAxisRaw (p1orp2 + "Vertical") > 0) {
			currentMove = diagMovementSpeed;
		} else {
			currentMove = movementSpeed;
		}

		//Applying gravity to the controller
		verticalVelocity += Physics.gravity.y * Time.deltaTime * gravityScale;

		//Jumping
		if(characterController.isGrounded && Input.GetAxis(p1orp2 + "Jump") == 1) {
			verticalVelocity = jumpSpeed;
		}

		//Moving the player
		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
		speed = myTransform.rotation * speed;
		characterController.Move(speed * Time.deltaTime );
	}
}
