using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MonsterMovement : MonoBehaviour
{
	public Transform target;
	public float speed = 1;
	public GameObject flashObject;
	
	public AudioClip sound;
    AudioSource audioSource;
	
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		// https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
		
		// Rotate the camera every frame so it keeps looking at the target
		GameObject targetXZ = new GameObject();
		Transform transformXZ = targetXZ.transform;
		Vector3 positionXZ =
			new Vector3(transformXZ.position.x, transform.position.y, transformXZ.position.x);
		transformXZ.position = positionXZ;
        transform.LookAt(targetXZ.transform);

        // Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
        //transform.LookAt(target, Vector3.left);
		
		transform.position += transform.forward * speed * Time.deltaTime;
		
		if (Vector3.Distance(transform.position, target.position) < 2.0f) {
			// TODO: Player is killed
			FlashController flash = flashObject.GetComponent<FlashController>();
			flash.dead = true;
			audioSource.PlayOneShot(sound, 0.5f);
		}
    }
	
	public void GetHit() {
		System.Random rand = new System.Random();
		
		Vector3 position = new Vector3();
		float angle = (float)rand.Next(0, 360);  // fix this angle
		float distance = 32.0f;
		position.x = Mathf.Cos(angle) * distance;
		position.y = transform.position.y;
		position.z = Mathf.Sin(angle) * distance;
		transform.position = position;
		
		speed += 0.2f;
	}
}
