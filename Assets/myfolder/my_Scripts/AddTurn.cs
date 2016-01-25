using UnityEngine;
using System.Collections;

public class AddTurn : MonoBehaviour {
    float cooltime;
	// Use this for initialization
	void Start () {
        Debug.Log("Cooldown Start");
        cooltime = 2.0f;
    }

	// Update is called once per frame
	void Update () {
	    cooltime -= Time.deltaTime;
        if(cooltime < 0)
        {
            this.gameObject.SetActive(false);
            cooltime = 2.0f;
        }
	}
}
