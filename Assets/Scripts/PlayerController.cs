using UnityEditor.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Animator Player_Anim;
    public bool pause = false;

    void Update()
    {
        if(InputSystem.actions["Attack"].WasPressedThisFrame())
        {
            Player_Anim.SetBool("IsDashing", !Player_Anim.GetBool("IsDashing"));
        }

        if (InputSystem.actions["Interact"].WasPressedThisFrame())
        {
            Player_Anim.SetBool("IsAttacking", !Player_Anim.GetBool("IsAttacking"));
        }

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
    }
}
