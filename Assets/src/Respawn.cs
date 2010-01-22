using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	public void OnTriggerEnter(Collider other) {
		if("Ship" == other.tag) {
			Debug.Log(other.gameObject.name);
			other.gameObject.GetComponent<Ship>().SetNextRespawn(this.gameObject);
		}
	}
}
