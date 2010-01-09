using UnityEngine;
using System.Collections;

public class PowerCore : MonoBehaviour {
	public float DefaultEnergyRecharge = 5;
	public float ThrusterEnergyRecharge = 20;
	public float WeaponEnergyRecharge = 20;
	public float SensorEnergyRecharge = 25;
	public float ShieldEnergyRecharge = 25;
	
	public float MarkerCycleTime = 2;
	public Texture PowerCoreBackground;
	public Texture PowerCoreMarker;
	
	float startTime;
	float endTime;
	float markerStart = 50;
	float markerEnd = 543;
	bool specialtyPowerupChosen;
	
	const float OFFSCREEN = -100;
	
	Rect powerCoreBackgroundRect = new Rect(50, 425, 500, 20);
	Rect thrusterIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	Rect weaponIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	Rect sensorIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	Rect shieldIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	Rect powerCoreMarkerRect = new Rect(OFFSCREEN, 425, 7, 20);
	Ship ShipBehaviour;
	
	public void Start() {
		ShipBehaviour = GetComponent<Ship>();
		ResetTrack();
	}
	
	public void Update() {
		powerCoreMarkerRect.x = Mathf.Lerp(markerStart, markerEnd, (Time.time - startTime) / MarkerCycleTime);
		
		if(Input.GetButtonDown("PowerCore")) {
			specialtyPowerupChosen = true;

			if(Overlapping(powerCoreMarkerRect, thrusterIconRect)) {
				ShipBehaviour.PowerupThruster(ThrusterEnergyRecharge);
				VoidTrack();
			}
			else if(Overlapping(powerCoreMarkerRect, weaponIconRect)) {
				ShipBehaviour.PowerupWeapon(WeaponEnergyRecharge);
				VoidTrack();
			}
			else if(Overlapping(powerCoreMarkerRect, sensorIconRect)) {
				ShipBehaviour.PowerupSensor(SensorEnergyRecharge);
				VoidTrack();
			}
			else if(Overlapping(powerCoreMarkerRect, shieldIconRect)) {
				ShipBehaviour.PowerupShield(ShieldEnergyRecharge);
				VoidTrack();
			}
			else {
				specialtyPowerupChosen = false;
				VoidTrack();
			}
		}
		
		if(Time.time > endTime) {
			if(!specialtyPowerupChosen) {
				ShipBehaviour.PowerupThruster(DefaultEnergyRecharge);
				ShipBehaviour.PowerupWeapon(DefaultEnergyRecharge);
				ShipBehaviour.PowerupSensor(DefaultEnergyRecharge);
				ShipBehaviour.PowerupShield(DefaultEnergyRecharge);
			}
			ResetTrack();
			var temp = markerEnd;
			markerEnd = markerStart;
			markerStart = temp;
		}
	}
	
	public void OnGUI() {
		GUI.backgroundColor = Color.white;
		GUI.DrawTexture(powerCoreBackgroundRect, PowerCoreBackground);
		GUI.DrawTexture(thrusterIconRect, ShipBehaviour.ThrusterIcon);
		GUI.DrawTexture(weaponIconRect, ShipBehaviour.WeaponIcon);
		GUI.DrawTexture(sensorIconRect, ShipBehaviour.SensorIcon);
		GUI.DrawTexture(shieldIconRect, ShipBehaviour.ShieldIcon);
		GUI.DrawTexture(powerCoreMarkerRect, PowerCoreMarker);
	}
	
	void ResetTrack() {
		specialtyPowerupChosen = false;
		startTime = Time.time;
		endTime = Time.time + MarkerCycleTime;
		do {
			RandomizeIconLocations();
		} while(Overlapping(thrusterIconRect, weaponIconRect) || 
		        Overlapping(thrusterIconRect, sensorIconRect) || 
		        Overlapping(thrusterIconRect, shieldIconRect) ||
		        Overlapping(weaponIconRect, sensorIconRect) ||
		        Overlapping(weaponIconRect, shieldIconRect) ||
		        Overlapping(sensorIconRect, shieldIconRect));
	}
	
	void VoidTrack() {
		thrusterIconRect.x = OFFSCREEN;
		weaponIconRect.x = OFFSCREEN;
		sensorIconRect.x = OFFSCREEN;
		shieldIconRect.x = OFFSCREEN;
		
	}
	
	bool Overlapping(Rect marker, Rect icon) {
		return ((icon.x < marker.x && icon.x + icon.width > marker.x) ||
		        (marker.x < icon.x && marker.x + marker.width > icon.x)
		       );
	}
	
	void RandomizeIconLocations() {
		thrusterIconRect.x = Random.Range(50, 530);
		weaponIconRect.x = Random.Range(50, 530);
		sensorIconRect.x = Random.Range(50, 530);
		shieldIconRect.x = Random.Range(50, 530);
	}
}
