using UnityEngine;
using System.Collections;

public class PowerCore : MonoBehaviour {
	public Ship ShipBehaviour;
	public Texture PowerCoreBackground;
	public Texture PowerCoreMarker;
	public float MarkerCycleTime = 0.5f;
	
	private float startTime;
	private float endTime;
	private float markerStart = 50;
	private float markerEnd = 543;
	
	public void Start() {
		ResetMarkerTime();
	}
	
	public void Update() {
		if(Input.GetButtonDown("PowerCore")) {
			Debug.Log("pressed");
		}
		
		if(Time.time > endTime) {
			ResetMarkerTime();
			var temp = markerEnd;
			markerEnd = markerStart;
			markerStart = temp;
		}
	}
	
	public void OnGUI() {
		GUI.backgroundColor = Color.white;
		GUI.DrawTexture(new Rect(50, 425, 500, 20), PowerCoreBackground);
		GUI.DrawTexture(new Rect(100, 427, 16, 16), ShipBehaviour.ThrusterIcon);
		GUI.DrawTexture(new Rect(484, 427, 16, 16), ShipBehaviour.WeaponIcon);
		var markerXPosition = Mathf.Lerp(markerStart, markerEnd, (Time.time - startTime) / MarkerCycleTime);
		GUI.DrawTexture(new Rect(markerXPosition, 425, 7, 20), PowerCoreMarker);
	}
	
	void ResetMarkerTime() {
		startTime = Time.time;
		endTime = Time.time + MarkerCycleTime;
	}
}
