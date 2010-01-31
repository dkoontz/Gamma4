using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject Laser;
	public ParticleEmitter LaserBurnEffect;
	
	static Ship ship;
	
	const float BEAM_LENGTH = 20.5f;
	float mining_laser_scale;
	GameObject beam;
	int laser_raycast_mask;
	
	
	public void Start() {
		if(null == ship) {
			ship = GameObject.Find("Ship").GetComponent<Ship>();
		}
		
		laser_raycast_mask = (1 << LayerMask.NameToLayer("Turrets")) + (1 << LayerMask.NameToLayer("Ignore Raycast"));
		beam = Laser.transform.Find("Beam").gameObject;
		beam.GetComponent<MeshRenderer>().enabled = false;
		beam.GetComponent<BoxCollider>().isTrigger = true;
		Laser.transform.parent = gameObject.transform;
		mining_laser_scale = beam.transform.parent.localScale.y;
		LaserBurnEffect.emit = false;
	}
	
	public void Update() {
		if(Input.GetButton("Player2") && ship.ActivateWeapon(Time.deltaTime)) {
			beam.GetComponent<MeshRenderer>().enabled = true;
			ship.Firing = true;
			if(!Laser.GetComponent<AudioSource>().isPlaying) {
				Laser.GetComponent<AudioSource>().Play();
			}
			
			Debug.DrawRay(beam.transform.position, beam.transform.up * BEAM_LENGTH);
			Debug.DrawRay(beam.transform.position + new Vector3(0, 0.5f, 0), beam.transform.up * BEAM_LENGTH);
			Debug.DrawRay(beam.transform.position + new Vector3(0, 1f, 0), beam.transform.up * BEAM_LENGTH);
			RaycastHit hitInfo;
			if(Physics.Raycast(beam.transform.position, beam.transform.up, out hitInfo, BEAM_LENGTH, ~laser_raycast_mask) || 
			   Physics.Raycast(beam.transform.position + new Vector3(0, 0.5f, 0), beam.transform.up, out hitInfo, BEAM_LENGTH, ~laser_raycast_mask) ||
			   Physics.Raycast(beam.transform.position + new Vector3(0, 1f, 0), beam.transform.up, out hitInfo, BEAM_LENGTH, ~laser_raycast_mask)) {
				var scale = beam.transform.parent.localScale;
				beam.transform.parent.localScale = new Vector3(scale.x, (hitInfo.distance / BEAM_LENGTH) * mining_laser_scale, scale.z);
//				Debug.Log("Raycast hit: " + hitInfo.transform.gameObject.name + ", distance; " + hitInfo.distance +
//				          ", %: " + hitInfo.distance / BEAM_LENGTH + " of " + mining_laser_scale + " is: " + (hitInfo.distance / BEAM_LENGTH) * mining_laser_scale);
				LaserBurnEffect.gameObject.transform.position = hitInfo.point;
				LaserBurnEffect.emit = true;
			}
			else {
				var scale = beam.transform.parent.localScale;
				beam.transform.parent.localScale = new Vector3(scale.x, mining_laser_scale, scale.z);
				LaserBurnEffect.emit = false;
			}
		}
		else {
			beam.GetComponent<MeshRenderer>().enabled = false;
			ship.Firing = false;
			LaserBurnEffect.emit = false;
			Laser.GetComponent<AudioSource>().Pause();
		}
	}
}