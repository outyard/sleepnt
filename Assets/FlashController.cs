using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FlashController : MonoBehaviour
{
	public Light flash;
	public GameObject flashObject;
	public GameObject targetObject;
	
	public bool dead;
	
	public AudioClip sound;
    AudioSource audioSource;
	
	private bool ready;
	
    // Start is called before the first frame update
    void Start()
    {
		flash.enabled = false;
		ready = true;
		dead = false;
		
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		/*
		if (dead) {
			return;
		}*/
		
		// Move thumb stick to make camera flash
        float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		if (Math.Abs(horizontal) >= 0.5 || Math.Abs(vertical) >= 0.5) {
			if (ready) {
				StartCoroutine(Flash());
			}
		}
    }
	
	IEnumerator Flash() {
		ready = false;
		
		audioSource.PlayOneShot(sound, 1.0f);
		
		Vector3 targetPosition = targetObject.transform.position;
		Vector3 flashForward = flashObject.transform.forward;
		
		// Assumes player is centered at the origin.
		float targetAngle = Mathf.Atan2(targetPosition.z, targetPosition.x);
		float flashAngle = Mathf.Atan2(flashForward.z, flashForward.x);
		bool hit = Math.Abs(targetAngle - flashAngle) < Mathf.Deg2Rad * flash.spotAngle / 2.0f;
		
		flash.enabled = true;
		yield return new WaitForSeconds(0.1f);
		flash.enabled = false;
		
		if (hit) {
			MonsterMovement monster = targetObject.GetComponent<MonsterMovement>();
			monster.GetHit();
		}
		
		yield return new WaitForSeconds(0.5f);
		ready = true;
	}
}
