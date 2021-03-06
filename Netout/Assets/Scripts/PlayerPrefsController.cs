﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsController : MonoBehaviour {
	
	private string _colorSelected;
	public AudioSource ButtonSound;
	
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public void ChangeColor(string _buttonColor)
	{
		ButtonSound.Stop();
		ButtonSound.Play();
		_colorSelected = _buttonColor;
	}

	public string colorSelected
	{
		get{return _colorSelected;}
	}

	public void loadScene(string _nextScene)
	{
		ButtonSound.Stop();
		ButtonSound.Play();
		SceneManager.LoadScene(_nextScene);
	}
}
