﻿using UnityEngine;
using System.Collections;

public class SteeringArrive : MonoBehaviour
{
	public float min_distance = 0.1f;
	public float slow_distance = 5.0f;
	public float time_to_target = 0.1f;

	Move move;

	void Start()
    { 
		move = GetComponent<Move>();
	}

	void Update() 
	{
		Steer(move.target.transform.position);
	}

	public void Steer(Vector3 target)
	{
        if (!move)
            move = GetComponent<Move>();

        // TODO 3: Create a vector to calculate our ideal velocity
        // then calculate the acceleration needed to match that velocity
        // before sending it to move.AccelerateMovement() clamp it to 
        // move.max_mov_acceleration

        Vector3 currVel = target - transform.position;
        float distanceToTarget = currVel.magnitude;

        currVel.Normalize();
        currVel *= move.max_mov_acceleration;

        Vector3 newAcceleration = currVel;

        if (distanceToTarget < slow_distance)
        {
            Vector3 idealVel = currVel.normalized * distanceToTarget * time_to_target;

            if (distanceToTarget < min_distance)
                idealVel = Vector3.zero;

            newAcceleration = idealVel - move.movement;
        }

        newAcceleration.y = 0;

        move.AccelerateMovement(newAcceleration);
    }

	void OnDrawGizmosSelected()
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, slow_distance);
	}
}