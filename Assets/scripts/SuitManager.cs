using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitManager : MonoBehaviour
{

    [SerializeField] GameObject chakrasObj;
    [SerializeField] Rigidbody caseCover;
    [SerializeField] GameObject boomEfx;

    private void Start()
    {
        chakrasObj.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.AllChakrasCollected)
        {
            GameObject Collectfx = Instantiate(boomEfx, caseCover.transform.position, Quaternion.identity) as GameObject;
            Destroy(Collectfx, 2f);
            caseCover.useGravity = true;
            caseCover.isKinematic = false;
            caseCover.AddForce(Vector3.up*2000,ForceMode.Force);
            chakrasObj.SetActive(true);
            Debug.Log("Boom");
        }
    }
} 
