using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.childCount == 0)
        {
            //Debug.Log("Destroy panel");
            Destroy(gameObject);
        }
	}
}
