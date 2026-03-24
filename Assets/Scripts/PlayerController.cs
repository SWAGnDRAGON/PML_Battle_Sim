using UnityEditor.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using static StaticData;
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
        if (activeAttackable && activeAttackInputFlag == false)
        {
            if (InputSystem.actions["Interact"].IsPressed())
            {
                Debug.Log("Active Attack: SUCCESS");
                activeAttackInputFlag = true;
                activeAttackSuccessFlag = true;
            }
        }
        else if (!activeAttackable && activeAttackInputFlag == false)
        {
            if (InputSystem.actions["Interact"].IsPressed())
            {
                Debug.Log("Active Attack: FAILURE");
                activeAttackInputFlag = true;
            }
        }
    }

    /// <summary>
    /// Toggles parent Button UI object activeSelf
    /// </summary>
    void ToggleButtonUI()
    {
        foreach(Transform button in buttonOptions)
        {
            button.gameObject.SetActive(!button.gameObject.activeSelf);
        }
    }

    /// <summary>
    /// Trigger attack animation, only called by UnityAction attackAction
    /// </summary>
    void Attack()
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
        if(activeAttackInputFlag == false)
        {
            Debug.Log("Active Attack: FAILURE BY DEFAULT");
        }
        Player_Anim.SetBool("IsAttacking", false);
    }

    public void ResetActiveAttackInputFlag()
    {
        activeAttackInputFlag = false;
        activeAttackSuccessFlag = false;
        if(Player_Anim.GetBool("IsAttacking") == false)
        {
            Player_Anim.SetBool("ResetPos", true);
            MoveCharacterSetDistance(startT.position);
            ToggleButtonUI();
        }
    }

    public void DamageNumberTest()
    {
        
        Array dmgTypes = Enum.GetValues(typeof(DamageType));


        if (activeAttackSuccessFlag == true)
        {
            FloatingNumberSpawner.Spawn(playerHandler.attack*2, enemyT.position, false, "physical", true);
        }
        else
        FloatingNumberSpawner.Spawn(playerHandler.attack, enemyT.position, false, "physical");

    }
}
