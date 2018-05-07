﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

	private Rigidbody rb;

	//Axis
	private float hr;// rightstick horizontal


	//movement variables
	public float speed = 500;
	public float breikingForce = 2;
	private bool isGrounded = false;
	private float surfaceAtraction = 20;


	//Inputs variables
	private bool isLClickPressed = false;
	private bool isRClickPressed = false;

	//rotation variables
	public float rotationSpeed = 50;
	public float extraRotationSpeed = 100;
	private Vector3 rotation;

	//SufaceRotattion variables
	private Quaternion nextQuaternion;
	private RaycastHit hit;


	// Use this for initialization
	void Start () {
		
		rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		hr = Input.GetAxisRaw("Horizontal");

		SurfaceRotation();

		Inputs();

		IsGrounded();

		//Debug.Log(transform.InverseTransformDirection(rb.velocity).magnitude);
		//Debug.DrawRay(transform.position, (transform.forward * movement.z + transform.right * movement.x) / speed * 5, Color.white);
		
	}
	
	void Inputs()
	{
		if(Input.GetMouseButtonDown(0))
			isLClickPressed = true;
		else if (Input.GetMouseButtonUp(0))
			isLClickPressed = false;

		if(Input.GetMouseButtonDown(1))
			isRClickPressed = true;
		else if (Input.GetMouseButtonUp(1))
			isRClickPressed = false;

	}

	void FixedUpdate(){

		surfaceAtraction = 10 + transform.InverseTransformDirection(rb.velocity).magnitude;

		if(!isGrounded){

			rb.AddForce(-Vector3.up * 9.81f, ForceMode.Acceleration);
		}

		if(rb.velocity.y > 2 ){

			rb.AddForce(-Vector3.up * 6f, ForceMode.Acceleration);
		}

		Acceleration();
		Breacking();

		if(hr != 0){

			Rotation(hr);

		}

		if(isGrounded){

			rb.AddForce(-transform.up * surfaceAtraction, ForceMode.Acceleration);
		}
		
		
	}

	void Acceleration()
	{
		if(isLClickPressed)
			rb.AddForce(transform.forward * speed, ForceMode.Force);
	}

	void Breacking()
	{
		if(isRClickPressed)
			rb.drag = breikingForce;
		else
			rb.drag = 0;
	}

	void Rotation(float hr){

		rotation.Set(0,hr,0);
		rotation = rotation.normalized;

		float nextRotationSpeed = 0;

		if(isRClickPressed)
			nextRotationSpeed = extraRotationSpeed;
		else
			nextRotationSpeed = rotationSpeed;

		//transform.Rotate(Vector3.up * rotation.y * rotationSpeed * Time.deltaTime);
		Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotation.y * nextRotationSpeed * Time.deltaTime);
		rb.MoveRotation(rb.rotation * deltaRotation);
	}

	void SurfaceRotation(){

		
		if(Physics.Raycast(transform.position, -transform.up, out hit, 2f, 1 << 8) ){
			nextQuaternion.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			nextQuaternion = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

			//transform.position = hit.point + hit.normal;
		}else{

			nextQuaternion.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			nextQuaternion = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
			
		}

		transform.rotation = Quaternion.Slerp(transform.rotation, nextQuaternion, Time.deltaTime * 10);
		//nextQuaternion = Quaternion.Euler(nextQuaternion.eulerAngles * Time.deltaTime);
		//rb.MoveRotation(rb.rotation * nextQuaternion);

	}

	void IsGrounded(){

		RaycastHit hitSphere;
		if(Physics.SphereCast(transform.position, 0.2f, -transform.up, out hitSphere, 1f, 1 << 8)){
			isGrounded = true;
		}else{
			isGrounded = false;
		}
	}
}
