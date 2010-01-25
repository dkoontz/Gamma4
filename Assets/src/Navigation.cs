using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {
	public Ship ShipBehaviour;
	public float ScrollSpeedPerSecond = 0.02f;
	
	public float MapBottomPositionInWorldspace = -20;
	public float MapTopPositionInWorldspace = 720;
	
	float mapHeight;
	float offset = 0;
	float shipOffset = 0;
	
	public void Start() {
		mapHeight = MapTopPositionInWorldspace - MapBottomPositionInWorldspace;
	}
	
	public void Update () {
		// calculate the ship's offset
		shipOffset = (ShipBehaviour.transform.position.z - MapBottomPositionInWorldspace) / mapHeight;
		
		if(Input.GetButton("Sensor") && ShipBehaviour.ActivateSensor(Time.deltaTime)) {
			offset += ScrollSpeedPerSecond * Time.deltaTime;
		}
		else {
			offset -= ScrollSpeedPerSecond * Time.deltaTime * 2;
			if(offset < shipOffset) {
				offset = shipOffset;
			}
		}
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}
