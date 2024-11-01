using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles player input in <see cref="OnMove(UnityEngine.InputSystem.InputValue)"/>
/// to move the character through applying a velocity to <see cref="rigidbody"/>
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <remarks>
    /// The "new" keyword is here to prevent a warning. It is optional.
    /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier">See the documentation</see>
    /// for more info
    /// </remarks>
    [SerializeField] new Rigidbody2D rigidbody;

    Entity entity;

    Vector2 movementDir = Vector2.zero;

    void Start(){
        entity = GetComponent<Entity>();
    }

    /// <summary>
    /// <para>
    /// This handles the "OnMove" message sent by <see cref="PlayerInput"/>
    /// when the player presses WASD (or whatever is defined in the <see cref="InputActionAsset">Controls</see>).
    /// The <paramref name="inputValue"/> contains the movement direction.
    /// </para>
    /// <para>
    /// The function changes the velocity of the <see cref="rigidbody"/> to point in the direction of the input
    /// at speed <see cref="MovementSpeed"/>.
    /// </para>
    /// </summary>
    public void OnMove(InputValue inputValue)
    {
        movementDir = inputValue.Get<Vector2>();
    }

    void FixedUpdate(){
        rigidbody.velocity = movementDir * entity.stats.MovementSpeed;
    }
}
