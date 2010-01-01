using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public float StartingEnergy = 100;
	public float EnergyRechargePerSecond = 2;
	public float ThrusterEnergyUsePerSecond = 100;
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
	
	public void Update () {
		thrusterEnergy += EnergyRechargePerSecond * Time.deltaTime;
		weaponEnergy += EnergyRechargePerSecond * Time.deltaTime;
		sensorEnergy += EnergyRechargePerSecond * Time.deltaTime;
		
		if(thrusterEnergy > StartingEnergy) { thrusterEnergy = StartingEnergy; }
		if(weaponEnergy > StartingEnergy) { weaponEnergy = StartingEnergy; }
		if(sensorEnergy > StartingEnergy) { sensorEnergy = StartingEnergy; }
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
	
	public bool ActivatePowerCore() {
		return true;
	}
}