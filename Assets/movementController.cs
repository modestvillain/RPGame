using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour {

	Animator a;
	float speed=2f;

	
	void Start () {
		a = GetComponent<Animator> ();
	}

	void Update () {
		float pos = transform.position.x;
		if (Input.GetKey ("left")) {
			pos-=speed*Time.deltaTime;
			a.SetBool("onRight",true);
			if(transform.rotation != Quaternion.Euler(0, 180, 0)) transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		else a.SetBool("onRight",false);
		if (Input.GetKey ("right")) {
			pos+=speed*Time.deltaTime;
			a.SetBool("onRight",true);
			if(transform.rotation != Quaternion.Euler(0, 0, 0)) transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else a.SetBool("onRight",false);
		float translation = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		transform.position = new Vector3(pos, transform.position.y, transform.position.z);
	}


}
