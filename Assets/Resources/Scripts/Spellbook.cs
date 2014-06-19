using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spellbook : MonoBehaviour {

	static GameObject 	_player,
						_black;
	static Player	player;
//	static int	rowMax = 4;
//	static float	xoff = -.5f,
//				 	yoff = 1f;
//	static List<Spell>	spells = new List<Spell>();
	public static bool	visible = false;
	Texture2D	castFirebolt,
				castIcebolt,
				castLightningbolt,
				castClones,
				castPhase;
	
	/*
	 * Allows the screen to initialize some variables
	 * and set itself to inactive.
	 */
	public void setup()
	{
		_player = GameManager._player;
		player = GameManager.player;
		_black = GameManager._black;
		gameObject.SetActive (false);
		castFirebolt = Resources.Load<Texture2D>("Textures/CastFirebolt");
		castIcebolt = Resources.Load<Texture2D>("Textures/CastIcebolt");
		castLightningbolt = Resources.Load<Texture2D>("Textures/CastLightningbolt");
		castClones = Resources.Load<Texture2D>("Textures/CastClones");
		castPhase = Resources.Load<Texture2D>("Textures/CastPhase");
	}
	
	/*
	 * Adds the given item to the player's inventory.
	 */
	public void addSpell(Spell s)
	{
//		spells.Add(s);
	}
	
	/*
	 * Turn the inventory screen on/off.
	 */
	public void toggleDisplay()
	{
		visible = !visible;
		gameObject.transform.position = _player.transform.position;
		_black.transform.position = transform.position;
		gameObject.SetActive(visible);
		_black.SetActive(visible);

		if(visible)
			Time.timeScale = 0f;
		else
			Time.timeScale = 1f;

//		int xmod = 0;
//		int ymod = 0;
//		
//		for(int i=0; i < spells.Count; i++)
//		{
//			xmod = i;
//			if(xmod > rowMax)
//			{
//				xmod = 0;
//				ymod++;
//			}
//			Vector2 position = spellbook.transform.position;
//			position.x += xoff + .5f*xmod;
//			position.y += yoff - .5f*ymod;
//			spells[i].gameObject.transform.position = position;
//			spells[i].gameObject.SetActive(visible);
//		}
	}

	void OnGUI()
	{
		if(visible)
		{
			Rect r = new Rect(240, 30, 200, 250);
			GUILayout.Window(0, r, showSpells, "SPELLBOOK");
//			GUI.DrawTexture(r, castFirebolt);
		}
	}

	void showSpells(int windowID)
	{
		if(GUILayout.Button(castFirebolt))
		{
			player.currentSpell = "CastFirebolt";
		}
		else if(GUILayout.Button(castIcebolt))
		{
			player.currentSpell = "CastIcebolt";
		}
		else if(GUILayout.Button(castLightningbolt))
		{
			player.currentSpell = "CastLightningbolt";
		}
		else if(GUILayout.Button(castClones))
		{
			player.currentSpell = "CastClones";
		}
		else if(GUILayout.Button(castPhase))
		{
			player.currentSpell = "CastPhase";
		}
	}
}
