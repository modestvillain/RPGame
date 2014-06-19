using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float trackingSpeed = 2f;
	public float zoomSpeed = 5f;
	private float 	farZoom = 3.5f,
					normalZoom = 2f;
	private GameObject player;

	void Start()
	{
		player = GameObject.Find ("Player");
		gameObject.camera.orthographicSize = normalZoom;
	}

	void Update ()
	{
		/* FOLLOW PLAYER MOVEMENT */
		Vector3 pos = player.transform.position;
		pos.z = transform.position.z;
		transform.position = pos;//Vector3.Lerp(transform.position, pos, trackingSpeed * Time.deltaTime);

		if(Input.GetKeyDown("p"))
		{														/* TOGGLE CAMERA ZOOM */
			if(gameObject.camera.orthographicSize == farZoom)
				gameObject.camera.orthographicSize = normalZoom;
			else
				gameObject.camera.orthographicSize = farZoom;
		}
	}
}