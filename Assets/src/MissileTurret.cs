using UnityEngine;
using System.Collections;

public class MissileTurret : MonoBehaviour {

	public GameObject Missile;
	public float MissileRechargeTimeInSeconds = 5;	
	public int IGNORE_LAYERS;
	public bool readyToFire = true;
	public float timeUntilNextMissile = 0;
	
	Transform centerTube;
	Transform leftTube;
	Transform rightTube;
	Collider[] colliders = new Collider[5];
	
	public void Start() {
		IGNORE_LAYERS = 1 << LayerMask.NameToLayer("Turrets");
		centerTube = transform.Find("Turret/Center Tube");
		leftTube = transform.Find("Turret/Left Tube");
		rightTube = transform.Find("Turret/Right Tube");
		colliders[0] = transform.Find("Ball").GetComponent<Collider>();
		colliders[1] = transform.Find("Turret").GetComponent<Collider>();
		colliders[2] = centerTube.GetComponent<Collider>();
		colliders[3] = leftTube.GetComponent<Collider>();
		colliders[4] = rightTube.GetComponent<Collider>();
	}
	
	public void OnTriggerStay(Collider other) {
		Debug.Log("triggered by: " + other.name);
		if("Ship" == other.tag) {
			transform.LookAt(other.transform.position);
			Debug.DrawLine(transform.position, other.transform.position);
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
		Debug.Log("Firing missile at: " + target.name);
		var vectorToTarget = target.transform.position - transform.position;
      	vectorToTarget.Normalize();
		
		var ray = new Ray(transform.position, vectorToTarget);
		var hitInfo = new RaycastHit();
		
		// only fire if we have clear line of sight to the target
		if(Physics.Raycast(ray, out hitInfo, 100, ~IGNORE_LAYERS)) {
			Debug.Log("Ray hit: " + hitInfo.collider.name);
			if("Ship" == hitInfo.collider.tag) {
				var missile = (GameObject)Instantiate(Missile, centerTube.position, centerTube.rotation);
				missile.transform.Rotate(-90, 0, 0);
				foreach(Collider collider in colliders) {
					Physics.IgnoreCollision(collider, missile.collider);				
				}
				missile.GetComponent<Missile>().Launch();
				
				missile = (GameObject)Instantiate(Missile, leftTube.position, leftTube.rotation);
				missile.transform.Rotate(-90, 0, 0);
				foreach(Collider collider in colliders) {
					Physics.IgnoreCollision(collider, missile.collider);				
				}
				missile.GetComponent<Missile>().Launch();
				
				missile = (GameObject)Instantiate(Missile, rightTube.position, rightTube.rotation);
				missile.transform.Rotate(-90, 0, 0);
				foreach(Collider collider in colliders) {
					Physics.IgnoreCollision(collider, missile.collider);				
				}
				missile.GetComponent<Missile>().Launch();
				
				timeUntilNextMissile = MissileRechargeTimeInSeconds;
				readyToFire = false;
			}
		}
	}
		
}
