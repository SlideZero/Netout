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
	[SerializeField] private GameObject _playerPrefs;

	private string textBoxName = "";

	[SyncVar (hook = "OnChangeName")]
	public string pName = "player";
	
	[SyncVar (hook = "OnChangeColor")]
	public string pColor = "#ffffff";
	
	public override void OnStartClient()
	{
		base.OnStartClient();
		Invoke("UpdateStates", 0.5f);
	}

	void UpdateStates()
	{
		OnChangeName(pName);
		OnChangeColor(pColor);
	}

	void OnChangeName(string n)	//regresa al cliente
	{
		pName = n;
		nameLabel.text = pName;
		//nameLabel.transform.GetChild(1).GetComponent<Text>().text = pName;
	}

	void OnChangeColor(string n)	//regresa al cliente el color
	{
		pColor = n;
		Renderer[] rends = GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rends)
		{
			r.material.SetColor("_Color", ColorFromHex(pColor));
		}
	}

	[Command]	//peticion al servidor para el color
	public void CmdChangeColor(string newColor)	//string newColor
	{
		pColor = newColor;
		Renderer[] rends = GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rends)
		{
			r.material.SetColor("_Color", ColorFromHex(pColor));
		}
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
				CmdChangeColor(_playerPrefs.GetComponent<PlayerPrefsController>().colorSelected);
		}
	}

	private Color ColorFromHex(string hex)
	{
		hex = hex.Replace("0x","");
		hex = hex.Replace("#","");

		byte a = 255;
		byte r = byte.Parse(hex.Substring(0,2),System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2),System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2),System.Globalization.NumberStyles.HexNumber);

		if(hex.Length == 8)
		{
			a = byte.Parse(hex.Substring(6,2),System.Globalization.NumberStyles.HexNumber);
		}

		return new Color32(r,g,b,a);
	}

	// Use this for initialization
	void Start () {

		_gameControler = GameObject.FindWithTag("GameController");

		if(isLocalPlayer)
		{
			_playerPrefs = GameObject.Find("PlayerPrefsController");
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
