﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour {

    //NECESITO HACER RAYCAST DE LA POSICION DEL MOUSE Y DETECTAR LA LAYER.
    //POSICION DEL MOUSE A POSICION DEL MUNDO

    Camera playerCamera;
    public LayerMask layerMask;
    float maxDistance = 100f;

    CharacterBehaviour playerBehaviour;
    Vector3 destination;
    Vector3 hitPosition;

    bool enemyWasHit = false;
    Transform enemyTransform = null;

	void Start () {
        playerBehaviour = GameObject.Find("Player").GetComponent<CharacterBehaviour>();
        playerCamera = this.GetComponent<Camera>();

        destination = playerBehaviour.gameObject.transform.position;
	}

    private void Update()
    {

        if (enemyWasHit)
        {
            if(destination != enemyTransform.position)
            {
                destination = enemyTransform.position;
                playerBehaviour.SetDestination(destination);
            }
        }
        else
        {
            if(destination != hitPosition)
            {
                destination = hitPosition;
                playerBehaviour.SetDestination(destination);
            }
        }
    }

    public void CastRay()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit, maxDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //player target pos = hit.transform.gameobject.pos;
                //It has to follow the enemy

                //Player should have a Target, if the ray hits enemy, follow enemy, if it hits the ground, go to the ground point.

                Debug.Log(hit.transform.name);

                enemyWasHit = true;
                enemyTransform = hit.transform;
                hitPosition = Vector3.zero;
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Debug.Log(hit.transform.name);

                enemyWasHit = false;
                hitPosition = hit.point;
                enemyTransform = null;
            }
        }
    }
}
