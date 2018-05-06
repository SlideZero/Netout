using UnityEngine;

public class IntroductionCamera : MonoBehaviour {

	[SerializeField] private GameObject _pista;
	float _yPos;

	void Start()
	{
		_yPos = _pista.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
		 transform.RotateAround(Vector3.zero, Vector3.up, 5 * Time.deltaTime);

	}
}
