using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed = 10f;
    [SerializeField] float gravity = -14f;
    [SerializeField] int playerHealth = 100;
    private Vector3 _gravityVector;

    //GroundCheck
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] float groundCheckRadius = 0.35f;
    [SerializeField] LayerMask groundLayer;
    //GroundCheck end
    public bool isGrounded = false;

    public float jumpSpeed = 3f;

    //UI
    public Slider healthSlider;
    public Text healthText;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
        GroundChecker();
        JumpAndGravity();
    }

    void MovePlayer()
    {
        Vector3 moveVector = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;

        characterController.Move(moveVector * speed * Time.deltaTime);
    }

    void GroundChecker()
    {
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }

    void JumpAndGravity()
    {
        _gravityVector.y += gravity * Time.deltaTime;

        characterController.Move(_gravityVector * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);


        if (isGrounded && _gravityVector.y <= 0)
        {
            _gravityVector.y = -3f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _gravityVector.y = Mathf.Sqrt(jumpSpeed * -2f * gravity); ;
        }
    }

    public void PlayerTakeDamage(int damageAmount)
    {
        playerHealth -= damageAmount;
        healthSlider.value -= damageAmount;
        HealthTextUpdate();

        if (playerHealth <= 0)
        {
            PlayerDeath();
            HealthTextUpdate();
             
            healthSlider.value = 0;
        }
    }

    private void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void HealthTextUpdate()
    {
        healthText.text = playerHealth.ToString();
    }
}
