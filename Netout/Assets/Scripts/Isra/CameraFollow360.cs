/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow360 : MonoBehaviour {

	static public Transform player;
	public float distance = 10;
	public float height = 5;
	public Vector3 lookOffset = new Vector3(0,1,0);
	public float cameraSpeed = 10;
	public float rotSpeed = 10;

	void LateUpdate()
	{
		if(player)
		{
			Vector3 lookPosition = player.position + lookOffset;
			Vector3 relativePos = lookPosition - transform.position;
			Quaternion rot = Quaternion.LookRotation(relativePos);

			transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * rotSpeed * 0.1f);

			Vector3 targetPos = player.transform.position + player.transform.up * height - player.transform.forward * distance;

			this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * cameraSpeed * 0.1f);
		}
	}
}*/

using UnityEngine;
using System.Collections;

public class CameraFollow360 : MonoBehaviour {
	static public Transform player;
	public float distance = 3.0f;
	public float height = 3.0f;
	public float damping = 5.0f;
	public bool smoothRotation = true;
	public float rotationDamping = 10.0f;
	
	void LateUpdate () {
		if(player)
		{
			Vector3 wantedPosition = player.TransformPoint(0, height, -distance);
			transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);

			if (smoothRotation) {
				Quaternion wantedRotation = Quaternion.LookRotation(player.position - transform.position, player.up);
				transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
			}
			
			else transform.LookAt (player, player.up);
		}
	}
}