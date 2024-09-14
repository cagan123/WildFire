using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : Entity
{
    [SerializeField] public LayerMask PlayerLayer;

    [Header("Chase Info")]
    public float agroDistance = 4f;

    [Header("Cooldown Info")]
    [HideInInspector] public float lastTimeAttacked;
    public float attackCooldown;
    [HideInInspector] public float attackCooldownTimer;

    [SerializeField] public bool startFlipped;

    [Header("Attack Info")]
    public bool Is4Directional;
    public float prepDuration1;
    public float prepDuration2;
    public float prepDuration3;
    public int attackNumber;
    public Transform[] attackCheck;
    public float[] attackCheckRadius;
    public float attackDistance;

    #region Components
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyState deathState;
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
        var direction_x = player.position.x - transform.position.x;
        if (direction_x < -.1f && !facingLeft) // it is -1 and 1 to prevent jittering
        {
            Flip();
        }
        else if (direction_x > .1f && facingLeft)
        {
            Flip();
        }
        if(Is4Directional){
            var direction_y = player.position.y -transform.position.y;
            if(direction_y < -.1f && !facingUp){
                anim.SetBool("isAttackingDown", true);
                facingUp = !facingUp;
            }
            else if (direction_y > .1f && facingUp){
                anim.SetBool("isAttackingDown", false);
                facingUp = !facingUp;
            }
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
            Gizmos.DrawWireSphere(attackCheck[i].position, attackCheckRadius[i]);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x-attackDistance, transform.position.y));
        }
    }
    #endregion
    # region Death
    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deathState);
    }
    public void GetDeathState(EnemyState _deathState){
        deathState = _deathState;
    }
    #endregion
}
