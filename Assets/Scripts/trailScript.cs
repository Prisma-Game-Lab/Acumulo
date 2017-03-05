using UnityEngine;

public class trailScript : MonoBehaviour {

	#region Public Variables

	#endregion

	#region Private Variables

	TrailRenderer thisTrail;
	#endregion 

	#region Start and Awake
	void Awake () {
		thisTrail = GetComponent<TrailRenderer>();
	}
	private void Start()
	{	thisTrail.sortingLayerName = "Player";
		thisTrail.sortingOrder = -1;
	}

	#endregion

	#region Fixed-Update²
	void Update () {
		
	}
	#endregion

	#region Auxiliary Variables

	#endregion
}
