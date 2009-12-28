using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

	public GameObject Particle;
	
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
		}
		
		emitter.ClearParticles();
	}
}
