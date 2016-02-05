using UnityEngine;
using System.Collections;

public class AllyManager : MonoBehaviour {

	public GameObject PlayerPrefab;
	Vector3 PlayerPosition = new Vector3 (-4.36f, 1.35f, 0);
	void Start () {
		//GeneratePlayer ();
	}
	

	public void GeneratePlayer () {
		GameObject Player = Instantiate (PlayerPrefab);
		Player.transform.position = PlayerPosition;
	}
}
