using UnityEngine;
using System.Collections;

public class PlayerHPBar : MonoBehaviour {
    public RectTransform healthTransform;
    private float minXValue;
    private float maxXValue;
    private float currentHealth;
    private float maxHealth;
    //private float currentXValue;
    // Use this for initialization
    void Start()
    {

        maxHealth = GameObject.Find("GameManager").GetComponent<PlayerPrefs>().player.MAX_HP;

        maxXValue = healthTransform.localPosition.x;
        minXValue = healthTransform.localPosition.x - healthTransform.rect.width;
        currentHealth = maxHealth;
        HandleHealth();
        //healthTransform.localPosition = new Vector2(minXValue, healthTransform.localPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<PlayerPrefs>().player.HP != currentHealth)
        {
            currentHealth = this.GetComponent<BaseCharacter>().HP;
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
