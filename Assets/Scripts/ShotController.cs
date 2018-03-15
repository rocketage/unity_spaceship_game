using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

	public float speed = 10f;

	void Start () {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
		Destroy (gameObject, 2.0f);
	}

}
