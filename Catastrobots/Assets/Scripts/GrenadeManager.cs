using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeManager : MonoBehaviour {

    public float countDown = 3f;
    public float throwForce = 1f;
    Rigidbody rb;
    ParticleSystem explosionParticles;
    bool played = false;

    public float radius = 5.0f;
    public float power = 10.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        explosionParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {
        //ThrowGrenade();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        countDown -= Time.deltaTime;

        if(countDown <= 0f)
        {
            if(!played)
            {
                explosionParticles.Play();
                played = true;
            }
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach(Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                EnemyHealth eh = hit.GetComponent<EnemyHealth>();

                if(rb != null)
                {
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
                }

                if (eh != null)
                {
                    eh.TakeDamage(1000, Vector3.zero);
                }
            }

            Destroy(gameObject, 1f);
        }
	}

    void ThrowGrenade()
    {
        rb.AddRelativeForce(Vector3.forward * throwForce);
    }
}
