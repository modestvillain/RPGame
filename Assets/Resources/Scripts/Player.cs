using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	Animator a;
	Collider2D box;
	SpriteRenderer sr;
	float 	speed = 2f,
		  	maxHeight = .7f,
		  	oldY = 0f,
		  	accel = 0f,
			strafeSpeed = 1f,
			normalSpeed = 2f,
			regenTime = 0f;
	bool 	facingRight = true,
		 	grounded = true,
			reachedMax = false;
	int		HP = 100,
			MP = 100,
			maxHP = 100,
			maxMP = 100;

	void Start () {

		a = GetComponent<Animator> ();
		box = GetComponent<BoxCollider2D>();
		sr = GetComponent<SpriteRenderer>();
	}
	
	void FixedUpdate () {

		if(MP < maxMP)
			regenTime += Time.deltaTime;
		if(regenTime > 1f) {
			MPRegen ();
			regenTime = 0f;
		}
		RaycastHit2D hit = Physics2D.Raycast (transform.position, -Vector2.up, .4f, 1<<8);

		/* GROUNDED */
		if(hit.collider != null) {
			if(hit.collider.tag == "Ground") {
				grounded = true;
				reachedMax = false;
			}
		}
		else {
			grounded = false;
		}

		/* INVENTORY */
		if(Input.GetKeyDown("i") || Inventory.visible) {
			if((Input.GetKeyDown("i") && grounded)) {
				GameManager.inventory.toggleDisplay();
			}
			return;
		}

		/* ACTION BUTTONS */
		if(Input.GetKey("a")) {											/* STRAFE */
			speed = strafeSpeed;
		}
		else {
			speed = normalSpeed;
		}

		if(Input.GetKeyDown("e")) {										/* SWORD ATTACK */
			a.SetBool("attacking",true);

		}
		else if(a.GetCurrentAnimatorStateInfo(0).IsName("Sword")) {
			a.SetBool("attacking",false);
		}

		if(Input.GetKeyDown("q")) {										/* SPELL CAST */

			if(MP > 0) {
				a.SetBool("attacking",true);
				GameObject spell = (GameObject)Instantiate (Resources.Load ("Prefabs/Firebolt"));

				if(facingRight) {
					spell.transform.position = new Vector2(transform.position.x+.5f,transform.position.y);
					spell.GetComponent<Spell>().sign = 1;
					Vector2 v = spell.transform.localScale;
					v.x *= -1;
					spell.transform.localScale = v;
				}
				else {
					spell.transform.position = new Vector2(transform.position.x-.5f,transform.position.y);
					spell.GetComponent<Spell>().sign = -1;
				}

				subtractMP(spell);
			}
		}
		else if(a.GetCurrentAnimatorStateInfo(0).IsName("Sword")) {
			a.SetBool("attacking",false);
		}

		if(Input.GetKeyDown("space") && grounded) {						/* JUMP */
			accel=75f;
			grounded=false;
			oldY=transform.position.y;
		}
		else if (Input.GetKey ("left") && !a.GetBool("attacking")) {	/* LEFT MOVEMENT */
			if(facingRight && !Input.GetKey ("a")) {
				Flip ();
			}
			a.SetBool("moving",true);
		}
		else if (Input.GetKey ("right") && !a.GetBool("attacking")) {	/* RIGHT MOVEMENT */
			if(!facingRight && !Input.GetKey ("a")) {
				Flip ();
			}
			a.SetBool("moving",true);
		}
		else {															/* NO MOVEMENT */
			a.SetBool("moving",false);
		}

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

		if(c.gameObject.tag == "Spikes") {
			HP -= 10;
			Debug.Log(HP);
			StartCoroutine(blink ());
		}
		else if(c.gameObject.transform.parent.tag == "Item") {
			GameManager.inventory.addItem(c.gameObject);
		}
	}

	void OnCollisionStay2d(Collision2D c) {

	}

	void OnCollisionExit2D(Collision2D c) {

	}

	IEnumerator blink() {

		for(int i=0; i<5; i++) {
			if(i%2==0)	sr.sortingLayerName = "Hidden";
			else        sr.sortingLayerName = "Player";
			yield return new WaitForSeconds(.1f);
		}
		sr.sortingLayerName = "Player";
	}

	/*
	 * Flip sprite from either left->right or right->left
	 */
	void Flip() {

		Vector2 v = gameObject.transform.localScale;
		v.x *= -1;
		gameObject.transform.localScale = v;
		facingRight =! facingRight;
	}

	/*
	 * Calculates the vector to set player velocity to
	 */
	Vector2 Move() {

		Vector2 vector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

		if(!a.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			vector = transform.TransformDirection (vector);
			vector *= speed;
		}

		if (!grounded && !reachedMax) {
			if(accel < 30f) {
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

	void subtractMP(GameObject spell) {
		MP -= spell.GetComponent<Spell>().cost;
	}

	void MPRegen() {
		MP += 20;
		if(MP > maxMP)
			MP = maxMP;
	}
}