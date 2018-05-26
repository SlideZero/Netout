using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour {

	private AudioSource over;

	void Start()
	{

		over = GetComponent<AudioSource>();
	}
	// Use this for initialization
	void OnMouseOver()
	{
		over.Play();
	}
}
