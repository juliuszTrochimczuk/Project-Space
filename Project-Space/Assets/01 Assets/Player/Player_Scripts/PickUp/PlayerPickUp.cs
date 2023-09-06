using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using Zenject;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject TextPodnies;
    [SerializeField] private GameObject TextRzuc;

    private bool inRange = false;
    private bool holding = false;

    private Transform heldObject;

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        UI.SetActive(true);
        if (!holding)
            TextPodnies.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;   
        if(!holding)
        {
            UI.SetActive(false);
        }
    }

    private void Start()
    {
        inputController.inputActions.MovementMap.PickUp.started += _ => PickUp();
    }

    private void PickUp()
    {
        if (inRange && !holding)
        {
            Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
            float rayLength = 500f;

            // actual Ray
            Ray ray = cam.ViewportPointToRay(rayOrigin);

            // debug Ray
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                if (hit.transform.gameObject.tag == "Pickable")
                {
                    holding = true;
                    TextPodnies.SetActive(false);
                    TextRzuc.SetActive(true);
                    UI.SetActive(true);
                    heldObject = hit.transform;
                    hit.transform.parent = cam.transform;
                    hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                    hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1f, hit.transform.position.z);
                }
            }
        }
        else if(holding)
        {
            holding = false;
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.parent = null;
            TextPodnies.SetActive(true);
            TextRzuc.SetActive(false);
        }
    }

}
