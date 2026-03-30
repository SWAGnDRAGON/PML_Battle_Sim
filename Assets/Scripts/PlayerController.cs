using UnityEditor.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PlayerController : CharacterController
{
    //Player Properties reference
    public PlayerHandler playerHandler;

    //Enemy Properties reference
    public EnemyHandler enemyHandler;

    //Animator component reference
    public Animator Player_Anim;

    //Attack Button reference
    public Button attackButton;

    //Parent object reference for Button UI
    public Transform buttonOptions;

    //Active Attack frame handler 
    bool activeAttackable = false;

    //Active Attack Input handler
    bool activeAttackInputFlag = false;

    //Active Attack Success handler
    bool activeAttackSuccessFlag = false;

    //Attack Action event
    private UnityAction attackAction;

    void Start()
    {
        //Initialize Attack Button
        attackAction += ToggleButtonUI;
        attackAction += Attack;

        attackButton.onClick.AddListener(attackAction);
    }

    void FixedUpdate()
    {
        //Active Attack Logic
        if (activeAttackable && activeAttackInputFlag == false && Player_Anim.GetBool("IsAttacking"))
        {
            if (InputSystem.actions["Interact"].IsPressed())
            {
                activeAttackInputFlag = true;
                activeAttackSuccessFlag = true;
            }
        }
        else if (!activeAttackable && activeAttackInputFlag == false && Player_Anim.GetBool("IsAttacking"))
        {
            if (InputSystem.actions["Interact"].IsPressed())
            {
                activeAttackInputFlag = true;
            }
        }

    }

    /// <summary>
    /// Toggles parent Button UI object activeSelf
    /// </summary>
    public void ToggleButtonUI()
    {
        foreach(Transform button in buttonOptions)
        {
            button.gameObject.SetActive(!button.gameObject.activeSelf);
        }
    }

    /// <summary>
    /// Animation Event function, begins Active Attack window
    /// </summary>
    public void EnableActiveAttackWindow()
    {
        if(activeAttackInputFlag == false)
            activeAttackable = true;
    }

    /// <summary>
    /// Animation Event function, ends Active Attack window
    /// </summary>
    public void DisableActiveAttackWindow()
    {
        activeAttackable = false;
        Player_Anim.SetBool("IsAttacking", false);
    }

    /// <summary>
    /// Animation Event function, Reset Active Attack Flags and Send Player back to start position.
    /// </summary>
    public void ResetActiveAttackInputFlag()
    {
        activeAttackInputFlag = false;
        activeAttackSuccessFlag = false;
        if(Player_Anim.GetBool("IsAttacking") == false)
        {
            Player_Anim.SetBool("ResetPos", true);
            ReturnCharacterToStart(startT.position);
        }
    }

    /// <summary>
    /// Animation Event function, Resets ResetPos to false during upon returning to Idle
    /// </summary>
    public void ResetPosToFalse()
    {
        anim.SetBool("ResetPos", false);
    }

    /// <summary>
    /// Animation Event function, toggles UI buttons back on after Player Idle begins
    /// </summary>
    public void ToggleUIButtonsOn()
    {
        foreach (Transform button in buttonOptions)
        {
            if(button.gameObject.activeSelf == false)
                button.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Animation Event function, Spawns damage number
    /// </summary>
    public override void DamageNumberTest()
    {
        int dmg = playerHandler.attack - enemyHandler.defense;
        if (activeAttackSuccessFlag == true)
        {
            dmg *= 2;
            FloatingNumberSpawner.Spawn(dmg, targetT.position, false, "physical", true);
        }
        else
            FloatingNumberSpawner.Spawn(dmg, targetT.position, false, "physical");

        enemyHandler.currentHealth -= dmg;
    }

    /// <summary>
    /// Animation Event function, triggers turn change.
    /// </summary>
    public void NextTurn()
    {
        battleManager.NextTurn();
    }
}
