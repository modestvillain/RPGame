using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {

	Transform player;
	GameObject target;
	float z;
	float trackingSpeed = 2.0f;
	float zoomSpeed = 5.0f;

	void Start () {
		
		if (player == null)
			player = GameObject.Find("Player").transform;
	}

	void Update()
	{
		//Vector3 pos = player.position;
		//pos.y = transform.position.y;
		
		//transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
		if((camera.orthographicSize+Input.GetAxis("Mouse ScrollWheel") * zoomSpeed)<2 || (camera.orthographicSize+Input.GetAxis("Mouse ScrollWheel") * zoomSpeed)> 4) return;
		else camera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
	}
}
