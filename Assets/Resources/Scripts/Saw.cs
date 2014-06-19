using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour {

	Animator a;
	bool kill = true;

	void Start()
	{
		a = GetComponent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			if(kill)
			{
				c.gameObject.GetComponent<Player>().HP = 0;
			}
		}
	}
	
	public void stop()
	{
		a.SetBool("stopped", true);
		kill = false;
	}

	public void start()
	{
		a.SetBool("stopped", false);
		kill = true;
	}
}
