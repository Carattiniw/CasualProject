using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftyAnimation : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField]private string currentAnimState;

    public const string solidIdle = "Solid_Idle";
    public const string solidBreakingBackToIdle = "Solid_Breaking_BackTo_Idle";
    public const string solidBreaking = "Solid_Breaking";
    public const string solidRunning = "Solid_Running";
    public const string solidIdleToRunningTransition = "SolidIdle_to_SolidRunning_Transition";
    public const string solidToLiquid = "Solid_To_Liquid";
    public const string solidToGas = "Solid_To_Gas";

    public const string liquidFalling = "Liquid_Falling";
    public const string liquidIdle = "Liquid_Idle";
    public const string liquidPipeSliding = "Liquid_Pipe_Sliding";
    public const string liquidPipeSlidingFromIdle = "Liquid_Pipe_Sliding_From_Idle";
    public const string liquidPipeSlidingToIdle = "Liquid_Pipe_Sliding_to_Idle";
    public const string liquidRunning = "Liquid_Running";
    public const string liquidSeepToLanding = "Liquid_Seep_to_Landing";
    public const string liquidSeepToLandingTransition = "Liquid_Seep_to_Landing_Transition";
    public const string liquidThroughCracks = "Liquid_Through_Cracks";
    public const string liquidToGas = "Liquid_to_Gas";
    public const string liquidToSolid = "Liquid_to_Solid";
    public const string liquidIdleToLiquidRunning = "Liquid_Idle_to_Liquid_Running";

    public const string gasIdle = "Gas_Idle";
    public const string gasPushPull = "Gas_Push_Pull";
    public const string gasPushPullFreezeFrame = "Gas_Push_Pull_FreezeFrame";
    public const string gasSeepingCracks = "Gas_Seeping_Cracks";
    public const string gasToLiquid = "Gas_to_Liquid";
    public const string gasToSolid = "Gas_to_Solid";


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimState == newState)
        {
            return;
        }
        else
        {
            currentAnimState = newState;
            animator.Play(newState);
            
        }
    }
}

