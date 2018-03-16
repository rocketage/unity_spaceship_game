using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float acceleration = 50f;
	public float maximumAngularSpeed = 50f;
	public float maximumSpeed = 10f;
	public float rotationFactor = 25f;
	public float fireRate = 0.001f;

	public GameObject shot;  
	public Transform shotSpawn;
	public GameObject explosion;
	public GameObject shipExplosion;

	private Rigidbody rb;
	private string horizontalAxis;
	private string verticalAxis;
	private string fireButton;
	private float timeWhenItCanFireAgain;
	private int health = 150;

	void Start() 
	{
		rb = GetComponent<Rigidbody> ();
		if (name == "Player1") {
			horizontalAxis = "Horizontal";
			verticalAxis = "Vertical";
			fireButton = "Fire1";
		} else {
			horizontalAxis = "HorizontalP2";
			verticalAxis = "VerticalP2";
			fireButton = "Fire2";
		}
	}

	void FixedUpdate () 
	{
		Move ();
		Rotate ();
		Clamp ();
	}

	void Update ()
	{
		if (Input.GetButton (fireButton) && Time.time > timeWhenItCanFireAgain) {
			timeWhenItCanFireAgain = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	} 

	void OnCollisionEnter(Collision collision)
	{
		HandleBoltImpact (collision);
		HandleHealth (collision);
	}

	private void HandleHealth(Collision collision)
	{
		health = health - 1;
		if (health < 1) {
			Destroy(gameObject);
			GameObject explosionAnimation = (GameObject)Instantiate(shipExplosion, transform.position, transform.rotation);
			Destroy(explosionAnimation.gameObject, 1.1f);
		}
	}

	private void HandleBoltImpact(Collision collision)
	{
		ContactPoint contact = collision.contacts[0];
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		Vector3 pos = contact.point;
		GameObject explosionAnimation = (GameObject)Instantiate(explosion, pos, rot);
		Destroy(explosionAnimation.gameObject, 2.0f);
	}

	private void Move() 
	{
		bool isAccelerating = Input.GetButton (verticalAxis);

		if (rb.velocity.magnitude > maximumSpeed) {
			rb.AddForce (transform.forward * -1 * (rb.velocity.magnitude - maximumSpeed));
		} else if (isAccelerating) {
			rb.AddForce (transform.forward * acceleration);
		}
	}

	private void Rotate() 
	{
		float rotationDirection = Input.GetAxis (horizontalAxis);

		if (rb.angularVelocity.magnitude > maximumAngularSpeed || rb.angularVelocity.magnitude < -maximumAngularSpeed) {
			rb.AddTorque ((transform.up * -1) * (rb.angularVelocity.magnitude - maximumAngularSpeed));
		} else {
			rb.AddTorque (transform.up * rotationDirection * rotationFactor * Time.deltaTime);
		}
	}

	private void Clamp() 
	{
		BumpAgainstEdges ();
		ClampPosition ();
		ClampRotation ();
	}

	private void ClampPosition() 
	{
		rb.position = new Vector3(Mathf.Clamp(rb.position.x, -12f, 12f), 0.0f, Mathf.Clamp(rb.position.z, -9f, 9f));
	}

	private void BumpAgainstEdges() 
	{
		if (transform.position.z > 9f || transform.position.z < -9f ||
			transform.position.x > 12f || transform.position.x < -12f) {
			rb.velocity *= -1f;
		}
	}

	private void ClampRotation()
	{
		if(rb.rotation.y != 0) {
			Quaternion r = rb.rotation;
			r.x = 0;
			r.z = 0;
			float m = Mathf.Sqrt (r.x * r.x + r.y * r.y + r.z * r.z + r.w * r.w);
			rb.rotation = new Quaternion (r.x / m, r.y / m, r.z / m, r.w / m);
		}
	}

}
