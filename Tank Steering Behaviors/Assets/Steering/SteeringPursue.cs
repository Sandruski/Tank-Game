﻿using UnityEngine;
using System.Collections;

public class SteeringPursue : MonoBehaviour
{
    public float max_prediction = 1.0f;

	Move move;
	SteeringArrive arrive;

	void Start()
    {
		move = GetComponent<Move>();
		arrive = GetComponent<SteeringArrive>();
	}
	
	void Update() 
	{
		Steer(move.target.transform.position, move.target.GetComponent<Move>().movement);
	}

	public void Steer(Vector3 target, Vector3 velocity)
	{
        // TODO 6: Create a fake position to represent
        // enemies predicted movement. Then call Steer()
        // on our Steering Arrive

        Vector3 predictedPosition = Vector3.zero;

        // Prediction 1 (simple)
        predictedPosition = target + velocity * max_prediction;

        // Prediction 2 (improved. The acceleration decreases depending on how close the target is)
        float distanceToTarget = Vector3.Distance(transform.position, target);
        float prediction = distanceToTarget / max_prediction;
        predictedPosition = target + velocity * prediction;

        arrive.Steer(predictedPosition);
	}
}
