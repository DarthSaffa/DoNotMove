using UnityEngine;
using System.Collections;

public class Player1Machinegun : MonoBehaviour {

	Transform myTransform;
	RaycastHit hit;
	float maxDist = 10000;

	//Bullethole
	public GameObject bulletHole;
	float distanceFromWall = 0.02f;

	//Fire rate and accuracy
	float accuracy = 0.08f;
	float fireRate = 0.075f;
	float nextFire = 0;

	//LineRenderer
	LineRenderer myLine;
	float myLineCurrentCooldown = 0;
	float myLineMaxCooldown = 0.05f;
	GameObject barrel;
	public string barrelName;

	public string p1orp2;

	// Use this for initialization
	void Start () {

		//Memes
		myTransform = transform;
		myLine = GetComponent<LineRenderer>();
		barrel = GameObject.Find (barrelName);
	
	}

	// Update is called once per frame
	void Update () {

		//Firing the raycast
		if(Input.GetAxis(p1orp2 + "Shoot") == 1 && Time.time > nextFire){
			myLineCurrentCooldown = myLineMaxCooldown;
			nextFire = Time.time + fireRate;
			//Accuracy
			Vector3 direction = Vector3.forward;
			direction.x += Random.Range (-accuracy,accuracy);
			direction.y += Random.Range (-accuracy,accuracy);
			direction.z += Random.Range (-accuracy,accuracy);

			//Firing the raycast
			if(Physics.Raycast(myTransform.position, myTransform.TransformDirection(direction), out hit, maxDist)){

				//Instantiating the bullethole
				Instantiate(bulletHole, hit.point + (hit.normal * distanceFromWall), Quaternion.LookRotation(hit.normal));
			}
		}
		//Making the line cooldown go down
		myLineCurrentCooldown -= Time.deltaTime;

		//Enabling and disabling line renderer
		if (myLineCurrentCooldown > 0) {
			myLine.enabled = true;
		} else {
			myLine.enabled = false;
		}
			
		//Setting the position of the line
		myLine.SetPosition (0, barrel.transform.position);
		myLine.SetPosition (1, hit.point);
	}
}
