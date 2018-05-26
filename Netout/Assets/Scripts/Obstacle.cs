using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour {

	public int type = 0;
	public Material m_type0;
	public Material m_type1;
	public Material m_type2;
	private Renderer render;
	public Text text;
	// Use this for initialization
	void Start () {
		render = GetComponent<Renderer>();

		switch(type)
		{
			case 0:
				render.material = m_type0;
				text.text = "Q";
				break;
			case 1:
				render.material = m_type1;
				text.text = "W";
				break;
			case 2:
				render.material = m_type2;
				text.text = "E";
				break;
		}
	}
}
