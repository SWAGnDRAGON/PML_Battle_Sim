using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleManager : MonoBehaviour
{
    //Turn Order Manager
    public Queue<GameObject> turnOrderList = new();

    public GameObject Player;

    public List<GameObject> Enemies;

    public CinemachineCamera cam;

    public CinemachineRotationComposer rotationComposer;

    public bool playerTurn = true;

    //Parent object reference for Button UI
    public Transform buttonOptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnOrderList.Enqueue(Player);

        for (int i = 0; i < Enemies.Count; i++)
        {
            turnOrderList.Enqueue(Enemies[i]);
        }

        FocusCameraOnCurrentTurnCharacter();
    }

    public void NextTurn()
    {
        //Add current turn character to back of queue
        turnOrderList.Enqueue(turnOrderList.Peek());

        //Remove current turn character from front of queue
        turnOrderList.Dequeue();

        //Move camera focus to current turn character
        FocusCameraOnCurrentTurnCharacter();

        if (turnOrderList.Peek() == Player)
            playerTurn = true;
        else
            playerTurn = false;

        //UI Handler
        if (playerTurn)
        {
            ToggleUIButtonsOn();
            rotationComposer.TargetOffset.x *= -1;
        }
        else
        {
            ToggleUIButtonsOff();
            rotationComposer.TargetOffset.x *= -1;
        }
            
    }

    void FocusCameraOnCurrentTurnCharacter()
    {
        cam.Follow = turnOrderList.Peek().transform;
        //cam.LookAt = turnOrderList.Peek().transform;
    }

    /// <summary>
    /// Toggles UI buttons on
    /// </summary>
    public void ToggleUIButtonsOn()
    {
        foreach (Transform button in buttonOptions)
        {
            if (button.gameObject.activeSelf == false)
                button.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Toggles UI buttons off 
    /// </summary>
    public void ToggleUIButtonsOff()
    {
        foreach (Transform button in buttonOptions)
        {
            if (button.gameObject.activeSelf == true)
                button.gameObject.SetActive(false);
        }
    }
}
