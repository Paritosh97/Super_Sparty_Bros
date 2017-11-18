using UnityEngine;
using System.Collections;

public class BossDie : MonoBehaviour {

	// if Player hits the stun point of the enemy, then call Stunned on the enemy
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// tell the enemy to be stunned
			this.GetComponentInParent<Boss>().Stunned();
            other.gameObject.GetComponent<CharacterController2D>().EnemyBounce();

            other.gameObject.GetComponent<CameraShake>().Shake(0.5f, 1.0f);
		}
	}
}
