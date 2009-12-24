using UnityEngine;
using System.Collections;

public class StationRotation : MonoBehaviour {
	public float SecondsPerRotation = 2;
	
	private float rotationDegreesPerSecond;
	
	void Start () {
		rotationDegreesPerSecond = 360 / SecondsPerRotation;	
	}
	
	void FixedUpdate () {
		transform.Rotate(transform.up * rotationDegreesPerSecond * Time.deltaTime);
	}
}
