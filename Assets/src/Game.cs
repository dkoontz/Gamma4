using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public List<ParticleEmitter> AsteroidEmitters;
	public List<ParticleEmitter> NebulaEmitters;
	
	public void Start () {
		AsteroidEmitters.ForEach(emitter => emitter.Emit(2));
		NebulaEmitters.ForEach(emitter => {
			emitter.Emit(300);
			emitter.emit = false;
		});
	}
}