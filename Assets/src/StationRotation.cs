using UnityEngine;
using System.Collections;

public class StationRotation : MonoBehaviour {
	public float SecondsPerRotation = 2;
	public Transform Child;
	
	private float rotationDegreesPerSecond;
	
	void Start () {
		if(Child != null) {
			Child.parent = transform;
		}
		rotationDegreesPerSecond = 360 / SecondsPerRotation;	
	}
	
	void FixedUpdate () {
		transform.Rotate(transform.up * rotationDegreesPerSecond * Time.deltaTime);
	}
}
