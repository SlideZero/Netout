using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UINicknameController : MonoBehaviour {

	[SerializeField] private GameObject _nWManager;
	//public GameObject _introCamera;
	public GameObject _camera;
	public GameObject _nicknameLayer;
	public Text _nickname;
	public string _nicknameString;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(NetworkServer.active || NetworkClient.active)
		{
			_camera.GetComponent<IntroductionCamera>().enabled = false;
			_camera.GetComponent<CameraFollow360>().enabled = true;
		}
	}

	public void SetNickname()
	{
		_nWManager.GetComponent<NetworkManagerHUD>().enabled = true;
		_nicknameString = _nickname.text;
		_nicknameLayer.SetActive(false);
	}
}
