using UnityEngine;
using System.Collections;

public class FaceHeadingOfParent : MonoBehaviour {

	private Vector3 previousPosition;
	private Vector3 headingVector;
	
	public void Start () {
		previousPosition = transform.position;
		headingVector = new Vector3();
	}
	
	public void Update () {
		headingVector.x = transform.position.x - previousPosition.x;
		headingVector.y = transform.position.y - previousPosition.y;
		headingVector.z = transform.position.z - previousPosition.z;
		if(headingVector.magnitude > 0.03) {
			transform.LookAt(transform.position + headingVector);
		}
		previousPosition = transform.position;
	}
}
