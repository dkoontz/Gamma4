using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public float Energy = 100;
	public float MaxEnergy = 100;
	public float ShieldStrength = 100;
	public float ThrusterEnergyUsePerSecond = 100;
	public float MissileEnergyUsePerShot = 25;
	public float EnergyRechargePerSecond = 20;
	
	public Material HighEnergyMaterial;
	public Material MediumEnergyMaterial;
	public Material LowEnergyMaterial;
		
	public void Update () {
		Energy += EnergyRechargePerSecond * Time.deltaTime;
		
		if(Energy > MaxEnergy) {
			Energy = MaxEnergy;
		}
	}
	
	public void OnGUI() {
		GUI.Label(new Rect(5, 5, 100, 25), Energy.ToString());
	}
	
	public bool ActivateThruster(float durationInSeconds) {
		var energyUse = ThrusterEnergyUsePerSecond * durationInSeconds;
		if(Energy >= energyUse) {
			Energy -= energyUse;
			return true;
		}
		else {
			return false;
		}
	}
	
	public bool ActivateWeapon() {
		if(Energy > MissileEnergyUsePerShot) {
			Energy -= MissileEnergyUsePerShot;
			return true;
		}
		else {
			return false;
		}
	}
}
