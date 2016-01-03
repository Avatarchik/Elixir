using UnityEngine;
using System.Collections;

public class AllyManager : MonoBehaviour {

	public GameObject PlayerPrefab;
	Vector3 PlayerPosition = new Vector3 (-4.44f, 0.83f, 0f);
	void Start () {
		GeneratePlayer ();
	}
	

	void GeneratePlayer () {
		GameObject Player = Instantiate (PlayerPrefab);
		Player.transform.position = PlayerPosition;
	}
}
