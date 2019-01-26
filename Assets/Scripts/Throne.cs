using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throne : MonoBehaviour {
	private Light throneLight;

	public bool isActive = false;

	private void Awake() {
		throneLight = GetComponentInChildren<Light>();
	}

	public void Activate() {
		isActive = true;

		throneLight.enabled = true;
	}

	public void Deactivate() {
		isActive = false;

		throneLight.enabled = false;
	}
}
