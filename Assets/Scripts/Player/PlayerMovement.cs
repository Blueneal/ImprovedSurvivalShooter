using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool onCooldown;
    public bool isMoving;
    public Slider staminaSlider;

    private int floorMask;
    private float speed;
    private float normalSpeed = 6f;
    private float sprintSpeed = 10f;
    private float startingStamina = 100f;
    private float currentStamina;
    private float camRayLength = 100f;
    private float timer = 10;
    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;

    void Awake()
    {
        floorMask = LayerMask.GetMask ("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        onCooldown = false;
        currentStamina = startingStamina;
        speed = normalSpeed;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw ("Horizontal");
        float v = Input.GetAxisRaw ("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);

        if (Input.GetKey(KeyCode.LeftShift) && onCooldown == false && isMoving == true)
        {
            StartCoroutine(PlayerSprinting());
        }
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
        bool walking = h != 0f || v != 0f;
        if (walking = h != 0f || v != 0f)
        {
            isMoving = true;
        }
        else
        {
            return;
        }
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation (newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("isWalking", walking);
    }

    //Speeds the player up for 10 seconds, then resets the player back to original speed
    private IEnumerator PlayerSprinting()
    {
        for (int i = 0; i < timer; i++)
        {
            speed = sprintSpeed;
            yield return new WaitForSeconds(1f);
            currentStamina -= 5f;
            staminaSlider.value = currentStamina;
            i++;
        }
        speed = normalSpeed;
        onCooldown = true;
        StartCoroutine(CoolDown());
    }

    //Adds an additional 10 second cooldown so the player cant sprint all the time
    private IEnumerator CoolDown()
    {
        for (int i = 0; i < timer; i++)
        { 
            yield return new WaitForSeconds(1f);
            currentStamina += 5f;
            staminaSlider.value = currentStamina;
            i++;
        }
        onCooldown = false;
    }
}
