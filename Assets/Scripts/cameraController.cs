using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
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
/*
	 * var player : Transform;
var trackingSpeed = 2.0;
var zoomSpeed = 5.0;
 
function Start () {
 
if (player == null)
    player = GameObject.Find("Player").transform;
}
 
function Update () {
    var pos = player.position;
    pos.y = transform.position.y;
 
    transform.position = Vector3.Lerp(transform.position, pos, trackingSpeed * Time.deltaTime);
    camera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
}
	 */