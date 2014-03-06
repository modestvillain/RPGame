using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour {

	Animator a;
	CharacterController c;
	float speed=2f;
	bool facingRight=true,
		 facingLeft=false;

	
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
			if(facingRight)	{
				Debug.Log ("run");
				Vector3 v = transform.localScale;
				v.x *= -1;
				transform.localScale = v;
				facingRight=false;
				facingLeft=true;
			}
		}
		//else a.SetBool("onRight",false);
		else if (Input.GetKey ("right")) {
			posx+=speed*Time.deltaTime;
			a.SetBool("onRight",true);
			if(facingLeft)	{
				Vector3 v = transform.localScale;
				v.x *= -1;
				transform.localScale = v;
				facingLeft=false;
				facingRight=true;
			}
		}
		else a.SetBool("onRight",false);
		float x = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		//posy -= 9.81f * Time.deltaTime;
		//c.SimpleMove(new Vector3(1, 1, 0) );
		//transform.position = new Vector3(pos, transform.position.y, transform.position.z);
		if(c.isGrounded) Debug.Log("yeah");
	}


}
