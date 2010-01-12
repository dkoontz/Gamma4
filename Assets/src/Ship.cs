using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

	public float StartingEnergy = 100;
	public float ThrusterEnergyUsePerSecond = 50;
	public float WeaponEnergyUsePerSecond = 80;
	public float RespawnTime = 3;
	public Texture ThrusterIcon;
	public Texture WeaponIcon;
	public Texture SensorIcon;
	public Texture ShieldIcon;
	
	public bool Firing {get; set;}
		
	float thrusterEnergy;
	float weaponEnergy;
	float sensorEnergy;
	float shieldEnergy;
	bool respawning = false;
	
	Rect thrusterIconRect = new Rect(5, 5, 32, 32);
	Rect thrusterTextRect = new Rect(40, 12, 100, 25);
	Rect weaponIconRect = new Rect(5, 40, 32, 32);
	Rect weaponTextRect = new Rect(40, 47, 100, 25);
	Rect sensorIconRect = new Rect(5, 75, 32, 32);
	Rect sensorTextRect = new Rect(40, 82, 100, 25);
	Rect shieldIconRect = new Rect(5, 110, 32, 32);
	Rect shieldTextRect = new Rect(40, 117, 100, 25);
	
	public void Start() {
		Firing = false;
		ResetPower();
	}
	
	public void OnGUI() {
		GUI.DrawTexture(thrusterIconRect, ThrusterIcon);
		GUI.Label(thrusterTextRect, string.Format("{0:f}", thrusterEnergy));
		GUI.DrawTexture(weaponIconRect, WeaponIcon);
		GUI.Label(weaponTextRect, string.Format("{0:f}", weaponEnergy));
		GUI.DrawTexture(sensorIconRect, SensorIcon);
		GUI.Label(sensorTextRect, string.Format("{0:f}", sensorEnergy));
		GUI.DrawTexture(shieldIconRect, ShieldIcon);
		GUI.Label(shieldTextRect, string.Format("{0:f}", shieldEnergy));
	}
	
	public void FixedUpdate() {
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}
	
	
	public bool ActivateThruster(float durationInSeconds) {
		var energyUse = ThrusterEnergyUsePerSecond * durationInSeconds;
		if(thrusterEnergy >= energyUse) {
			thrusterEnergy -= energyUse;
			return true;
		}
		else {
			return false;
		}
	}
	
	public bool ActivateWeapon(float durationInSeconds) {
		var energyUse = WeaponEnergyUsePerSecond * durationInSeconds;
		if(weaponEnergy >= energyUse) {
			weaponEnergy -= energyUse;
			return true;
		}
		else {
			return false;
		}
	}
	
	public void PowerupThruster(float amount) {
		thrusterEnergy += amount;
		if(thrusterEnergy > StartingEnergy) { thrusterEnergy = StartingEnergy; }
	}
	
	public void PowerupWeapon(float amount) {
		weaponEnergy += amount;
		if(weaponEnergy > StartingEnergy) { weaponEnergy = StartingEnergy; }
	}
	
	public void PowerupSensor(float amount) {
		sensorEnergy += amount;
		if(sensorEnergy > StartingEnergy) { sensorEnergy = StartingEnergy; }
	}
	
	public void PowerupShield(float amount) {
		shieldEnergy += amount;
		if(shieldEnergy > StartingEnergy) { shieldEnergy = StartingEnergy; }
	}
	
	public void Damage(float damageAmount) {
		if(!respawning) {
			shieldEnergy -= damageAmount;
			if(shieldEnergy <= 0) {
				StartCoroutine(DestroyAndRespawn(RespawnTime));
			}
		}
	}
	
	IEnumerator DestroyAndRespawn(float delayTime) {
		respawning = true;
		var emitters = GetComponentsInChildren<ParticleEmitter>();
		foreach(var e in emitters) {
			if("Ship Explosion" == e.name) {
				e.emit = true;
			}
			else if("Power Core" == e.name) {
				e.emit = false;
			}
		}

		yield return new WaitForSeconds(RespawnTime);
	
		foreach(var e in emitters) {
			if("Ship Explosion" == e.name) {
				e.emit = false;
			}
			else if("Power Core" == e.name) {
				e.emit = true;
			}
		}
		
		Respawn();
		respawning = false;
	}
	
	void Respawn() {
		transform.position = new Vector3(0, 0, 0);
		
		GetComponentInChildren<FaceHeadingOfParent>().gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
		transform.rigidbody.velocity = new Vector3(0, 0, 0);
		transform.rigidbody.angularVelocity = new Vector3(0, 0, 0);
		ResetPower();
	}
	
	void ResetPower() {
		thrusterEnergy = StartingEnergy;
		weaponEnergy = StartingEnergy;
		sensorEnergy = StartingEnergy;
		shieldEnergy = 100;
	}
}