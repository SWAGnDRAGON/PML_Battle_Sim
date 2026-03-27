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

    //Attack event
    public UnityEvent attackEvent;

    //Attack Action event
    private UnityAction attackAction;

    bool canAttack = true;

    void Start()
    {
        //Initialize Attack functionality
        attackAction += Attack;

        attackEvent.AddListener(attackAction);
    }

    void FixedUpdate()
    {
        if(this.battleManager.turnOrderList.Peek() == this.gameObject && canAttack)
        {
            canAttack = false;
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
        playerHandler.currentHealth -= (enemyHandler.attack - playerHandler.defense);
    }

    public void NextTurn()
    {
        battleManager.NextTurn();
        canAttack = true;
    }
}
