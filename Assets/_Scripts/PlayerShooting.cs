// Filename: PlayerShooting.cs
// Author: Brahm Ramkissoon
// Created Date  (dd/mm/yyyy): 20/11/2015
// Description: Handle Muzzle flash and bullet impact particles
//              -- written by Tom Tsiliopoulos
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using System.Collections;


public class PlayerShooting : MonoBehaviour
{
    // PUBLIC INSTANCE VARIABLES - Exposed on the inspector
    public ParticleSystem muzzleFlash;
    public GameObject impact;
    public Animator rifleAnimator;
    public AudioSource bulletFireSound;


    // PRIVATE INSTANCE VARIABLES
    private GameObject[] _impacts;
    private int _currentImpact = 0;
    private int _maxImpacts = 5;

    private bool _shooting = false; // gun idle is not shooting


	// Use this for initialization
	void Start ()
	{
        // create an object pool - loop 5 objects
	    this._impacts = new GameObject[this._maxImpacts];
	    for (int impactCount = 0; impactCount < this._maxImpacts ; impactCount++)
	    {
	        this._impacts[impactCount] = (GameObject) Instantiate(this.impact);
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    // play muzzle flash and shoot impact when left mouse button is pressed
	    if (CrossPlatformInputManager.GetButtonDown( "Fire1" ))
	    {
            this._shooting = true;
	        this.muzzleFlash.Play();
	        this.bulletFireSound.Play();
	        this.rifleAnimator.SetTrigger( "Fire" );
	    }

	    if (CrossPlatformInputManager.GetButtonUp("Fire1"))
	    {
            this._shooting = false;
	    }
	}
}
