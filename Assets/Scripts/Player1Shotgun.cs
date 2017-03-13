using UnityEngine;
using System.Collections;

public class Player1Shotgun : MonoBehaviour {

	Transform myTransform;
	RaycastHit hit;
	float maxDist = 10000;

	//Bullethole
	public GameObject bulletHole;
	float distanceFromWall = 0.02f;

	//Fire rate and accuracy
	float accuracy = 0.2f;
	float fireRate = 0.4f;
	float nextFire = 0;

	public string p1orp2; 

	// Use this for initialization
	void Start () {

		//Memes
		myTransform = transform;
	
	}

	// Update is called once per frame
	void Update () {

		//Firing the raycast
		if(Input.GetButtonDown("Shoot") && Time.time > nextFire){
			nextFire = Time.time + fireRate;

			for (int i = 0; i < 5; i++) {
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
}
