using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeLoop : MonoBehaviour {

	private AudioSource source;

	public List<AudioClip> loopClips;

	private void Awake() {
		source = GetComponent<AudioSource>();
		source.clip = loopClips[Random.Range(0, loopClips.Count)];
		source.Play();
	}
}
