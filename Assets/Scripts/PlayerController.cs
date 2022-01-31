using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerNav))]
public class PlayerController : MonoBehaviour
{
    Camera camera_main;
    PlayerNav navigator;
    int layermask = 1 << 7;

    void Start()
    {
        camera_main = Camera.main;
        navigator = GetComponent<PlayerNav>();
    }

    void Update()
    {
        // Player Left Clicks
        // Not sure yet
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = camera_main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {

            }

        }

        // Player Right Clicks
        // Move player to clicked tile
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = camera_main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layermask)) {
                UnityEngine.Debug.Log("Player clicked: " +  hit.collider.name + " " + hit.point);
                UnityEngine.Debug.Log("Moving player to point: " +  hit.collider.name + " " + hit.collider.transform.position);
                navigator.navigate(hit.collider.transform.position);
            }

        }
    }

}
