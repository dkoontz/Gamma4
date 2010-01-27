using UnityEngine;
using System.Collections;

public class MissileTurret : MonoBehaviour {

	public GameObject Missile;
	public float MissileRechargeTimeInSeconds = 5;
		
	int TURRETS_LAYER = 1 << LayerMask.NameToLayer("Turrets");
	bool readyToFire = true;
	float timeUntilNextMissile = 0;
	Transform centerTube;
	Transform leftTube;
	Transform rightTube;
	
	public void Start() {
		centerTube = transform.Find("Center Tube");
		leftTube = transform.Find("Left Tube");
		rightTube = transform.Find("Right Tube");
	}
	
	public void OnTriggerStay(Collider other) {
		if("Ship" == other.gameObject.tag) {
			transform.LookAt(other.gameObject.transform.position);
			Debug.DrawLine(transform.position, other.gameObject.transform.position);
			if(readyToFire) {
				FireMissileAt(other.gameObject);
			}
		}
	}
	
	public void Update () {
		if(timeUntilNextMissile > 0) {
			timeUntilNextMissile -= Time.deltaTime;
		}
		
		if(timeUntilNextMissile <= 0) {
			readyToFire = true;
		}
	}
	
	void FireMissileAt(GameObject target) {
		var vectorToTarget = target.transform.position - transform.position;
      	vectorToTarget.Normalize();
		
		var ray = new Ray(transform.position, vectorToTarget);
		var hitInfo = new RaycastHit();
		
		// only fire if we have clear line of sight to the target
		if(Physics.Raycast(ray, out hitInfo, 100, ~TURRETS_LAYER)) {
			if("Ship" == hitInfo.transform.gameObject.tag) {
				var missile = (GameObject)Instantiate(Missile);
				missile.transform.position = centerTube.position;
				missile.transform.rotation = centerTube.rotation;
				missile.transform.Rotate(-90, 0, 0);
				missile.GetComponent<Missile>().Launch();
				
				missile = (GameObject)Instantiate(Missile);
				missile.transform.position = leftTube.position;
				missile.transform.rotation = leftTube.rotation;
				missile.transform.Rotate(-90, 0, 0);
				missile.GetComponent<Missile>().Launch();
				
				missile = (GameObject)Instantiate(Missile);
				missile.transform.position = rightTube.position;
				missile.transform.rotation = rightTube.rotation;
				missile.transform.Rotate(-90, 0, 0);
				missile.GetComponent<Missile>().Launch();
				
				timeUntilNextMissile = MissileRechargeTimeInSeconds;
				readyToFire = false;
			}
		}
	}
		
}
