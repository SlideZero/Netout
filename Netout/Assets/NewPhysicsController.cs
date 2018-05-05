using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPhysicsController : MonoBehaviour {

	private Rigidbody rb;

	//Axis
	private float h;//leftstick horizontal
	private float v;//leftstsick vertical
	private float hr;// rightstick horizontal

	//Properties
	public float velocity;
	public float maxVelocity;
	public float acceleration;
	private bool isGrounded = false;



	private Vector3 movement;
	private Vector3 lastMovement;
	private Vector3 rotation;
	public float rotationSpeed = 50;
	private Quaternion nextQuaternion;


	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		h = Input.GetAxisRaw("Horizontal");
		v = Input.GetAxisRaw("Vertical");
		hr = Input.GetAxisRaw("HorizontalR");


		Acceleration();
		Movement();
		SurfaceRotation();



		if(hr != 0){

			Rotation(hr);

		}

		IsGrounded();
	}

	void Movement()
	{
		movement.Set(h,0,v);
		
		movement = movement.normalized * velocity;
		if(h != 0 || v != 0)
		{
			transform.position += transform.forward * movement.z * Time.deltaTime;
			transform.position += transform.right * movement.x * Time.deltaTime;
			lastMovement = movement.normalized;
		}else
		{
			lastMovement = lastMovement.normalized * velocity;
			transform.position += transform.forward * lastMovement.z * Time.deltaTime;
			transform.position += transform.right * lastMovement.x * Time.deltaTime;
		}
	}

	void Acceleration()
	{

		if(h != 0 || v != 0)
		{
			if(velocity <= maxVelocity)
				velocity += acceleration * Time.deltaTime;
		}else
		{
			if(velocity >= 0)
				velocity -= acceleration * 5.0f * Time.deltaTime;
		}
		
	}

	void Rotation(float hr){

		rotation.Set(0,hr,0);
		rotation = rotation.normalized;

		transform.Rotate(Vector3.up * rotation.y * rotationSpeed * Time.deltaTime);
	}

	void SurfaceRotation(){

		RaycastHit hit;
		if(Physics.Raycast(transform.position, -transform.up, out hit, 1.5f, 1 << 8)){
			
			nextQuaternion = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
			transform.position = hit.point + hit.normal;

		}else if(Physics.Raycast(transform.position, -Vector3.up, out hit, 50, 1 << 8)){

			
			nextQuaternion = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
			nextQuaternion.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		}else{

			nextQuaternion = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
			nextQuaternion.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		}

		transform.rotation = Quaternion.Slerp(transform.rotation, nextQuaternion, Time.deltaTime * 10);

	}

	void IsGrounded(){

		RaycastHit hit;
		if(Physics.SphereCast(transform.position, 0.2f, -transform.up, out hit, 1f, 1 << 8)){

			isGrounded = true;
		}else{
			isGrounded = false;
		}
	}
}
