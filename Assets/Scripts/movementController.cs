using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
	
	Animator a;
	Collider2D box;
	float speed=2f,
		  maxHeight=.7f,
		  originalY=0f,
		  accel;
	bool facingRight=true,
		 colliding=false,
		 grounded=true,
		 reachedMax=false,
		 jumped=false;

	void Start () {

		a = GetComponent<Animator> ();
		box = GetComponent<BoxCollider2D>();
		Inventory.setup ();
	}
	
	void FixedUpdate () {

		/* 
		 * Get user input
		 */
		a.SetBool("attacking",false);

		if(Input.GetKeyDown("i") || Inventory.visible) {
			if((Input.GetKeyDown("i") && grounded)) {
				Inventory.toggleDisplay();
			}
			return;
		}
		if(Input.GetKeyDown("space") && grounded) {
			accel=75f;
			grounded=false;
			jumped=true;
			originalY=transform.position.y;
		}
		if(Input.GetKeyDown("e") && !jumped) {
			a.SetBool("attacking",true);
		}
		else if (Input.GetKey ("left")) {
			if(facingRight)	{
				Flip ();
			}
			a.SetBool("moving",true);
		}
		else if (Input.GetKey ("right")) {
			if(!facingRight) {
				Flip ();
			}
			a.SetBool("moving",true);
		}
		else a.SetBool("moving",false);

		rigidbody2D.velocity = Move ();
	}

	/*
	 * If collision came from below, reallow jumps
	 * 
	 * Original taken from here and then modified by me,
	 * "The only problem with it is if for some reason
	 * the initial collision is not below - but you
	 * could probably use OnCollisionStay to solve that"
	 * http://answers.unity3d.com/questions/40588/detect-collision-from-bottom.html
	 */
	void OnCollisionEnter2D(Collision2D c) {

		foreach(ContactPoint2D cp in c.contacts) {
			if(Vector2.Dot(cp.normal, Vector2.up) > 0.5f || 
			   Vector2.Dot(cp.normal, Vector2.up) < -0.5f)
			{
				grounded = true;
				reachedMax = false;
				jumped = false;
			}
		}

		if(c.gameObject.transform.parent.name == "Item") {
			Inventory.addItem(c.gameObject);
		}

//		ContactPoint2D contact = c.contacts[0];
//		if(Vector2.Dot(contact.normal, Vector2.up) > 0.5 ||
//		   Vector2.Dot(c.contacts[c.contacts.Length-1].normal, Vector2.up) > 0.5 ||
//		   Vector2.Dot(contact.normal, Vector2.up) < -0.5 ||
//		   Vector2.Dot(c.contacts[c.contacts.Length-1].normal, Vector2.up) < -0.5)
//		{
//			grounded = true;
//			reachedMax = false;
//			jumped = false;
//		}
	}

	void OnCollisionStay2d(Collision2D c) {
		grounded = true;
	}

	void OnCollisionExit2D(Collision2D c) {
		grounded = false;
	}

	/*
	 * Flip sprite from either left->right or right->left
	 */
	void Flip() {

		Vector3 v = gameObject.transform.localScale;
		v.x *= -1;
		gameObject.transform.localScale = v;
		facingRight =! facingRight;
	}

	/*
	 * Calculates the vector to set player velocity to
	 */
	Vector2 Move() {

		Vector2 vector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if(a.GetBool("moving")) { // responsiveness; if user isn't moving, stop
			vector = transform.TransformDirection (vector);
			vector *= speed;
		}
		if (!grounded && !reachedMax && jumped) {
			if(accel < 30f) {//transform.position.y-originalY>=maxHeight) {
				reachedMax=true;
			}
			else {
				vector.y += accel * speed * Time.deltaTime;
				accel -= 3f;
			}
		}
		else {
			vector.y -= 10 * 9.8f * Time.deltaTime;
		}
		return vector;
	}

	public Vector2 getPosition() {
		return gameObject.transform.position;
	}
}