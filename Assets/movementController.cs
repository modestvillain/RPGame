using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour {

	Animator a;
	float speed = 2;

	
	void Start () {
		a = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetKey ("left")) {
			a.SetBool("onRight",true);
			if(transform.rotation != Quaternion.Euler(0, 180, 0)) transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		else a.SetBool("onLeft",false);
		if (Input.GetKey ("right")) {
			a.SetBool("onRight",true);
			if(transform.rotation != Quaternion.Euler(0, 0, 0)) transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else a.SetBool("onRight",false);
		float translation = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		transform.Translate (translation , 0, 0);
	}


}
