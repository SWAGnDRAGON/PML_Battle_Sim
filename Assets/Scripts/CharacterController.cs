using UnityEditor.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class CharacterController : MonoBehaviour
{
    //Animator component reference
    public Animator anim;

    //The speed of movement
    public float moveSpeed = 10f;

    //Safe overlap distance
    public float distanceBuffer = 0.4f;

    //Target transform reference
    public Transform targetT;

    //Player Start transform reference
    public Transform startT;

    /// <summary>
    /// Trigger attack animation, only called by UnityAction attackAction
    /// </summary>
    public void Attack()
    {
        MoveCharacterSetDistance(targetT.position);
        anim.SetBool("IsAttacking", true);
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
        anim.SetBool("ResetPos", false);
    }

    /// <summary>
    /// Move Character within set distanceBuffer of target position
    /// </summary>
    /// <param name="targetPos"></param>
    public void ReturnCharacterToStart(Vector3 targetPos)
    {
        //Ensure only one movement coroutine runs at a time
        StopAllCoroutines();

        //Start movement coroutine
        StartCoroutine(MovementLoopReturn(targetPos));
    }

    /// <summary>
    /// Calls Vector3.MoveTowards() to target position every frame until reaching the set distanceBuffer
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private IEnumerator MovementLoopReturn(Vector3 targetPos)
    {
        //Loop until character is very close to the target position
        while (Vector3.Distance(transform.position, targetPos) >= 0f)
        {
            //Move towards the target position each frame
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        anim.SetBool("ResetPos", false);
    }

    /// <summary>
    /// Animation Event function, Spawns damage number
    /// </summary>
    public virtual void DamageNumberTest()
    {
        FloatingNumberSpawner.Spawn(1, targetT.position, false, "physical");
    }
}
