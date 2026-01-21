using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Player Components
    private Rigidbody2D m_rigidbody2D;
    private GatherInputs m_gatherInputs;
    private Transform m_transform;
    private Animator m_animator;

    [Header("Move and Jump settings")]
    [SerializeField] private float m_speed;
    private int direction = 1;
    [SerializeField] private float m_jumpForce;
    [SerializeField] private int extraJumps;
    [SerializeField] private int counterExtraJumps;
    private int idSpeed;

    [Header("Ground settings")]
    [SerializeField] private Transform lFoot;
    [SerializeField] private Transform rFoot;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask groundLayer;
    private int idIsGrounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_gatherInputs = GetComponent<GatherInputs>();
        m_transform = GetComponent<Transform>();
        m_animator = GetComponent<Animator>();
        idSpeed = Animator.StringToHash("Speed");
        idIsGrounded = Animator.StringToHash("IsGrounded");
        lFoot = GameObject.Find("LFoot").GetComponent<Transform>();
        rFoot = GameObject.Find("RFoot").GetComponent<Transform>();
        counterExtraJumps = extraJumps;
    }

    void Update()
    {
        setAnimatorValue();
    }

    private void setAnimatorValue()
    {
        m_animator.SetFloat(idSpeed, Mathf.Abs(m_gatherInputs.ValueX));
        m_animator.SetBool(idIsGrounded, isGrounded);
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        CheckGround();
    }

    private void Move()
    {
        Flip();
        m_rigidbody2D.linearVelocity = new Vector2(m_speed * m_gatherInputs.ValueX, m_rigidbody2D.linearVelocity.y);
    }

    private void Flip()
    {
        if (m_gatherInputs.ValueX * direction < 0)
        {
            direction *= -1;
            m_transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    private void Jump()
    {
        if (m_gatherInputs.IsJumping)
        {
            if (isGrounded)
            {
                m_rigidbody2D.linearVelocity = new Vector2(m_speed * m_gatherInputs.ValueX, m_jumpForce);
            }

            if (counterExtraJumps > 0)
            {
                m_rigidbody2D.linearVelocity = new Vector2(m_speed * m_gatherInputs.ValueX, m_jumpForce);
                counterExtraJumps--;
            }
        }

        m_gatherInputs.IsJumping = false;

    }

    private void CheckGround()
    {
        RaycastHit2D lFootRay = Physics2D.Raycast(lFoot.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rFootRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLength, groundLayer);

        if (lFootRay || rFootRay)
        {
            isGrounded = true;
            counterExtraJumps = extraJumps;
        }
        else
        {
            isGrounded = false;
        }
    }
}
