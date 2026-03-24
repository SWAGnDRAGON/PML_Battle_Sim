using UnityEditor.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PlayerController : MonoBehaviour
{
    //Player Properties reference
    public PlayerHandler playerHandler;

    //Animator component reference
    public Animator Player_Anim;

    //The speed of movement
    public float moveSpeed = 10f;

    //Safe overlap distance
    public float distanceBuffer = 0.4f;

    //Enemy transform reference
    public Transform enemyT;

    //Player Start transform reference
    public Transform startT;

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
    /// Trigger attack animation, only called by UnityAction attackAction
    /// </summary>
    public void Attack()
    {
        MoveCharacterSetDistance(enemyT.position);
        Player_Anim.SetBool("IsAttacking", true);
    }

    /// <summary>
    /// Move Character within set distanceBuffer of target position
    /// </summary>
    /// <param name="targetPos"></param>
    public void MoveCharacterSetDistance(Vector3 targetPos)
    {
        //Ensure only one movement coroutine runs at a time
        StopAllCoroutines();

        //Start movement coroutine
        StartCoroutine(MovementLoop(targetPos));
    }

    /// <summary>
    /// Calls Vector3.MoveTowards() to target position every frame until reaching the set distanceBuffer
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private IEnumerator MovementLoop(Vector3 targetPos)
    {
        //Loop until character is very close to the target position
        while (Vector3.Distance(transform.position, targetPos) > distanceBuffer)
        {
            //Move towards the target position each frame
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        Player_Anim.SetBool("ResetPos", false);
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
            MoveCharacterSetDistance(startT.position);
        }
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
    public void DamageNumberTest()
    {
        if (activeAttackSuccessFlag == true)
        {
            FloatingNumberSpawner.Spawn(playerHandler.attack*2, enemyT.position, false, "physical", true);
        }
        else
        FloatingNumberSpawner.Spawn(playerHandler.attack, enemyT.position, false, "physical");

    }
}
