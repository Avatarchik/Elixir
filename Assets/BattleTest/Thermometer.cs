using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Thermometer : MonoBehaviour {

    int currentTemperature;
    Text text;

    public void SetTemperature(int newTemperature)
    {
        currentTemperature = newTemperature;
    }
    
    public void Heating(int deltaTemperature)
    {
        currentTemperature += deltaTemperature;
    }
    
    public void Cooling(int deltaTemperature)
    {
        currentTemperature -= deltaTemperature;
    }
    
    public int GetTemperature()
    {
        return currentTemperature;
    }

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
