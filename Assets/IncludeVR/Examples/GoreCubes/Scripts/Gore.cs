using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gore : MonoBehaviour
{
    public GameObject splat;
    public Color colour;
    string[] ignoreList = new string[] { "gore", "ExplodeCube", "HMD", "Cube", "Left Hand", "Right Hand" };

    // Use this for initialization
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = colour;
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
        foreach(string s in ignoreList)
        {
            if (collision.gameObject.name == s) return;
        }

        foreach (ContactPoint contact in collision.contacts)
        {
            GameObject blyat = Instantiate(splat, contact.point + Vector3.up * 0.001f, Quaternion.Euler(90, 0, 0));
            float rand = Random.Range(-0.25f, 0.25f);
            blyat.transform.localScale += new Vector3(rand, rand, rand);
            blyat.GetComponent<MeshRenderer>().material.color = colour;
            Destroy(blyat, Random.Range(15f, 20f));
        }
    }
}
