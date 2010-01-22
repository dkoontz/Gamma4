using UnityEngine;
using System.Collections;

public class GatePowerCell : MonoBehaviour {
	public Gate LinkedGate;
	public float PowerupPerSecond = 100;
	
	float powerLevel = 0;
	ParticleEmitter activeIndicator;
	bool activated = false;
	Ship ship;

	public void Start() {
		SetTransparency(0.2f);
		activeIndicator = transform.Find("Active Indicator").GetComponent<ParticleEmitter>();
		activeIndicator.emit = false;
		ship = GameObject.Find("Ship").GetComponent<Ship>();
	}
	
	public void OnTriggerStay(Collider other) {
		if(!activated) {
			if("Weapon" == other.tag && ship.Firing) {
				powerLevel += PowerupPerSecond * Time.deltaTime;
			}
			
			if(powerLevel >= 100) { 
				powerLevel = 100;
				activeIndicator.emit = true;
				activated = true;
				LinkedGate.PowerCellActivated(this);
			}
	
			SetTransparency(((powerLevel / 100) * 0.8f) + 0.2f);
		}
	}
	
	void SetTransparency(float alpha) {
		var color = renderer.material.color;
		renderer.material.color = new Color(color.r, color.g, color.b, alpha);
	}
}
