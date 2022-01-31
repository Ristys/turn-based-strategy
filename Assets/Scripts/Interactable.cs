using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Distance to object
    public float radius = 3f;
    bool isFocused = false;
    Transform player;

    public Transform interactableTransform;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        if (isFocused && !hasInteracted) {
            float distance = Vector3.Distance(player.position, interactableTransform.position);
            if (distance <= radius) {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocused = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected() 
    {
		if (interactableTransform == null)
			interactableTransform = transform;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactableTransform.position, radius);
    }
}
