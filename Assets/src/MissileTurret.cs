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
	BoxCollider turretCollider;
	
	public void Start() {
		IGNORE_LAYERS = 1 << LayerMask.NameToLayer("Turrets");
		centerTube = transform.Find("Turret/Center Tube");
		leftTube = transform.Find("Turret/Left Tube");
		rightTube = transform.Find("Turret/Right Tube");
		turretCollider = transform.Find("Ball").GetComponent<BoxCollider>();
	}
	
	public void OnTriggerStay(Collider other) {
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
		var vectorToTarget = target.transform.position - transform.position;
      	vectorToTarget.Normalize();
		
		var ray = new Ray(transform.position, vectorToTarget);
		var hitInfo = new RaycastHit();
		
		// only fire if we have clear line of sight to the target
		if(Physics.Raycast(ray, out hitInfo, 100, ~IGNORE_LAYERS)) {
			if("Ship" == hitInfo.collider.tag) {
				var missile = (GameObject)Instantiate(Missile, centerTube.position, centerTube.rotation);
				missile.transform.Rotate(-90, 0, 0);
				Physics.IgnoreCollision(turretCollider, missile.collider);
				missile.GetComponent<Missile>().Launch();
				
				missile = (GameObject)Instantiate(Missile, leftTube.position, leftTube.rotation);
				missile.transform.Rotate(-90, 0, 0);
				Physics.IgnoreCollision(turretCollider, missile.collider);
				missile.GetComponent<Missile>().Launch();
				
				missile = (GameObject)Instantiate(Missile, rightTube.position, rightTube.rotation);
				missile.transform.Rotate(-90, 0, 0);
				Physics.IgnoreCollision(turretCollider, missile.collider);
				missile.GetComponent<Missile>().Launch();
				
				timeUntilNextMissile = MissileRechargeTimeInSeconds;
				readyToFire = false;
			}
		}
	}
		
}
