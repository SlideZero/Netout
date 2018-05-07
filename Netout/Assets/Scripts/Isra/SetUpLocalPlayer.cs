using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetUpLocalPlayer : NetworkBehaviour {

	public Text namePrefab;
	public Text nameLabel;
	public Text LapPrefab;
	public Text lapLabel;
	public Transform namePos;
	GameObject _gameControler;

	private string textBoxName = "";

	[SyncVar (hook = "OnChangeName")]
	public string pName = "player";
	

	void OnChangeName(string n)	//regresa al cliente
	{
		pName = n;
		nameLabel.text = pName;
		//nameLabel.transform.GetChild(1).GetComponent<Text>().text = pName;
	}

	[SyncVar (hook = "OnChangeLap")]
	public int pLap = 0;

	void OnChangeLap(int n)	//regresa al cliente
	{
		pLap = n;
		lapLabel.text = "" + pLap + "/1";
		//nameLabel.transform.GetChild(1).GetComponent<Text>().text = pName;
	}

	[Command]	//peticion al servidor
	public void CmdChangeName(string newName)
	{
		pName = newName;
		nameLabel.text = pName;
		//nameLabel.transform.GetChild(1).GetComponent<Text>.text = pName;
	}

	[Command]	//peticion al servidor
	public void CmdChangeLap(int lapNum)
	{
		pLap = lapNum;
		lapLabel.text = "" + pLap + "/1";
		//nameLabel.transform.GetChild(1).GetComponent<Text>.text = pName;
	}

	void OnGUI()	//Este es para dibujar un TextField en pantalla
	{
		if(isLocalPlayer)
		{
			/*textBoxName = GUI.TextField (new Rect(25,15,100,25), textBoxName);
			if(GUI.Button(new Rect(130,15,35,25), "Set"))*/
				CmdChangeName(_gameControler.GetComponent<UINicknameController>()._nicknameString);
				CmdChangeLap(pLap);
		}
	}

	// Use this for initialization
	void Start () {

		_gameControler = GameObject.FindWithTag("GameController");

		if(isLocalPlayer)
		{
			GetComponent<MovementController>().enabled = true;
			CameraFollow360.player = this.gameObject.transform;
		}
		else
		{
			GetComponent<MovementController>().enabled = false;
		}

		GameObject canvas = GameObject.FindWithTag("MainCanvas");
		nameLabel = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as Text;
		nameLabel.transform.SetParent(canvas.transform);
		lapLabel = Instantiate(LapPrefab, Vector3.zero, Quaternion.identity) as Text;
		lapLabel.transform.SetParent(canvas.transform);
		lapLabel.rectTransform.anchoredPosition = new Vector3(0,0,0);

	}

	void Update()
	{
		Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(namePos.position);
		nameLabel.transform.position = nameLabelPos;
	}

	public void AddLap(int lapNum)
	{
		if(isLocalPlayer)
			CmdChangeLap(lapNum);
	}
}
