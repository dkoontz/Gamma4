using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public float StartingEnergy = 100;
	public float ThrusterEnergyUsePerSecond = 50;
	public float MissileEnergyUsePerShot = 25;
	public Texture ThrusterIcon;
	public Texture WeaponIcon;
	public Texture SensorIcon;
	public Texture ShieldIcon;
		
	float thrusterEnergy;
	float weaponEnergy;
	float sensorEnergy;
	float shieldEnergy = 100;
	
	Rect thrusterIconRect = new Rect(5, 5, 32, 32);
	Rect thrusterTextRect = new Rect(40, 12, 100, 25);
	Rect weaponIconRect = new Rect(5, 35, 32, 32);
	Rect weaponTextRect = new Rect(40, 42, 100, 25);
	Rect sensorIconRect = new Rect(5, 65, 32, 32);
	Rect sensorTextRect = new Rect(40, 70, 100, 25);
	Rect shieldIconRect = new Rect(5, 100, 32, 32);
	Rect shieldTextRect = new Rect(40, 108, 100, 25);
	
	public void Start() {
		thrusterEnergy = StartingEnergy;
		weaponEnergy = StartingEnergy;
		sensorEnergy = StartingEnergy;
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
	
	public bool ActivateWeapon() {
		if(weaponEnergy >= MissileEnergyUsePerShot) {
			weaponEnergy -= MissileEnergyUsePerShot;
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
		shieldEnergy -= damageAmount;
		if(shieldEnergy <= 0) {
			//destroyed & respawn
		}
	}
}