using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerPrefPreview : MonoBehaviour {

	private Color _buttonColor;

	public void ChangeLocalColor()
	{
		Renderer rend = GetComponent<Renderer>();
		rend.material.SetColor("_Color", SetPlayerColor());
	}

	private Color SetPlayerColor()
	{
		_buttonColor = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
		return _buttonColor;
	}

	void Update()
	{
		 transform.Rotate(Vector3.up * Time.deltaTime * 20, Space.World);
	}
}


/*Renderer[] rends = GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rends)
		{
			if(r.gameObject.name == "BODY")
			{
				r.material.SetColor("_Color", SetPlayerColor());
				break;
			}
		}*/