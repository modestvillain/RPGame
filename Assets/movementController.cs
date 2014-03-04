using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour {

	Animator a;
	float speed;

	
	void Start () {
		a = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetKey ("left")) {
			speed = -2;
			a.SetBool("onRight",true);
			if(transform.rotation != Quaternion.Euler(0, 180, 0)) transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		else a.SetBool("onRight",false);
		if (Input.GetKey ("right")) {
			a.SetBool("onRight",true);
			speed = 2;
			if(transform.rotation != Quaternion.Euler(0, 0, 0)) transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else a.SetBool("onRight",false);
		float translation = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		transform.Translate (translation , 0, 0);
	}


}
