﻿using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour {

	Animator a;
	CharacterController c;
	float speed=2f;

	
	void Start () {
		a = GetComponent<Animator> ();
		c = GetComponent<CharacterController> ();
	}

	void Update () {
		float posx = transform.position.x;
		float posy = transform.position.y;
		if (Input.GetKey ("left")) {
			posx-=speed*Time.deltaTime;
			a.SetBool("onRight",true);
			if(transform.rotation != Quaternion.Euler(0, 180, 0)) transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		else a.SetBool("onRight",false);
		if (Input.GetKey ("right")) {
			posx+=speed*Time.deltaTime;
			a.SetBool("onRight",true);
			if(transform.rotation != Quaternion.Euler(0, 0, 0)) transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else a.SetBool("onRight",false);
		float x = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		posy -= 9.81f * Time.deltaTime;
		c.SimpleMove(new Vector3(0, 0, 0) );
		//transform.position = new Vector3(pos, transform.position.y, transform.position.z);
		if(c.isGrounded) Debug.Log("yeah");
	}


}