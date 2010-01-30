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
	const float COLLISION_RECT_X_OFFSET = 5;
	
	float powerCoreiconScreenWidthPercentage = 0.025f;
	float powerCoreBackgroundHeight = 20;
	float powerCoreIconHeight = 16;
	float powerCoreIconWidth;
	float powerCoreIconYPosition;
	float bottomOfMainCamera;
	
	float startTime;
	float endTime;
	float markerStart = 0;
	float markerEnd;
	float leftMarkerEdge;
	float backgroundWidth;
	float visualIndicatorEdge = 1.5f;
	bool specialtyPowerupChosen;
	
	Rect powerCoreBackgroundRect;
	Rect thrusterIconRect;
	Rect weaponIconRect;
	Rect sensorIconRect;
	Rect shieldIconRect;
	Rect powerCoreMarkerRect;
	Rect powerCoreCollisionRect;
	
	public void Start() {
		if(null == ship) {
			ship = GameObject.Find("Ship").GetComponent<Ship>();
		}
		
		markerEnd = Screen.width;
		bottomOfMainCamera = Screen.height - GameObject.Find("Map Camera").GetComponent<Camera>().pixelHeight;
		powerCoreIconYPosition = bottomOfMainCamera - ((powerCoreBackgroundHeight - powerCoreIconHeight) / 2) - powerCoreIconHeight;
		powerCoreBackgroundRect = new Rect(0, bottomOfMainCamera - powerCoreBackgroundHeight, (float)Screen.width, powerCoreBackgroundHeight);
		
		powerCoreIconWidth = Screen.width * powerCoreiconScreenWidthPercentage;
		thrusterIconRect = new Rect(OFFSCREEN, powerCoreIconYPosition, powerCoreIconWidth, powerCoreIconHeight);
		weaponIconRect = new Rect(OFFSCREEN, powerCoreIconYPosition, powerCoreIconWidth, powerCoreIconHeight);
		sensorIconRect = new Rect(OFFSCREEN, powerCoreIconYPosition, powerCoreIconWidth, powerCoreIconHeight);
		shieldIconRect = new Rect(OFFSCREEN, powerCoreIconYPosition, powerCoreIconWidth, powerCoreIconHeight);
		powerCoreMarkerRect = new Rect(OFFSCREEN, bottomOfMainCamera - powerCoreBackgroundHeight, 7, powerCoreBackgroundHeight);
		powerCoreCollisionRect = new Rect(powerCoreMarkerRect.x, powerCoreMarkerRect.y, powerCoreMarkerRect.width + (2 * COLLISION_RECT_X_OFFSET), powerCoreBackgroundHeight);	
		
		leftMarkerEdge = markerStart;
		backgroundWidth = markerEnd - markerStart;
		ResetTrack();
	}
	
	public void Update() {
		powerCoreMarkerRect.x = Mathf.Lerp(markerStart, markerEnd, (Time.time - startTime) / MarkerCycleTime);
		powerCoreCollisionRect = powerCoreMarkerRect;
		powerCoreCollisionRect.x -= COLLISION_RECT_X_OFFSET;

		if(Input.GetButtonDown("Player3")) {
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
				Vector3 pos = new Vector3(locationPercentage * visualIndicatorEdge, 
				                          SuccessVisualIndicator.transform.localPosition.y,
				                          SuccessVisualIndicator.transform.localPosition.z);
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
		GUI.DrawTexture(thrusterIconRect, ship.ThrusterIconSmall);
		GUI.DrawTexture(weaponIconRect, ship.WeaponIconSmall);
		GUI.DrawTexture(sensorIconRect, ship.SensorIconSmall);
		GUI.DrawTexture(shieldIconRect, ship.ShieldIconSmall);
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
		thrusterIconRect.x = Random.Range(0, Screen.width - powerCoreIconWidth);
		weaponIconRect.x = Random.Range(0, Screen.width - powerCoreIconWidth);
		sensorIconRect.x = Random.Range(0, Screen.width - powerCoreIconWidth);
		shieldIconRect.x = Random.Range(0, Screen.width - powerCoreIconWidth);
	}
}
