using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Example : MonoBehaviour {

	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


			RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);

			if(hit.collider != null)	
			{
				
				Debug.Log (hit.collider.gameObject);
				
			}else{
				Debug.Log("nothing");
			}
			
		}
	
	}
	

}
