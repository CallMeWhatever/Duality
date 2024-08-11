using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask portalLayer;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private float TimeLimit;
    [SerializeField] private float TimeBonus;
    [SerializeField] private float jumpCooldownTime;
    [SerializeField] private float portalBoost;
    [SerializeField] private float damageRecoilModifyer;
    public float elapsedTime;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private MusicManager musicManager;
    private bool flipCooldwonActive = false;
    private float cooldown;
    private float TimeBonusCooldown;
    private float worldUp = 1;
    private bool Doublejump;
    private float jumpCooldown;
    public bool enable;

    private Animator anim;
    


    private void Awake(){
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        musicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>(); 
        elapsedTime = TimeLimit;
        TimeBonusCooldown = -1;
        jumpCooldown = -1;
        enable = true;
        
    }

    private void Start(){
        musicManager.ChangeMusic("up");
    }
    private void Update(){
        //while(!enable){}
        if(enable){
        
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed ,body.velocity.y);

        if (horizontalInput > 0.01f){
            body.transform.localScale = new Vector3(0.2f,0.2f * worldUp,0.2f);
        }
        else if(horizontalInput < -0.01f){
            body.transform.localScale = new Vector3(-0.2f,0.2f * worldUp,0.2f);
        }

        bool grounded = isGrounded();
        if (grounded && !Input.GetKey(KeyCode.Space)){
            Doublejump = false;
        }
        if(Input.GetKey(KeyCode.Space) && (grounded || Doublejump) && (jumpCooldown <= 0)){
            Doublejump = !Doublejump;
            jumpCooldown = jumpCooldownTime;
            Jump();
        }
        if((Input.GetKey(KeyCode.X) && !flipCooldwonActive) || touchesPortal()){
            FlipWorld();
            flipCooldwonActive = true;
            cooldown = 1;
        }
        touchesItem(); //Function to detect item collisions and applies effects

        if (TimeBonusCooldown >= 0){
            TimeBonusCooldown -= Time.deltaTime;
        }
        
        if (jumpCooldown > 0){ jumpCooldown -= Time.deltaTime*10; }

        if (cooldown <= 0){flipCooldwonActive = false;}
        
        cooldown -= Time.deltaTime;

        anim.SetBool("Idle", (horizontalInput == 0) && grounded);
    }
    }

    private void Jump(){
        body.velocity = new Vector2(body.velocity.x, jumpSpeed * worldUp);
    }
    public void bounce(float bouncyness){
        body.velocity = new Vector2(body.velocity.x, bouncyness * worldUp);
        Doublejump = true;
    }

    public void Damage_Recoil(float _damage){
        body.velocity = new Vector2(-body.velocity.x * _damage * damageRecoilModifyer, _damage * damageRecoilModifyer * worldUp);
        Doublejump = true;
    }

    private void OnCollisionEnter2D(Collision2D Collision){
    }

    private bool isGrounded(){
        RaycastHit2D raycastHit = (worldUp > 0) ? Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer)
        : Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.up, 0.1f, groundLayer);

        return raycastHit.collider != null;
    }
    private bool touchesWall(){
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, groundLayer);
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, groundLayer);
        return (raycastHitRight.collider != null) || (raycastHitLeft.collider != null) ;
    }
    private bool touchesPortal(){
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, portalLayer);
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, portalLayer);
        RaycastHit2D raycastHitUp = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.up, 0.1f, portalLayer);
        RaycastHit2D raycastHitDown = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, portalLayer);
        
        return (raycastHitRight.collider != null) || (raycastHitLeft.collider != null) || (raycastHitUp.collider != null) || (raycastHitDown.collider != null);
    }
    private void touchesItem()
    {
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, itemLayer);
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, itemLayer);
        RaycastHit2D raycastHitUp = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.up, 0.1f, itemLayer);
        RaycastHit2D raycastHitDown = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, itemLayer);
        if (TimeBonusCooldown < 0){
            if (raycastHitRight.collider != null)
            {
                if (raycastHitRight.collider.gameObject.CompareTag("TimeBonus"))
                {
                    elapsedTime += TimeBonus;
                    TimeBonusCooldown = 2.0f;
                    Destroy (raycastHitRight.collider.gameObject);
                }
            }
            else if (raycastHitLeft.collider != null)
            {
                if (raycastHitLeft.collider.gameObject.CompareTag("TimeBonus"))
                {
                    elapsedTime += TimeBonus;
                    TimeBonusCooldown = 2.0f;
                    Destroy (raycastHitLeft.collider.gameObject);
                }
            }
            else if (raycastHitUp.collider != null)
            {
                if (raycastHitUp.collider.gameObject.CompareTag("TimeBonus"))
                {
                    elapsedTime += TimeBonus;
                    TimeBonusCooldown = 2.0f;
                    Destroy (raycastHitUp.collider.gameObject);
                }
            }
            else if (raycastHitDown.collider != null)
            {
                if (raycastHitDown.collider.gameObject.CompareTag("TimeBonus"))
                {
                    elapsedTime += TimeBonus;
                    TimeBonusCooldown = 2.0f;
                    Destroy (raycastHitDown.collider.gameObject);
                }
            }
        }
    }

    public void TimerFlip(){
        if (worldUp > 0) { FlipWorld(); }
    }
    private void FlipWorld(){
        if (worldUp < 0){
            elapsedTime = TimeLimit;
        }
        
        body.transform.position = new Vector2(body.position.x, -body.position.y);
        body.gravityScale *= -1;
        worldUp *= -1;
        body.transform.localScale = new Vector3(0.2f,0.2f * worldUp,0.2f);
        if (worldUp < 0 && elapsedTime >= 0){
            elapsedTime = -1;
            body.velocity = new Vector2(body.velocity.x, body.velocity.y + portalBoost * worldUp);
        }
        if (worldUp < 0){
            musicManager.ChangeMusic("down");
        }
        else{
            musicManager.ChangeMusic("up");
        }
        
    }

    public void MovePlayer(float x, float y){
        body.position = new Vector2(body.position.x + x, body.position.y + y);
    }
}

