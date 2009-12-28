using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public List<ParticleEmitter> AsteroidEmitters;
	
	void Start () {
		AsteroidEmitters.ForEach(emitter => emitter.Emit(1));
	}
	
}