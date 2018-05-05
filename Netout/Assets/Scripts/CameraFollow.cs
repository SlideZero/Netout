using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


	public Transform player;
	public Vector3 offset;

	private float vr = 20;// Axis right stick
	private const float V_MIN = 0;
	private const float V_MAX = 60;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		vr += Input.GetAxisRaw("VerticalR");

		vr = Mathf.Clamp(vr, V_MIN, V_MAX);

		transform.position = player.position;

		
		transform.rotation = player.rotation;

		transform.Rotate(Vector3.right * vr);

	}

	
}
