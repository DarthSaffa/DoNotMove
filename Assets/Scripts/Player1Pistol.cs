using UnityEngine;
using System.Collections;

public class Player1Pistol : MonoBehaviour {

	Transform myTransform;
	RaycastHit hit;
	float maxDist = 10000;

	//Bullethole
	public GameObject bulletHole;
	float distanceFromWall = 0.01f;

	// Use this for initialization
	void Start () {

		myTransform = transform;
	
	}
	
	// Update is called once per frame
	void Update () {

		//Firing the raycast
		if(Input.GetButtonDown("Fire1")){
			if(Physics.Raycast(myTransform.position, myTransform.TransformDirection(Vector3.forward), out hit, maxDist)){

				//Instantiating the bullethole
				Instantiate(bulletHole, hit.point + (hit.normal * distanceFromWall), Quaternion.LookRotation(hit.normal));
				Debug.Log("Memes");
			}
		}
	}
}
