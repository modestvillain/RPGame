using UnityEngine;
using System.Collections;

public class Flame : MonoBehaviour {

	void OnEnable()
	{
		StartCoroutine(burn());
	}

	IEnumerator burn()
	{
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}
}
