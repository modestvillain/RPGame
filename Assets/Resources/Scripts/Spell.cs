using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

	public int sign;
	public int cost;
	float 	dist,
			maxDist = 3f;

	void OnEnable() {
		cost = 50;
	}

	void Update() {
		float newX = sign*Time.deltaTime*4f;
		dist += newX;
		transform.position = new Vector2(transform.position.x + newX, transform.position.y);

		if(Mathf.Abs(dist) > maxDist) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D c) {
		Destroy(gameObject);
		//Debug.Log("hi");
	}
}
