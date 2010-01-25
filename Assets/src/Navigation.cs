using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {
	public float ScrollSpeedPerSecond = 0.02f;
	
	public float MapBottomPositionInWorldspace = -20;
	public float MapTopPositionInWorldspace = 720;
	
	static Ship ship;
	
	float mapHeight;
	float offset = 0;
	float shipOffset = 0;
	
	public void Start() {
		if(null == ship) {
			ship = GameObject.Find("Ship").GetComponent<Ship>();
		}
		mapHeight = MapTopPositionInWorldspace - MapBottomPositionInWorldspace;
	}
	
	public void Update () {
		shipOffset = (ship.transform.position.z - MapBottomPositionInWorldspace) / mapHeight;
		
		if(Input.GetButton("Sensor") && ship.ActivateSensor(Time.deltaTime)) {
			offset += ScrollSpeedPerSecond * Time.deltaTime;
		}
		else {
			offset -= ScrollSpeedPerSecond * Time.deltaTime * 0.5f;
			if(offset < shipOffset) {
				offset = shipOffset;
			}
		}
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}
