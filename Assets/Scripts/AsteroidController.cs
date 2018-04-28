using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour 
{
	private Rigidbody rb;

	void Start() 
	{
		rb = GetComponent<Rigidbody> ();
	}
		
	void FixedUpdate () 
	{
		WrapBounds ();
		//ClampRotation ();
	}

	void WrapBounds()
	{
		if (rb.position.x > 19.0f || rb.position.x < -19.0f) {
			rb.position = new Vector3(rb.position.x * -1.0f, 0.0f, rb.position.z);
		}

		if (rb.position.z > 11.0f || rb.position.z < -11.0f) {
			rb.position = new Vector3(rb.position.x, 0.0f, rb.position.z * -1.0f);
		}
	}

	private void ClampRotation()
	{
		// doesn't work... :-(
		if (rb.rotation.y != 0) {
			Quaternion r = rb.rotation;
			r.x = 0;
			r.z = 0;
			float m = Mathf.Sqrt (r.x * r.x + r.y * r.y + r.z * r.z + r.w * r.w);
			rb.rotation = new Quaternion (r.x / m, r.y / m, r.z / m, r.w / m);
		}
	}
}
