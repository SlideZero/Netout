using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

	private Rigidbody rb;

	//Axis
	private float h;//leftstick horizontal
	private float v;//leftstsick vertical
	private float hr;// rightstick horizontal


	//movement variables
	public float speed = 500;
	private Vector3 movement;
	private float yMonentum = 2f;
	private bool isGrounded = false;
	private float surfaceAtraction = 20;

	//variables de bajada
	private bool bajada = false;

	//rotation variables
	public float rotationSpeed = 50;
	private Vector3 rotation;

	//SufaceRotattion variables
	private Quaternion nextQuaternion;


	// Use this for initialization
	void Start () {
		
		rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		h = Input.GetAxisRaw("Horizontal");
		v = Input.GetAxisRaw("Vertical");
		hr = Input.GetAxisRaw("Horizontal");

		SurfaceRotation();

		if(hr != 0){

			Rotation(hr);

		}

		IsGrounded();

		//Sube la velocidad cuando esta de bajada y vulve a la normalidad poco a poco cuando ya no esta
		if(rb.velocity.y < -1f && isGrounded){
			bajada = true;
		}else{

			bajada = false;
		}

		if(bajada && -rb.velocity.y > yMonentum){

			yMonentum = 1 + (-rb.velocity.y / 15);
		}

		if(!bajada && yMonentum > 1){

			yMonentum -= Time.deltaTime / 2;
		}

		if(yMonentum < 1 || !isGrounded){

			yMonentum = 1;
		}

		//Debug.Log(yMonentum);
		//rb.AddForce(-Vector3.up * 9.81f, ForceMode.Acceleration);
		Debug.Log(transform.InverseTransformDirection(rb.velocity).magnitude);
		Debug.DrawRay(transform.position, (transform.forward * movement.z + transform.right * movement.x) / speed * 5, Color.white);
		
	}
	

	void LateUpdate(){

		
	}

	void FixedUpdate(){

		surfaceAtraction = 10 + transform.InverseTransformDirection(rb.velocity).magnitude;

		//rb.velocity = Vector3.ClampMagnitude(rb.velocity, 30);

		if(!isGrounded){

			rb.AddForce(-Vector3.up * 9.81f, ForceMode.Acceleration);
		}

		if(rb.velocity.y > 2 ){

			rb.AddForce(-Vector3.up * 6f, ForceMode.Acceleration);
		}

		if(h != 0 || v != 0){
			Movement(h,v);
		}

		if(isGrounded){

			rb.AddForce(-transform.up * surfaceAtraction, ForceMode.Acceleration);
		}
		
		
	}


	void Movement(float h, float v){

		movement.Set(h,0,v);
		
		movement = movement.normalized * speed *yMonentum;

		//rb.velocity =  transform.rotation * new Vector3(movement.x, 0, movement.z);
		
		rb.AddForce(transform.forward * movement.z, ForceMode.Force);
		rb.AddForce(transform.right * movement.x, ForceMode.Force);
			
	}

	void Rotation(float hr){

		rotation.Set(0,hr,0);
		rotation = rotation.normalized;

		transform.Rotate(Vector3.up * rotation.y * rotationSpeed * Time.deltaTime);
	}

	void SurfaceRotation(){

		RaycastHit hit;
		if(Physics.Raycast(transform.position, -transform.up, out hit, 2f, 1 << 8) ){
			nextQuaternion.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			nextQuaternion = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

			//transform.position = hit.point + hit.normal;
		}else{

			nextQuaternion.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			nextQuaternion = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
			
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
