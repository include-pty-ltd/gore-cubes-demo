using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gore : MonoBehaviour
{
    public GameObject splat;

    // Use this for initialization
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "gore" && collision.gameObject.name != "ExplodeCube")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                GameObject blyat = Instantiate(splat, contact.point + Vector3.up * 0.001f, Quaternion.identity);
                float rand = Random.Range(-0.25f, 0.25f);
                blyat.transform.localScale += new Vector3(rand, rand, rand);
                blyat.transform.rotation = Quaternion.FromToRotation(-Vector3.forward, Vector3.up);
                Destroy(blyat, Random.Range(15f, 20f));
            }
        }
    }
}
