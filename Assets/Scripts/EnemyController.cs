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
    //Enemy Properties reference
    public EnemyHandler enemyHandler;

    //Player Properties reference
    public PlayerHandler playerHandler;

    //Enemy Animator reference
    public Animator Enemy_Anim;

    //Attack event
    public UnityEvent attackEvent;

    //Attack Action event
    private UnityAction attackAction;

    //Attack Parry frame handler 
    bool isParryable = false;

    //Attack Parry Input handler
    bool parryInputFlag = false;

    //Attack Parry Success handler
    bool parrySuccessFlag = false;

    //Enemy Attack Handler
    bool canAttack = true;

    void Start()
    {
        //Initialize Attack functionality
        attackAction += Attack;

        attackEvent.AddListener(attackAction);
    }

    void FixedUpdate()
    {
        //Checks if its the Enemy Turn
        if(this.battleManager.turnOrderList.Peek() == this.gameObject && canAttack)
        {
            canAttack = false;
            attackEvent.Invoke();
        }

        //Parry Window Logic
        if (isParryable && parryInputFlag == false && Enemy_Anim.GetBool("IsAttacking"))
        {
            if (InputSystem.actions["Interact"].IsPressed())
            {
                parryInputFlag = true;
                parrySuccessFlag = true;
            }
        }
        else if (!isParryable && parryInputFlag == false && Enemy_Anim.GetBool("IsAttacking"))
        {
            if (InputSystem.actions["Interact"].IsPressed())
            {
                parryInputFlag = true;
            }
        }
    }


    /// <summary>
    /// Animation Event function, begins Attack Parry window
    /// </summary>
    public void EnableParryWindow()
    {
        if (parryInputFlag == false)
            isParryable = true;
    }

    /// <summary>
    /// Animation Event function, ends Attack Parry window
    /// </summary>
    public void DisableParryWindow()
    {
        isParryable = false;
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

        //Reset logic params
        parryInputFlag = false;
        parrySuccessFlag = false;
    }

    /// <summary>
    /// Animation Event function, Spawns damage number
    /// </summary>
    public override void DamageNumberTest()
    {
        int dmg = enemyHandler.attack - playerHandler.defense;

        if (parrySuccessFlag)
        {
            dmg = 0;
            FloatingNumberSpawner.Spawn(dmg, targetT.position, false, "physical", true);
        }
        else
            FloatingNumberSpawner.Spawn(dmg, targetT.position, false, "physical", false);

        playerHandler.currentHealth -= dmg;
    }

    /// <summary>
    /// Triggers turn change
    /// </summary>
    public void NextTurn()
    {
        battleManager.NextTurn();
        canAttack = true;
    }
}
