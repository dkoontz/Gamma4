using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

	public GameObject Particle;
	public bool OneShot = false;
	public Vector3 RandomRotation = new Vector3(0, 0, 0);
	
	ParticleEmitter emitter;
	
	public void Start() {
		emitter = GetComponent<ParticleEmitter>();
	}
	
	public void Update() {
		if(emitter.particleCount == 0) { return; }
		
		foreach(var particle in emitter.particles) {
			var go = (GameObject)Instantiate(Particle);
			
			go.transform.position = particle.position;
			go.transform.LookAt(particle.position + particle.velocity);
			go.rigidbody.velocity = particle.velocity;
			go.rigidbody.rotation = Quaternion.Euler(Random.Range(-RandomRotation.x, RandomRotation.x),
			                                         Random.Range(-RandomRotation.y, RandomRotation.y),
			                                         Random.Range(-RandomRotation.z, RandomRotation.z));
		}
		
		if(OneShot) {
			emitter.emit = false;
		}
		emitter.ClearParticles();
	}
}
