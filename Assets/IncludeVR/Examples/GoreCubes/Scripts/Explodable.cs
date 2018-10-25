using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Include;

public class Explodable : MonoBehaviour
{

    public GameObject gore;
    public int amountOfGore;
    public float explosionForce;
    public BoxCollider leftHand;
    public BoxCollider rightHand;

    new BoxCollider collider;
    int touchId;
    bool exploded;
    Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        startPos = transform.position;
        touchId = Include.Input.RegisterTouchable(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Include.Input.IsTouched(gameObject) && !exploded)
        {
            Explode(new Color(0xB2, 0x00, 0x00));
        }

        if (UnityEngine.Input.GetAxis("Left Trigger") > 0.5f && !exploded)
        {
            if (leftHand.bounds.Intersects(collider.bounds))
            {
                Explode(new Color(0x00, 0x00, 0xB2));
            }
        }

        if (UnityEngine.Input.GetAxis("Right Trigger") > 0.5f && !exploded)
        {
            if (rightHand.bounds.Intersects(collider.bounds))
            {
                Explode(new Color(0x00, 0xB2, 0x00));
            }
        }
    }

    void Explode(Color colour)
    {
        exploded = true;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Rigidbody>().detectCollisions = false;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().useGravity = false;
        for (int i = 0; i < amountOfGore; i++)
        {
            float rand1 = Random.Range(-0.1f, 0.1f);
            float rand2 = Random.Range(5f, 10f);
            GameObject gaw = Instantiate(gore, this.transform);
            gaw.GetComponent<Gore>().colour = colour;
            Destroy(gaw, rand2);
            gaw.transform.localScale += new Vector3(rand1, rand1, rand1);
            gaw.transform.position = this.transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
            gaw.name = "gore";
            gaw.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, this.transform.position, 1f, 0f, ForceMode.Impulse);
        }
        StartCoroutine(DelayedRespawn(5));
    }

    IEnumerator DelayedRespawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        Respawn();
        RandomizePos();
    }

    void Respawn()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<Rigidbody>().detectCollisions = true;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.transform.position = startPos;
        exploded = false;
    }

    void RandomizePos()
    {
        this.transform.position = new Vector3(Random.Range(-2f, 2f), 5, Random.Range(-2f, 2f));
    }
}
