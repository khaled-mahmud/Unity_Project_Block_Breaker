using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public AudioClip crack;
	public Sprite[] hitsprites;
	public static int breakableCount = 0;
	public GameObject smoke;

	private int timesHeat;
	private LevelManager levelManager;
	private bool isBreakable;

	// Use this for initialization
	void Start ()
	{
		isBreakable = (this.tag == "Breakable");
		// Keep track of breakable bricks
		if (isBreakable) {
			breakableCount++;
		}

		timesHeat = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		//AudioSource.PlayClipAtPoint (crack, transform.position, 0.8f);
		if (isBreakable) {
			AudioSource.PlayClipAtPoint (crack, transform.position, 0.8f);
			HandleHits ();
		}
	}

	void HandleHits () {
		timesHeat++;
		int maxHits = hitsprites.Length + 1;
		if (timesHeat >= maxHits) {
			breakableCount--;
			levelManager.BrickDestroyed();
			//PuffSmoke();
			Destroy (gameObject);
		} else {
			LoadSprites();
		}
	}

	void PuffSmoke () {
			//smoke.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
            //Instantiate(smoke, gameObject.transform.position, Quaternion.identity);
            //Below is the alternate method.
            GameObject smokePuff = Instantiate (smoke, transform.position, Quaternion.identity) as GameObject;
			smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}

	void LoadSprites ()
	{
		int spriteIndex = timesHeat - 1;
		if (hitsprites [spriteIndex] != null) {
			this.GetComponent<SpriteRenderer> ().sprite = hitsprites [spriteIndex];
		} else {
			Debug.LogError ("Brick sprite missing");
		}
	}

	// TODO Remove this method once we actually win!
	void SimulateWin () {
		levelManager.LoadNextLevel();
	}
}
