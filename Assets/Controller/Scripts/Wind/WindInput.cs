using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WindMover))]
public class WindInput : MonoBehaviour
{

    [SerializeField] LayerMask raycastLayerMask;
    [SerializeField] float inputDelay = 0.5f;
    WindMover mover;
    private void Start()
    {
        mover = GetComponent<WindMover>();
    }

    void CastRay(Ray ray)
    {
        Debug.DrawRay(ray.origin, ray.direction * 30, Color.blue, 5f);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask);
        mover.CreateWindByPos(hit.point);
        readyForGetInput = false;
        StartCoroutine(InputDelay());
    }

    bool readyForGetInput = true;
    void Update()
    {
        if (readyForGetInput)
        {
            if (Input.touchCount > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                CastRay(ray);
            }


            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                CastRay(ray);
            }

        }
    }

    IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(inputDelay);
        readyForGetInput = true;
    }
}
