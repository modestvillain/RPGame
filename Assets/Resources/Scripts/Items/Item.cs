using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool pickedUp = false;
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && pickedUp)
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if(hit.collider != null && hit.collider.gameObject == gameObject)
			{
				pickedUp = false;
				useItem();
			}
		}
	}

	/*
	 * Generic method for consumption of an item.
	 */
	public virtual void useItem() {}

	public void destroy()
	{
		Destroy(gameObject);
	}
}