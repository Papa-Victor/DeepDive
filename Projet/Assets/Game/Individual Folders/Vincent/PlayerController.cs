using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private State<PlayerController> currentState;
    private Rigidbody2D rb2d;
    private Collider2D mainCollider;
    [SerializeField]
    private List<Transform> floorDetectors;
    private Transform collidingLadder;
    private ContactFilter2D floorDetectionFilter;
    private AudioSource Audio;
    private Animator Anim;

    private bool isGrounded;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float horizontalDeceleration;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float ladderSpeed;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = RegularPlayerInputState.GetInstance();
        currentState.EnterState(this);
        rb2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();
        floorDetectionFilter = new ContactFilter2D();
        floorDetectionFilter.layerMask = ~LayerMask.GetMask("Player");
        floorDetectionFilter.useLayerMask = true;
        floorDetectionFilter.useTriggers = false;
        collidingLadder = null;

        Audio = GameObject.FindGameObjectWithTag("Audio Jump").GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.Execute(this);
        }
        CheckFloorCollision();

        if (rb2d.velocity.x > 1.0f)
        {
            Anim.SetBool("Idle", false);
            Anim.SetFloat("Velocity", 1.0f);
        }
        else if (rb2d.velocity.x < -1.0f)
        {
            Anim.SetBool("Idle", false);
            Anim.SetFloat("Velocity", -1.0f);
        }
        else
        {
            Anim.SetBool("Idle", true);
            Anim.SetFloat("Velocity", 0.0f);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
            Audio.Play();
        }
    }

    public void HorizontalMovement(float value)
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        if (mainCollider.Cast(Vector2.right * value, floorDetectionFilter, hits, 0.05f) > 0)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        else if (value == 0 && rb2d.velocity.x != 0)
        {
            float velocityX = rb2d.velocity.x;
            int decelerationDirection = (int)(velocityX / Mathf.Abs(velocityX)) * -1;
            rb2d.velocity += Vector2.right * decelerationDirection * horizontalDeceleration;
        }
        else
        {
            rb2d.velocity = new Vector2(speed * value, rb2d.velocity.y);
        }
    }

    #region Ladder
    public void MoveUpLadder(float value)
    {
        rb2d.velocity = new Vector2(0, value * ladderSpeed);
    }

    public void GetOnLadder()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerOnLadder");
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 0;
        rb2d.position = new Vector2(collidingLadder.position.x, rb2d.position.y);
    }

    public void GetOffLadder()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 1;
    }

    public bool CanGetOffLadder(float value)
    {
        RaycastHit2D[] hitResults = new RaycastHit2D[1];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = false;
        int hits = mainCollider.Cast(new Vector2(value, 0), contactFilter, hitResults, 0.1f);
        if (hits > 0) return false;
        return true;
    }
    #endregion

    public void SuspendActivity()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 0;
    }

    public void StopVelocity()
    {
        rb2d.velocity = Vector2.zero;
    }

    public void ResumeActivity()
    {
        rb2d.gravityScale = 1;
    }

    public void ChangeState(State<PlayerController> newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    #region Collision

    public void CheckFloorCollision()
    {
        isGrounded = false;
        foreach (Transform t in floorDetectors)
        {
            Vector2 position2D = new Vector2(t.position.x, t.position.y);
            RaycastHit2D[] hits = new RaycastHit2D[10];
            int numHits = Physics2D.Raycast(position2D, Vector2.down, floorDetectionFilter, hits, 0.02f);
            if (numHits > 0)
            {
                isGrounded = true;

                Anim.SetBool("Airborne", false);

                return;
            }
            else
            {
                isGrounded = false;
            }
        }
        Anim.SetBool("Airborne", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            collidingLadder = collision.transform;
            Anim.SetBool("Ladder", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            Anim.SetBool("Ladder", false);
            collidingLadder = null;
            if (currentState == LadderPlayerInputState.GetInstance())
            {
                ChangeState(RegularPlayerInputState.GetInstance());
            }
        }
    }
    #endregion

    #region Get/Set

    public Transform GetInsideLadder() { return collidingLadder; }

    #endregion
}