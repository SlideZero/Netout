using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject _loadingScreen;
	public Slider _slider;
	public Text _progressText;

	public void LoadLevel(string _nextScene)
	{
		StartCoroutine(LoadAsyncronously(_nextScene));
	}

	IEnumerator LoadAsyncronously(string _nextScene)
	{
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
