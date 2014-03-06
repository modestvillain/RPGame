using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour {

	Animator a;
	CharacterController c;
	GameObject player;
	float speed=2f;
	bool facingRight=true;

	void Start () {
		a = GetComponent<Animator> ();
		c = GetComponent<CharacterController> ();
		player = c.gameObject;
	}

	void Update () {
		float posx = transform.position.x;
		float posy = transform.position.y;
		if (Input.GetKey ("left")) {
			posx-=speed*Time.deltaTime;

			if(facingRight)	{
				gameObject.SetActive(false);
				Flip (player);
				Debug.Log (transform.localScale + "LEFT");
			}
			a.SetBool("onRight",true);
			gameObject.SetActive(true);
		}
		else if (Input.GetKey ("right")) {
			posx+=speed*Time.deltaTime;

			if(!facingRight) {
				Flip (player);
				Debug.Log (transform.localScale + "RIGHT");
			}
			a.SetBool("onRight",true);
		}
		else a.SetBool("onRight",false);
		c.SimpleMove(Physics.gravity);
		//float x = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		//posy -= 9.81f * Time.deltaTime;
		//c.SimpleMove(new Vector3(1, 1, 0) );
		//transform.position = new Vector3(pos, transform.position.y, transform.position.z);
		if(c.isGrounded) Debug.Log("yeah");
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Debug.Log ("collided");
		c.SimpleMove(new Vector3(0,0,0) );
	}

	void Flip(GameObject g) {
		Vector3 v = g.transform.localScale;
		v.x *= -1;
		g.transform.localScale = v;
		facingRight=!facingRight;
	}
}
