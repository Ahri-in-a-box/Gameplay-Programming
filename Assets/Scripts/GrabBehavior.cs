using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBehavior : MonoBehaviour
{
    private Vector3 oldPos;
    private Quaternion oldRot;

    // Start is called before the first frame update
    void Start()
    {
        oldPos = gameObject.transform.position;
        oldRot = gameObject.transform.rotation;
    }

    public void OnPicked()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void OnDrop()
    {
        gameObject.SetActive(false);
        gameObject.transform.SetPositionAndRotation(oldPos, oldRot);
        gameObject.SetActive(true);
    }
}
