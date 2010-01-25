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
	public ParticleEmitter SuccessVisualIndicator;
	
	static Ship ship;
	const float OFFSCREEN = -100;
	
	float startTime;
	float endTime;
	float markerStart = 50;
	float markerEnd = 543;
	float leftMarkerEdge;
	float backgroundWidth;
	float visualIndicatorEdge = 1.25f;
	bool specialtyPowerupChosen;
	
	Rect powerCoreBackgroundRect = new Rect(50, 425, 500, 20);
	Rect thrusterIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	Rect weaponIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	Rect sensorIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	Rect shieldIconRect = new Rect(OFFSCREEN, 427, 16, 16);
	Rect powerCoreMarkerRect = new Rect(OFFSCREEN, 425, 7, 20);
	Rect powerCoreCollisionRect = new Rect(OFFSCREEN, 425, 11, 20);
	float COLLISION_RECT_X_OFFSET = 5;
	
	public void Start() {
		if(null == ship) {
			ship = GameObject.Find("Ship").GetComponent<Ship>();
		}
		leftMarkerEdge = markerStart;
		backgroundWidth = markerEnd - markerStart;
		ResetTrack();
	}
	
	public void Update() {
		powerCoreMarkerRect.x = Mathf.Lerp(markerStart, markerEnd, (Time.time - startTime) / MarkerCycleTime);
		powerCoreCollisionRect = powerCoreMarkerRect;
		powerCoreCollisionRect.x -= COLLISION_RECT_X_OFFSET;

		if(Input.GetButtonDown("PowerCore")) {
			specialtyPowerupChosen = true;
			var hitSomething = false;

			if(Overlapping(powerCoreCollisionRect, thrusterIconRect)) {
				ship.PowerupThruster(ThrusterEnergyRecharge);
				VoidTrack();
				hitSomething = true;
			}
			else if(Overlapping(powerCoreCollisionRect, weaponIconRect)) {
				ship.PowerupWeapon(WeaponEnergyRecharge);
				VoidTrack();
				hitSomething = true;
			}
			else if(Overlapping(powerCoreCollisionRect, sensorIconRect)) {
				ship.PowerupSensor(SensorEnergyRecharge);
				VoidTrack();
				hitSomething = true;
			}
			else if(Overlapping(powerCoreCollisionRect, shieldIconRect)) {
				ship.PowerupShield(ShieldEnergyRecharge);
				VoidTrack();
				hitSomething = true;
			}
			else {
				specialtyPowerupChosen = false;
				VoidTrack();
			}
			
			if(hitSomething) {
				float locationPercentage = ((powerCoreMarkerRect.x - leftMarkerEdge) / backgroundWidth * 2) - 1;
				Debug.Log(locationPercentage);
				Vector3 pos = new Vector3(locationPercentage * visualIndicatorEdge, -1, 2);
				SuccessVisualIndicator.gameObject.transform.localPosition = pos;
				SuccessVisualIndicator.Emit(5);
			}
		}
		
		if(Time.time > endTime) {
			if(!specialtyPowerupChosen) {
				ship.PowerupThruster(DefaultEnergyRecharge);
				ship.PowerupWeapon(DefaultEnergyRecharge);
				ship.PowerupSensor(DefaultEnergyRecharge);
				ship.PowerupShield(DefaultEnergyRecharge / 2);
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
		GUI.DrawTexture(thrusterIconRect, ship.ThrusterIcon);
		GUI.DrawTexture(weaponIconRect, ship.WeaponIcon);
		GUI.DrawTexture(sensorIconRect, ship.SensorIcon);
		GUI.DrawTexture(shieldIconRect, ship.ShieldIcon);
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
