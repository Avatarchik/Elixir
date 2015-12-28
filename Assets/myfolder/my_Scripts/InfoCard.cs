using UnityEngine;
using System.Collections;

public class InfoCard : MonoBehaviour {
	
	private baseCard card;
	
	public baseCard Card
	{
		get{ return card; }
		set{card = value;}
	}
}
