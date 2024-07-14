using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : Entity
{
    [SerializeField] protected LayerMask PlayerLayer;

    [Header("Chase Info")]
    public float agroDistance = 4f;

    [Header("Cooldown Info")]
    [HideInInspector] public float lastTimeAttacked;
    public float attackCooldown;
    [HideInInspector] public float attackCooldownTimer;

    [HideInInspector]public int facingDirection { get; private set; } = 1;
    [HideInInspector]protected bool facingLeft = true;
    [HideInInspector]protected bool facingUp = false;
    [SerializeField] public bool startFlipped;
    [Header("Attack Info")]
    public float prepDuration1;
    public float prepDuration2;
    public float prepDuration3;
    public int attackNumber;
    public Transform[] attackCheck;
    public float attackCheckRadius;
    public float attack2CheckRadius;
    public float attack3CheckRadius;
    public float attackDistance;

    #region Components
    public EnemyStateMachine stateMachine { get; private set; }
    [HideInInspector]public FieldOfView fieldOfView;
    public PathFinder pathFinder{get; private set;}
    [HideInInspector]public Transform player;
    public Collider2D collider2d{get; private set;}
    [HideInInspector]public Assasin assasin;
    [HideInInspector]public Patroller patrol;
    #endregion

    
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        pathFinder = GetComponentInChildren<PathFinder>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
        collider2d = GetComponentInChildren<Collider2D>();
        if(GetComponent<Assasin>() != null){
            assasin = GetComponent<Assasin>();
        }
        if(GetComponent<Patroller>() != null){
            patrol = GetComponent<Patroller>();
        }
        if(startFlipped){
            Flip();
        }
    }
    protected override void Start()
    {
        base.Start();   
        player = PlayerManager.instance.player.transform;   
    }
    public bool PlayerInAssasinArea(){
        if(GetComponent<Assasin>() != null){
            return assasin.assasinTriggered;
        }
        else{
            return false;
        }
    }
    public bool Patrolling(){
        if(GetComponent<Patroller>() != null){
            return !patrol.playerSeen;
        }
        else{
            return false;
        }

    }
    public bool canSeePlayer() => fieldOfView.playerSeen;
    public Vector2 direction() => pathFinder.direction;
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTirgger();
   
    #region EnemyToPlayer...
    public float EnemytoPlayerDistance()
    {
        var distance = Vector2.Distance(transform.position, player.position);
        return distance;
    }

    public Vector2 EnemyToPlayerDirection()
    {
        var direction = (player.position - transform.position).normalized;
        return direction;
    }
    #endregion

    #region Enemy Flippers

    public virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        facingLeft = !facingLeft;
        transform.Rotate(0, 180, 0);
    }
    public void ControlFlipforEnemytoPlayer()
    {
        var direction_y = player.position.y -transform.position.y;
        var direction_x = player.position.x - transform.position.x;
        if (direction_x < -.1f && !facingLeft) // it is -1 and 1 to prevent jittering
        {
            Flip();
        }
        else if (direction_x > .1f && facingLeft)
        {
            Flip();
        }
        if(direction_y < -.1f ){
            anim.SetBool("isAttackingDown", true);
            facingUp = false;
        }
        else{
            anim.SetBool("isAttackingDown", false);
            facingUp = true;
        }
    }
    public void ControlFlipforEnemybyVelocity(){
        if(rb.velocity.x> 0 && facingLeft){
            Flip();
        }
        if(rb.velocity.x<0 && !facingLeft){
            Flip();
        }
    }
    #endregion

    #region Gizmos
   protected virtual void OnDrawGizmos()
    {
        for(int i = 0; i < attackCheck.Length; i++){
            Gizmos.DrawWireSphere(attackCheck[i].position, attackCheckRadius);
        }
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x-attackDistance, transform.position.y));
    }

    #endregion

}
