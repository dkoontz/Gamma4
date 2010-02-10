using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSection : MonoBehaviour {

	public Transform sectionEntrance;
	public List<Transform> sectionExit;
	
	public void Start () {
	
	}
	
	public void Update () {
	
		
	}
	
	public void OnDrawGizmos() {
		if(null != sectionEntrance) {
			Gizmos.DrawIcon(sectionEntrance.position, "SectionEntrance.png");
		}
		
		if(null != sectionExit && sectionExit.Count > 0) {
			sectionExit.ForEach(exit => Gizmos.DrawIcon(exit.position, "SectionExit.png"));
		}
		
	}
}
