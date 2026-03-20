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

    //Bool for pause demo
    public bool pause = false;

    //Attack Button reference
    public Button attackButton;

    public Transform buttonOptions;

    //Attack Action event
    private UnityAction attackAction;

    void Start()
    {
        attackAction += toggleButtonUI;
        attackAction += attack;

        attackButton.onClick.AddListener(attackAction);
    }

    void toggleButtonUI()
    {
        //attackButton.gameObject.SetActive(false);
        foreach(Transform button in buttonOptions)
        {
            button.gameObject.SetActive(!button.gameObject.activeSelf);
        }
    }

    void attack()
    {
        MoveCharacterSetDistance(enemyT.position);
        Player_Anim.SetBool("IsDashing", !Player_Anim.GetBool("IsDashing"));
        Player_Anim.SetBool("IsAttacking", !Player_Anim.GetBool("IsAttacking"));
    }

    void Update()
    {
        if (InputSystem.actions["Jump"].WasPressedThisFrame())
        {
            pause = !pause;
            if(pause)
            {
                Player_Anim.speed = 0f;
            }
            else
            {
                Player_Anim.speed = 1f;
            }
        }

        if (InputSystem.actions["Interact"].WasPressedThisFrame())
        {
            MoveCharacterSetDistance(enemyT.position);
            Player_Anim.SetBool("IsDashing", !Player_Anim.GetBool("IsDashing"));
            Player_Anim.SetBool("IsAttacking", !Player_Anim.GetBool("IsAttacking"));
        }
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
    }
}
