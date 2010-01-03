using UnityEngine;
using System.Collections;

public class PowerCore : MonoBehaviour {
	public float DefaultEnergyRecharge = 5;
	public float ThrusterEnergyRecharge = 15;
	public float WeaponEnergyRecharge = 15;
	public float SensorEnergyRecharge = 15;
	
	public Ship ShipBehaviour;
	public float MarkerCycleTime = 2;
	public Texture PowerCoreBackground;
	public Texture PowerCoreMarker;
	
	private float startTime;
	private float endTime;
	private float markerStart = 50;
	private float markerEnd = 543;
	private bool specialtyPowerupChosen;
	
	private const float OFFSCREEN = -100;
	
	private Rect powerCoreBackgroundRect = new Rect(50, 425, 500, 20);
	private Rect thrusterIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	private Rect weaponIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	private Rect sensorIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	private Rect powerCoreMarkerRect = new Rect(OFFSCREEN, 425, 7, 20);
	
	public void Start() {
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
		GUI.DrawTexture(powerCoreMarkerRect, PowerCoreMarker);
	}
	
	void ResetTrack() {
		specialtyPowerupChosen = false;
		startTime = Time.time;
		endTime = Time.time + MarkerCycleTime;
		thrusterIconRect.x = Random.Range(50, 530);
		weaponIconRect.x = Random.Range(50, 530);
	}
	
	void VoidTrack() {
		thrusterIconRect.x = OFFSCREEN;
		weaponIconRect.x = OFFSCREEN;
	}
	
	bool Overlapping(Rect marker, Rect icon) {
		return ((icon.x < marker.x && icon.x + icon.width > marker.x) ||
		        (marker.x < icon.x && marker.x + marker.width > icon.x)
		       );
	}
}
