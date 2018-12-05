﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	/*La deteccion por raycast del suelo y los muros no se llaman 
	constantemente, solo cuando son pedidos, así que el coste es 
	muy bajo. */

	[Range(400, 800)]
	[SerializeField] private float  movementSpeed; //Determina la velocidad de movimiento.

	private float smoothMove = .005f; //Hace más suave el movimiento.
	private Vector3 velocity = new Vector3();
	Vector3 targetVelocity;

	private float horizontalMove;
	[SerializeField] private static bool lookingRight = true;
	private int directionWall, directionLook;

	private Rigidbody2D rb;


	[SerializeField] private LayerMask groundLayer; //Detecta el layerMask del suelo.
    [SerializeField] private LayerMask enemyLayer;

	private Vector3 wallVel; //Determina la velocidad de movimiento en el muro.
	private Vector2 jumpWall = new Vector2(1f, 2f); //Determina el angulo de salto.

	private Vector3 offset = new Vector3(0f, -1f, 0f);

	[Range(10, 20)]
    [SerializeField] private float jumpVel;

	private float fallMultiplier = 7f;
	private float lowerMultiplier = 2f;

	private bool canPlay = true;

	private RaycastHit2D hit;

	private Animator anim;

	private bool canJump, canMove, canAttack, canWall, stillJumping, wannaJumpWall, isOnWall = false;
	private bool animGoJump = false, animJumping = false, animAttack = false;

	private bool flipped;


	void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        velocity = Vector3.zero;
        directionLook = 1;
        isOnWall = false;
        animGoJump = false;
        animJumping = false;
        animAttack = false;
        smoothMove = .005f;
        velocity = new Vector3();
        jumpWall = new Vector2(1f, 2f);
        new Vector3(0f, -1f, 0f);
        fallMultiplier = 7f;
        lowerMultiplier = 2f;
        lookingRight = true;
    }

	void Update(){
		if(anim.GetBool("isSmoking") || anim.GetBool("isCocaine") || anim.GetBool("isCristal")){
			canPlay = false;
		}
		else{
			canPlay = true;
		}

		if(!wannaJumpWall){
			horizontalMove = Input.GetAxis("Horizontal");
		}

		canJump = Input.GetButtonDown("Jump");
		stillJumping = Input.GetButton("Jump");
		canAttack = Input.GetButtonDown("Attack");
		canMove = horizontalMove != 0;

        if (!isOnWall && canPlay)
            jumpOnGround();
		animationUpdate();
    }

	void animationUpdate(){
		anim.SetFloat("HorizontalMove", Mathf.Abs(horizontalMove));
		anim.SetBool("goJump", animGoJump);
		anim.SetBool("isJumping", rb.velocity.y >= 1);
		anim.SetFloat("isFalling", rb.velocity.y);
		anim.SetBool("isAttacking", animAttack);
		anim.SetBool("isOnWall", isOnWall);
	}

	void FixedUpdate () {
		//Da un valor entre movementSpeed y -movementSpeed por el tiempo.
		//Diferencia entre la situacion del muro y el movimiento normal.
		if(canPlay){
			if(rb.velocity.x == 0){
				onWall();
				if(directionLook != 0 && canJump && isOnWall)
					StartCoroutine(jumpOnWall(directionLook));
			}
			if(canMove && !isOnWall){
				Movement();
			}
			else{
				rb.velocity = new Vector2(0, rb.velocity.y);
			}
		
			if(canAttack){
				attackPlayer();
			}

			if(!isOnWall && flipped){
				flipped = false;
				transform.Rotate(0f, 180f, 0f);
			}
		}
		else{
			rb.velocity = Vector2.zero;
		}
	}

	void attackPlayer(){
		if (detectEnemiesHit() && detectGround()){
			animAttack = true;
			FindObjectOfType<AudioManager>().Play("PlayerAttack");
			StartCoroutine(finishAttack());
		}
		else{
			animAttack = false;
		}
	}

	IEnumerator finishAttack(){
		yield return new WaitForSeconds(0.75f);
		animAttack = false;
	}

	//---Movimiento Vertical---\\
	void Movement() {
		//Crea el vector de velocidad en y.
		if(horizontalMove == 0){ 
			targetVelocity = new Vector2(0, rb.velocity.y);
		}
		else{ 
			targetVelocity = new Vector2(horizontalMove  * movementSpeed * Time.fixedDeltaTime, rb.velocity.y);
			}
		rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothMove);
		if(!isOnWall){
			if (horizontalMove < 0 && lookingRight){ Flip(); }
			else if (horizontalMove > 0 && !lookingRight){ Flip(); }
		}
	}

	void onWall(){
		if(!detectGround()){
			if(detectWall()){
				if(directionLook == directionWall && horizontalMove  == directionWall){
					isOnWall = true;

					wannaJumpWall = false;

					if(!flipped){
						flipped = true;
						transform.Rotate(0f, 180f, 0f);
					}
					wallVel = new Vector2(directionLook, rb.velocity.y / 2);
					rb.velocity = Vector3.SmoothDamp(rb.velocity, wallVel, ref velocity, smoothMove);
				}
				else{
					isOnWall = false;
				}
			}
			else{
				isOnWall = false;
			}
		}
		else{
			isOnWall = false;
		}

	}

	//---Salto en el Muro---\\
	IEnumerator jumpOnWall(float direction){
		FindObjectOfType<AudioManager>().Play("PlayerJumpOnWall");
		animJumping = true;
		wannaJumpWall = true;
		isOnWall = false;

		horizontalMove = -direction;
		jumpWall.x *= -direction;

		rb.AddForce(jumpWall * jumpVel * 50);

		yield return new WaitForSeconds(.4f);
		wannaJumpWall = false;
	}

	//---Salto Normal---\\
	void jumpOnGround(){
		if (canJump && !isOnWall && detectGround()){
			FindObjectOfType<AudioManager>().Play("PlayerJumpOnGround");
			animGoJump = true;
			rb.AddForce(Vector2.up * jumpVel * 100);
		}
		else{
			animGoJump = false;
		}
		if (rb.velocity.y < 0){
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.fixedDeltaTime; 
		}
		else if (rb.velocity.y > 0 && !stillJumping){
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowerMultiplier -1) * Time.fixedDeltaTime;
		}
	}

	//---Detecta el Suelo---\\
	bool detectGround(){
		hit = Physics2D.CircleCast(transform.position + offset, 0.2f, Vector2.down, 0.2f, groundLayer);
		if (hit.collider != null) {
			return true;
		}
		return false;
	}

	bool detectEnemiesHit(){
		if(lookingRight){
			hit = Physics2D.Raycast(transform.position , Vector2.right, 1,  enemyLayer);
		}
		else{
			hit = Physics2D.Raycast(transform.position, Vector2.left, 1,  enemyLayer);
		}
		if (hit.collider != null){
			EnemyController.isScared = true;
			GameObject enemyHit = hit.collider.gameObject;
			enemyHit.GetComponentInChildren<EnemyController>().dieEnemy();
			enemyHit = null;
			return true;
		}
		return false;
	}

	bool detectWall(){
		if(lookingRight){
			hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f,  groundLayer);
			directionWall = 1;
		}
		else{
			hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, groundLayer);
			directionWall = -1;
		}
		if (hit.collider != null){
			return true;
		}
		directionWall = 0;
		return false;
	}

	//---Voltea en GameObject---\\
	void Flip(){
		lookingRight = !lookingRight;
		directionLook = -directionLook;
		transform.Rotate(0f, 180f, 0f);
	}

	//---GETTERS Y SETTERS---\\

    public float GetMovementSpeed()
    {
        return this.movementSpeed;
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }

    public float GetJumpVel()
    {
        return this.jumpVel;
    }

    public void SetJumpVel(float jumpVel)
    {
        this.jumpVel = jumpVel;
    }
}
