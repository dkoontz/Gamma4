using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

//	public List<ParticleEmitter> AsteroidEmitters;
//	public List<ParticleEmitter> NebulaEmitters;
	
	public GUIText timeDisplay;
	
	float timeRemaining = 60 * 4;
	
	public void Start () {
//		AsteroidEmitters.ForEach(emitter => emitter.Emit(2));
//		NebulaEmitters.ForEach(emitter => {
//			emitter.Emit(300);
//			emitter.emit = false;
//		});
	}
	
	public void Update() {
		timeRemaining -= Time.deltaTime;
		timeDisplay.text = string.Format("Seconds Remaining: {0:0.00}", timeRemaining);
	}
}