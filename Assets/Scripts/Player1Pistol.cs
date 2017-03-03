﻿using UnityEngine;
using System.Collections;

public class Player1Pistol : MonoBehaviour {

	Transform myTransform;
	RaycastHit hit;
	float maxDist = 10000;

	//Bullethole
	public GameObject bulletHole;
	float distanceFromWall = 0.02f;

	float accuracy = 0.05f;

	// Use this for initialization
	void Start () {

		myTransform = transform;
	
	}

	// Update is called once per frame
	void Update () {

		//Firing the raycast
		if(Input.GetButtonDown("Fire1")){

			//Accuracy
			Vector3 direction = Vector3.forward;
			direction.x += Random.Range (-accuracy,accuracy);
			direction.y += Random.Range (-accuracy,accuracy);
			direction.z += Random.Range (-accuracy,accuracy);

			//Firing the raycast
			if(Physics.Raycast(myTransform.position, myTransform.TransformDirection(direction), out hit, maxDist)){

				//Instantiating the bullethole
				Instantiate(bulletHole, hit.point + (hit.normal * distanceFromWall), Quaternion.LookRotation(hit.normal));
				Debug.Log("Memes");
			}
		}
	}
}
