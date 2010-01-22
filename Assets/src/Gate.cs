using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gate : MonoBehaviour {
	public int CellsRequiredToOpen = 2;
	
	List<GatePowerCell> activeCells = new List<GatePowerCell>();
	
	public void PowerCellActivated(GatePowerCell cell) {
		if(!activeCells.Contains(cell)) {
			activeCells.Add(cell);
		}
		
		if(activeCells.Count >= CellsRequiredToOpen) {
			transform.Find("Gate").animation.Play();
		}
	}
}
