using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;
    private bool slicing;

    public Vector3 direction {  get; private set; }
    public float minSliceVelocity = 0.01f;
    public float sliceForce = 5f;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            StartSlicing();
        } else if (Input.GetMouseButtonUp(0)) { 
            StopSlicing();
        }  else if (slicing) {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 newposition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newposition.z = 0f;
        transform.position = newposition;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newposition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newposition.z = 0f;

        direction = newposition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newposition;
    }
}
