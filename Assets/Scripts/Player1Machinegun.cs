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

	// Use this for initialization
	void Start () {

		//Memes
		myTransform = transform;
	
	}

	// Update is called once per frame
	void Update () {

		//Firing the raycast
		if(Input.GetButton("Fire1") && Time.time > nextFire){
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
	}
}
