using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {

    }
    public float reyDistance = 1f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            if (Physics.Raycast(this.transform.position + transform.up * 0.5f, transform.forward * reyDistance, out hit))
            {
                Debug.Log("Osuttiin objetiin: " + hit.transform.name);

                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OnEnterInteractable>())
        {
            other.GetComponent<OnEnterInteractable>().OnEnterInteract();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<OnEnterInteractable>())
        {
            other.GetComponent<OnEnterInteractable>().OnExitInteract();
        }
    }



}
