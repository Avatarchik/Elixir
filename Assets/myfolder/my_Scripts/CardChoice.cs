using UnityEngine;
using System.Collections;

public class CardChoice : MonoBehaviour {
	public IEnumerator cardChoice(){
		if(Input.GetMouseButtonDown(0)){
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (pos,Vector2.zero,0f);
			if(hit.collider!=null)
			{

			}

	}


}
