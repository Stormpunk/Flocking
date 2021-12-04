using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehaviour
{
    public FlockBehaviour[] behaviours;
    public float[] weights;

    public override Vector2 CalculateMove(FlockingAgent agent, List<Transform> context, Flock flock)
    {
        if(weights.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch with " + name, this);
            return Vector2.zero;
        }
        Vector2 move = Vector2.zero;
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, flock)* weights[i];
            if(partialMove != Vector2.zero)
            {
                if(partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                move += partialMove;
            }
        }

        return move;
    }
}
