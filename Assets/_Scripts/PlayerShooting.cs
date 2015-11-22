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
    public AudioSource bulletImpactSound;
    public GameObject explosion;


    // PRIVATE INSTANCE VARIABLES
    private GameObject[] _impacts;
    private int _currentImpact = 0;
    private int _maxImpacts = 5;
    private Transform _transform;

    private bool _shooting = false; // gun idle is not shooting


	// Use this for initialization
	void Start ()
	{

        // reference to the gameObject's transform component
        this._transform = gameObject.GetComponent<Transform>(); 
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

    // Physics Effects
    void FixedUpdate()
    {
        if (this._shooting)
        {
            this._shooting = false;
            RaycastHit hit;

            Debug.DrawRay( this._transform.position, this._transform.forward, Color.green );
            if (Physics.Raycast (this._transform.position, this._transform.forward, out hit, 50f))
            {
                // whenever shots hit barrel, it explodes
                if (hit.transform.CompareTag("Barrel"))
                {
                    Destroy( hit.transform.gameObject );
                    Instantiate( this.explosion, hit.point, Quaternion.identity );
                    this.explosion.transform.position = hit.point;
                }

                // show particle system at location of ray impact
                this._impacts[this._currentImpact].transform.position = hit.point;
                this._impacts[this._currentImpact].GetComponent<ParticleSystem>().Play();
                this.bulletFireSound.Play();

                // ensure array does not go out of bounds
                if (++this._currentImpact >= this._maxImpacts)
                {
                    this._currentImpact = 0;
                }
            }
        }
    }
}
