using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	// store references to components on the gameObject
	Rigidbody2D _rigidbody;
	Animator _animator;
	AudioSource _audio;
	Vector2 bullet_Pos;

	static int attacks = 0;

	public GameObject player;
	public int bosspower = 5;
	public int damageAmount = 10; // probably deal a lot of damage to kill player immediately

	// SFXs
	public AudioClip stunnedSFX;
	public AudioClip attackSFX;

	public GameObject bullet;

	void Awake() {		
		_rigidbody = GetComponent<Rigidbody2D> ();
		if (_rigidbody==null) // if Rigidbody is missing
			Debug.LogError("Rigidbody2D component missing from this gameobject");
		
		_animator = GetComponent<Animator>();
		if (_animator==null) // if Animator is missing
			Debug.LogError("Animator component missing from this gameobject");
		
		_audio = GetComponent<AudioSource> ();
		if (_audio==null) { // if AudioSource is missing
			Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
			// let's just add the AudioSource component dynamically
			_audio = gameObject.AddComponent<AudioSource>();
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		Fire();		
	}

	// Attack player
	void OnTriggerEnter2D(Collider2D collision)
	{
		if ((collision.tag == "Player"))
		{
			CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
			if (player.playerCanMove) {

				// attack sound
				playSound(attackSFX);
								
				// apply damage to the player
				player.ApplyDamage (damageAmount);				
			}
		}
	}

	// play sound through the audiosource on the gameobject
	void playSound(AudioClip clip)
	{
		_audio.PlayOneShot(clip);
	}

	void Fire()
	{
		bullet_Pos.x = Random.Range(-11.0f, 11.0f);
		bullet_Pos.y = 5.2f;
		Instantiate(bullet, bullet_Pos, Quaternion.identity);
	}

	public void Stunned()
	{
		playSound(stunnedSFX);
		if(attacks >= bosspower)
		{
			_animator.SetTrigger("Death");
			player.GetComponent<CharacterController2D>().Victory();
		}
		else
			attacks++;
	}
}
