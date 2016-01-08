using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour {
    public RectTransform healthTransform;
    private float minXValue;
    private float maxXValue;
    private int currentHealth;
    private int maxHealth;
    //private float currentXValue;
	// Use this for initialization
	void Start () {
        Debug.Log(this.gameObject);
        maxHealth = this.GetComponent<Monster>().maxHp;
        maxXValue = healthTransform.localPosition.x;
        minXValue = healthTransform.localPosition.x - healthTransform.rect.width;
        currentHealth = maxHealth;
        //healthTransform.localPosition = new Vector2(minXValue, healthTransform.localPosition.y);
    }
	
	// Update is called once per frame
	void Update () {
        if(this.GetComponent<Monster>().hp != currentHealth)
        {
            currentHealth = this.GetComponent<Monster>().hp;
            HandleHealth();
        }
    }
    private void HandleHealth()
    {
        float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);
        healthTransform.localPosition = new Vector2(currentXValue, healthTransform.localPosition.y);
    }
    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
