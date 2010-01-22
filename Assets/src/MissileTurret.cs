using UnityEngine;
using System.Collections;

public class MissileTurret : MonoBehaviour {

	public GameObject Missile;
	public float MissileRechargeTimeInSeconds = 5;
	
	int TURRETS_LAYER = 1 << LayerMask.NameToLayer("Turrets");
	public bool readyToFire = true;
	public float timeUntilNextMissile = 0;
	
	
	public void OnTriggerStay(Collider other) {
		if("Ship" == other.gameObject.tag && readyToFire) {
//			Debug.DrawLine(transform.position, other.gameObject.transform.position);
			FireMissileAt(other.gameObject);
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
				missile.transform.position = transform.position;
				missile.transform.LookAt(target.transform.position);
				missile.transform.Translate(missile.transform.forward * -5f);
				missile.GetComponent<Missile>().Launch();
				
				missile = (GameObject)Instantiate(Missile);
				missile.transform.position = transform.position;
				missile.transform.LookAt(target.transform.position);
				missile.transform.Rotate(0, -15, 0);
				missile.transform.Translate(missile.transform.forward * -5f);
				missile.GetComponent<Missile>().Launch();
				
				missile = (GameObject)Instantiate(Missile);
				missile.transform.position = transform.position;
				missile.transform.LookAt(target.transform.position);
				missile.transform.Rotate(0, 15, 0);
				missile.transform.Translate(missile.transform.forward * -5f);
				missile.GetComponent<Missile>().Launch();
				
				timeUntilNextMissile = MissileRechargeTimeInSeconds;
				readyToFire = false;
			}
		}
	}
		
}
