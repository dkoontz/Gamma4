using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {
	public float ScrollSpeedPerSecond = 0.02f;
	
//	public float MapBottomPositionInWorldspace = -20;
//	public float MapTopPositionInWorldspace = 720;
	
	static Ship ship;
	
//	float mapHeight;
//	float offset = 0;
//	float shipOffset = 0;
	public float zPosition;
	
	public void Start() {
		if(null == ship) {
			ship = GameObject.Find("Ship").GetComponent<Ship>();
		}
		zPosition = ship.transform.position.z;
//		mapHeight = MapTopPositionInWorldspace - MapBottomPositionInWorldspace;
	}
	
	public void Update () {
//		shipOffset = ((ship.transform.position.z - MapBottomPositionInWorldspace) / mapHeight) * 0.70f;
		
		
		if(Input.GetButton("Sensor") && ship.ActivateSensor(Time.deltaTime)) {
			zPosition += ScrollSpeedPerSecond * Time.deltaTime;
		}
		else {
			zPosition -= ScrollSpeedPerSecond * Time.deltaTime * 0.5f;
			if(zPosition < ship.transform.position.z) {
				zPosition = ship.transform.position.z;
			}
		}
		transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
		//		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}
