using UnityEditor.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class EnemyController : CharacterController
{
    //Player Properties reference
    public EnemyHandler enemyHandler;

    //Attack event
    public UnityEvent attackEvent;

    //Attack Action event
    private UnityAction attackAction;

    void Start()
    {
        //Initialize Attack Button
        attackAction += Attack;

        attackEvent.AddListener(attackAction);
    }

    void FixedUpdate()
    {
        if(InputSystem.actions["Jump"].IsPressed())
        {
            attackEvent.Invoke();
        }
    }

    /// <summary>
    /// Animation Event function, Resets ResetPos to false during upon returning to Idle
    /// </summary>
    public void ResetTurnBools()
    {
        anim.SetBool("ResetPos", false);
    }

    /// <summary>
    /// Animation Event function, Resets ResetPos to false during upon returning to Idle
    /// </summary>
    public void ReturnEnemy()
    {
        anim.SetBool("IsAttacking", false);
        anim.SetBool("ResetPos", true);
        ReturnCharacterToStart(startT.position);
    }

    /// <summary>
    /// Animation Event function, Spawns damage number
    /// </summary>
    public override void DamageNumberTest()
    {
        FloatingNumberSpawner.Spawn(enemyHandler.attack, targetT.position, false, "physical", false);
        
    }
}
