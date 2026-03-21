using UnityEditor.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //Animator component reference
    public Animator Player_Anim;

    //The speed of movement
    public float moveSpeed = 10f;

    //Safe overlap distance
    public float distanceBuffer = 0.5f;

    //Enemy transform reference
    public Transform enemyT;

    //Attack Button reference
    public Button attackButton;

    //Parent object reference for Button UI
    public Transform buttonOptions;

    //Active Attack frame handler 
    bool activeAttackable = false;

    //Active Attack Input handler
    bool activeAttackInputFlag = false;

    //Attack Action event
    private UnityAction attackAction;

    void Start()
    {
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
                DisableActiveAttackWindow();
                activeAttackInputFlag = true;
            }
        }
        else if (!activeAttackable && activeAttackInputFlag == false)
        {
            if (InputSystem.actions["Interact"].IsPressed())
            {
                Debug.Log("Active Attack: FAILURE");
                activeAttackInputFlag = true;
                Player_Anim.SetBool("IsAttacking", !Player_Anim.GetBool("IsAttacking"));
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
        Player_Anim.SetBool("IsDashing", !Player_Anim.GetBool("IsDashing"));
        Player_Anim.SetBool("IsAttacking", !Player_Anim.GetBool("IsAttacking"));
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
        //Trigger attack animation transition upon reaching the target
    }

    public void EnableActiveAttackWindow()
    {
        if(activeAttackInputFlag == false)
            activeAttackable = true;
    }

    public void DisableActiveAttackWindow()
    {
        activeAttackable = false;
    }

    public void ResetActiveAttackInputFlag()
    {
        activeAttackInputFlag = false;
    }
}
