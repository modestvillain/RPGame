using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static Camera main;
	public static GameObject _player,
							 _inventory;
	public static Inventory inventory;

	void Start() {

		main = GameObject.Find ("MainCamera").camera;
		_player = GameObject.Find ("Player");
		_inventory = (GameObject)Instantiate(Resources.Load("Prefabs/Inventory"));
		inventory = (Inventory)_inventory.GetComponent("Inventory");
		inventory.setup();
	}
}
