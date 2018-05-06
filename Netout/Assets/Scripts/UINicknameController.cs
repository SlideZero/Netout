using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UINicknameController : MonoBehaviour {

	[SerializeField] private GameObject _nWManager;
	//public GameObject _introCamera;
	public GameObject _camera;
	public GameObject _nicknameLayer;
	public Text _nickname;
	public string _nicknameString;
	public string menuSceneName;
	public GameObject _loadingScreen;
	public Slider _slider;
	public Text _progressText;
	private bool inGame = false;

	// Use this for initialization
	void Start () {
		_nWManager = GameObject.Find("NetworkManager");
	}
	
	// Update is called once per frame
	void Update () {
		if(NetworkServer.active || NetworkClient.active)
		{
			_camera.GetComponent<IntroductionCamera>().enabled = false;
			_camera.GetComponent<CameraFollow360>().enabled = true;
			inGame = true;
		}

		if((!NetworkServer.active && !NetworkClient.active) && inGame)
			StartCoroutine(LoadAsyncronously(menuSceneName));
	}

	public void SetNickname()
	{
		_nWManager.GetComponent<NetworkManagerHUD>().enabled = true;
		_nicknameString = _nickname.text;
		_nicknameLayer.SetActive(false);
	}

	IEnumerator LoadAsyncronously(string _nextScene)
	{
		_nWManager.GetComponent<NetworkManagerHUD>().enabled = false;
		AsyncOperation operation = SceneManager.LoadSceneAsync(_nextScene);
		_loadingScreen.SetActive(true);
		while(!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			_slider.value = progress;
			_progressText.text = progress * 100f + "%";
			yield return null;
		}
	}
}
