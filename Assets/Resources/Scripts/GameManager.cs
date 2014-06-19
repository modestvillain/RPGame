using UnityEngine;
using System.Collections;

/*
 * Self-explanatory: reponsible for managing the game.
 */
public class GameManager : MonoBehaviour {

	public static Camera main;
	public static GameObject _player,
							 _inventory,
							 _spellbook,
							 _black;
	public static Inventory inventory;
	public static Player	player;
	public static Spellbook spellbook;
	Vector2 respawnPoint;

	/*
	 * CONVENTION: UNDERSCORE (_) means GameObject (GO),
	 * lack of underscore means script.
	 */
	void Start()
	{
		main = GameObject.Find("MainCamera").camera;
		_black = (GameObject)Instantiate(Resources.Load("Prefabs/Black"));
		_black.SetActive(false);
		_player = GameObject.Find("Player");
		player = (Player)_player.GetComponent("Player");
		_inventory = (GameObject)Instantiate(Resources.Load("Prefabs/Inventory"));
		inventory = (Inventory)_inventory.GetComponent("Inventory");
		inventory.setup();
		_spellbook = (GameObject)Instantiate(Resources.Load("Prefabs/Spellbook"));
		spellbook = (Spellbook)_spellbook.GetComponent("Spellbook");
		spellbook.setup();
		player.inv = inventory;
		player.spb = spellbook;
		respawnPoint = player.transform.position;
	}

	void Update()
	{
		if(player.HP <= 0)
		{
			_player.transform.position = respawnPoint;
			player.respawn();
		}
	}

	void OnGUI()
	{
		GUI.color = Color.red;
		GUILayout.Label("HP: " + player.HP);
		GUI.color = Color.blue;
		GUILayout.Label("MP: " + player.MP);
	}

	/*
	 * Basically a database/dictionary function that returns
	 * the mana cost of a given spell.
	 */
	public static int spellCost(string spell)
	{
		if(spell == "CastFirebolt" || spell == "CastIcebolt" || spell == "CastLightningbolt")
			return 50;
		return 0;
	}
}
