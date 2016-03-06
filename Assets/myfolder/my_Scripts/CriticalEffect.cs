using UnityEngine;
using System.Collections;

public class CriticalEffect : MonoBehaviour {
	float cooltime;
	// Use this for initialization
	void Start () {
		Debug.Log("Cooldown Start");
		cooltime = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		cooltime -= Time.deltaTime;
		if(cooltime < 0)
		{
			this.gameObject.SetActive(false);
			cooltime = 1.0f;
		}
	}
}
