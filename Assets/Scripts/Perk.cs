using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk : MonoBehaviour {
	public Timer timer = new Timer();

	abstract protected void TriggerPerk(PlayerController perkOwner);
}
