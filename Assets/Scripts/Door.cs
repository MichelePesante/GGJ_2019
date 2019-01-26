using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorGroup {
	Group1,
	Group2
}

public class Door : MonoBehaviour {

	public DoorGroup group;

	public bool closed = true;
	public bool locked = false;

	private void Awake() {
		switch(gameObject.tag) {
			case "Group1":
				this.group = DoorGroup.Group1;
				break;
			case "Group2":
				this.group = DoorGroup.Group2;
				break;
			default:
				break;
		}
	}

	public void open() {
		if(!locked && closed)
			closed = false;
	}

	public void close() {
		if(!locked && !closed)
			closed = true;
	}

	public void setLocked() {
		locked = true;
	}

	public void unlock() {
		locked = false;
	}
}
