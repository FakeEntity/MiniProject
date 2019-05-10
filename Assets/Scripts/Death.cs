using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public string deathTag = "Enemy";
    public Rigidbody gibPrefab;
    Rigidbody gib;
    Rigidbody rb;
    public int minGibcount = 3;
    public int maxGibcount = 5;
    public float minCreatePosX = -1.0f;
    public float maxCreatePosX = 1.0f;
    public float minCreatePosY = 0.1f;
    public float maxCreatePosY = 1.0f;
    public float minCreatePosZ = -1.0f;
    public float maxCreatePosZ = 1.0f;
    public float minForceAddX = 0.1f;
    public float maxForceAddX = 1.0f;
    public float minForceAddY = 0.1f;
    public float maxForceAddY = 1.0f;
    public float minForceAddZ = 0.1f;
    public float maxForceAddZ = 1.0f;
    public float minForceUp = 0.1f;
    public float maxForceUp = 1.0f;
    int i;
    float xx, yy, zz;
    float dist, distx, disty, distz;
    float forcex;
    float forcey;
    float forcez;
    float forceup;
    Vector3 dir;
    Vector3 pos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        i = Random.Range(minGibcount, maxGibcount+1);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == deathTag)
        {
            while (i > 0)
            {
                xx = rb.transform.position.x + Random.Range(minCreatePosX, maxCreatePosX);
                yy = rb.transform.position.y + Random.Range(minCreatePosY, maxCreatePosY);
                zz = rb.transform.position.z + Random.Range(minCreatePosZ, maxCreatePosZ);
                forceup = Random.Range(minForceUp, maxForceUp);
                pos.Set(xx, yy, zz);
                distx = Mathf.Abs(xx - transform.position.x);
                disty = Mathf.Abs(yy - transform.position.y);
                distz = Mathf.Abs(zz - transform.position.z);
                forcex = Random.Range(minForceAddX, maxForceAddX) / distx;
                forcey = Random.Range(minForceAddY, maxForceAddY) / disty;
                forcez = Random.Range(minForceAddY, maxForceAddY) / distz;
                dir.Set((xx - rb.transform.position.x) * forcex, (yy - rb.transform.position.y+forceup) * forcey, (zz - rb.transform.position.z) * forcez);
                gib = Instantiate(gibPrefab, pos, Quaternion.identity);
                gib.AddForce(dir+rb.velocity);
                i--;
            }
            Destroy(gameObject);
        }

    }
}
