using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float trackingSpeed = 2.0f;
	public float zoomSpeed = 5.0f;
	private GameObject player;

	void Start()
	{
		player = GameObject.Find ("Player");
	}

	void FixedUpdate () {
		Vector3 pos = player.transform.position;
		pos.z = transform.position.z;
		transform.position = Vector3.Lerp(transform.position, pos, trackingSpeed * Time.deltaTime);
		float val = gameObject.camera.orthographicSize - Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;
		if(val > .5 && val < 2) {
			gameObject.camera.orthographicSize = val;
		}

	}
}