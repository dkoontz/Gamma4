using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

	public float StartingEnergy = 100;
	public float ThrusterEnergyUsePerSecond = 50;
	public float WeaponEnergyUsePerSecond = 80;
	public float SensorEnergyUsePerSecond = 20;
	public float RespawnTime = 3;
	public bool InfiniteEnergy = false;
	public Texture ThrusterIcon;
	public Texture WeaponIcon;
	public Texture SensorIcon;
	public Texture ShieldIcon;
	public Texture ThrusterIconSmall;
	public Texture WeaponIconSmall;
	public Texture SensorIconSmall;
	public Texture ShieldIconSmall;
	public GUIText ThrusterPowerText;
	public GUIText WeaponPowerText;
	public GUIText SensorPowerText;
	public GUIText ShieldPowerText;
	
	public bool Firing {get; set;}
	
	float iconScreenPercentage = 0.05333f;
	float thrusterEnergy;
	float weaponEnergy;
	float sensorEnergy;
	float shieldEnergy;
	bool respawning = false;
	float originalYPosition;
	GameObject respawn;
	
	Rect thrusterIconRect;
	Rect weaponIconRect;
	Rect sensorIconRect;
	Rect shieldIconRect;
	
	public void Start() {
		originalYPosition = transform.position.y;
		Firing = false;
		ResetPower();
		var iconSize = iconScreenPercentage * Screen.width;
		var iconSpacing = iconSize + (Screen.height / 100);
		thrusterIconRect = new Rect(5, 5, iconSize, iconSize);
		weaponIconRect = new Rect(5, thrusterIconRect.y + iconSpacing, iconSize, iconSize);
		sensorIconRect = new Rect(5, weaponIconRect.y + iconSpacing, iconSize, iconSize);
		shieldIconRect = new Rect(5, sensorIconRect.y + iconSpacing, iconSize, iconSize);
	}
	
	public void OnGUI() {
		GUI.DrawTexture(thrusterIconRect, ThrusterIcon);
		ThrusterPowerText.text = string.Format("{0:0.0}", thrusterEnergy);
		GUI.DrawTexture(weaponIconRect, WeaponIcon);
		WeaponPowerText.text = string.Format("{0:0.0}", weaponEnergy);
		GUI.DrawTexture(sensorIconRect, SensorIcon);
		SensorPowerText.text = string.Format("{0:0.0}", sensorEnergy);
		GUI.DrawTexture(shieldIconRect, ShieldIcon);
		ShieldPowerText.text = string.Format("{0:0.0}", shieldEnergy);
	}
	
	public void FixedUpdate() {
		transform.position = new Vector3(transform.position.x, originalYPosition, transform.position.z);
	}
	
	
	public bool ActivateThruster(float durationInSeconds) {
		var energyUse = 0f;
		if(!InfiniteEnergy) {
			energyUse = ThrusterEnergyUsePerSecond * durationInSeconds;
		}
		if(thrusterEnergy >= energyUse) {
			thrusterEnergy -= energyUse;
			return true;
		}
		else {
			return false;
		}
	}
	
	public bool ActivateWeapon(float durationInSeconds) {
		var energyUse = 0f;
		if(!InfiniteEnergy) {
			energyUse = WeaponEnergyUsePerSecond * durationInSeconds;
		}
		if(weaponEnergy >= energyUse) {
			weaponEnergy -= energyUse;
			return true;
		}
		else {
			return false;
		}
	}

	public bool ActivateSensor(float durationInSeconds) {
		var energyUse = 0f;
		if(!InfiniteEnergy) {
			energyUse = SensorEnergyUsePerSecond * durationInSeconds;
		}
		if(sensorEnergy >= energyUse) {
			sensorEnergy -= energyUse;
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
		GetComponent<AudioSource>().Play();
		if(!respawning) {
			shieldEnergy -= damageAmount;
			if(shieldEnergy <= 0) {
				StartCoroutine(DestroyAndRespawn(RespawnTime));
			}
		}
	}
	
	public void SetNextRespawn(GameObject respawn) {
		this.respawn = respawn;
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
		transform.position = respawn.transform.position;
		
		transform.Find("Thruster Rotator").rigidbody.velocity = Vector3.zero;
		transform.Find("Weapon Rotator").rigidbody.velocity = Vector3.zero;
		transform.rigidbody.velocity = Vector3.zero;
		ResetPower();
	}
	
	void ResetPower() {
		thrusterEnergy = StartingEnergy;
		weaponEnergy = StartingEnergy;
		sensorEnergy = StartingEnergy;
		shieldEnergy = 100;
	}
}