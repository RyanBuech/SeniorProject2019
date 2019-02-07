﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }
    //layermask for hiding player and what not!!!!
    public LayerMask playerTargetMask;
    public LayerMask ObstacleMask;
    private bool Look(StateController controller)
    {
        bool retValue = false;
        Collider[] inViewRadius = Physics.OverlapSphere(controller.eyes.transform.position, controller.enemyStats.lookRadius,playerTargetMask);

        for (int i = 0; i< inViewRadius.Length; i++)
        {
            Transform target = inViewRadius[i].transform;
            Vector3 dirToTarget = (controller.gameManager.PlayerGO.transform.position - controller.eyes.transform.position).normalized;
            if(Vector3.Angle(controller.eyes.transform.forward, dirToTarget)< controller.enemyStats.fovAngle /2 )
            {
                float distToTarget = Vector3.Distance(controller.eyes.transform.position, target.position);
                if(!Physics.Raycast(controller.eyes.transform.position,dirToTarget,distToTarget,ObstacleMask))
                {
                    retValue = true;
                }
            }

        }

        return retValue;


    }
     
    public Vector3 DirFromAngel(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
       
     


   
