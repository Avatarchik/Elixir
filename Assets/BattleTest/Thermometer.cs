using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Thermometer : MonoBehaviour {

    int currentTemperature;
    Text text;

	// Use this for initialization
	void Start () {
        text = GameObject.Find("ThermometerText").GetComponent<Text>();
        currentTemperature = 20; // default temperature.
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "현재 온도 : " + currentTemperature + " 도";
	}
}
