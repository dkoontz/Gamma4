using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public float StartingEnergy = 100;
	public float ThrusterEnergyUsePerSecond = 50;
	public float MissileEnergyUsePerShot = 25;
	public Texture ThrusterIcon;
	public Texture WeaponIcon;
		
	float thrusterEnergy;
	float weaponEnergy;
	float sensorEnergy;
	
	public void Start() {
		thrusterEnergy = StartingEnergy;
		weaponEnergy = StartingEnergy;
		sensorEnergy = StartingEnergy;
	}
	
	public void OnGUI() {
		GUI.DrawTexture(new Rect(5, 5, 32, 32), ThrusterIcon);
		GUI.Label(new Rect(40, 12, 100, 25), string.Format("{0:f}", thrusterEnergy));
		GUI.DrawTexture(new Rect(5, 35, 32, 32), WeaponIcon);
		GUI.Label(new Rect(40, 42, 100, 25), string.Format("{0:f}", weaponEnergy));
		GUI.Label(new Rect(40, 70, 100, 25), string.Format("{0:f}", sensorEnergy));
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
}