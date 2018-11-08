using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	/*La deteccion por raycast del suelo y los muros no se llaman 
	constantemente, solo cuando son pedidos, así que el coste es 
	muy bajo. */

	[Range(400, 800)]
	public float  movementSpeed; //Determina la velocidad de movimiento.

	private float smoothMove = .005f; //Hace más suave el movimiento.
	private Vector3 velocity = new Vector3();
	Vector3 targetVelocity;

	private float horizontalMove;
	public static bool lookingRight = true;

	private Rigidbody2D rb;


	public LayerMask groundLayer; //Detecta el layerMask del suelo.

	private Vector3 wallVel; //Determina la velocidad de movimiento en el muro.
	private Vector2 jumpWall = new Vector2(1, 1); //Determina el angulo de salto.
	private bool isOnWall = false;

	private Vector3 offset = new Vector3(0f, -1f, 0f);

	[Range(10, 20)]
	public float jumpVel;


	private float fallMultiplier = 7f;
	private float lowerMultiplier = 2f;


	private RaycastHit2D hit;

	private Animator anim;


	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		velocity = Vector3.zero;
	}

	void FixedUpdate () {
		//Da un valor entre movementSpeed y -movementSpeed por el tiempo.
		horizontalMove = Input.GetAxis("Horizontal");
		anim.SetFloat("HorizontalMove",Mathf.Abs(horizontalMove));

		//Diferencia entre la situacion del muro y el movimiento normal.
		if(Input.GetButton("GrapWall")){
			onWall();
		}
		else{
			isOnWall = false;
			anim.SetBool("isOnWall", false);
		}
		if(!isOnWall){
			Movement();
			jumpOnGround();

			//Animaciones
			if(rb.velocity.y > 0){
				anim.SetBool("goJump", false);
				anim.SetBool("isJumping", true);
			}
			else if(rb.velocity.y <= 0){
				anim.SetBool("isJumping", false);
				anim.SetFloat("isFalling", rb.velocity.y);
			}

			//Rota al jugador
			if (horizontalMove < 0 && lookingRight){
				Flip();
			}
			else if (horizontalMove > 0 && !lookingRight){
				Flip();
			}
		}
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
		
	}

	//---Detecta si Está Tocando un Muro---\\
	void onWall(){
		if (!detectGround()){
			if(detectLefttWall()){
				isOnWall = true;

				//Animacion
				anim.SetBool("isOnWall", true);

				if (!lookingRight){
					Flip();
				}

				wallVel = new Vector2(0, rb.velocity.y / 2);
				rb.velocity = Vector3.SmoothDamp(rb.velocity, wallVel, ref velocity, smoothMove);

				jumpOnWall(1);
			}
			else if(detectRightWall()){
				isOnWall = true;

				//Animacion
				anim.SetBool("isOnWall", true);

				if (lookingRight){
					Flip();
				}

				wallVel = new Vector2(0, rb.velocity.y / 2);
				rb.velocity = Vector3.SmoothDamp(rb.velocity, wallVel, ref velocity, smoothMove);

				jumpOnWall(-1);
			}
			else{
				isOnWall = false;
			}
		}
		else{
			isOnWall =false;
			anim.SetBool("isOnWall", false);
		}
	}

	void attackPlayer(){
		if (detectEnemiesHit()){
			print("Enemigo Golpeado");
		}
	}

	//---Salto Normal---\\
	void jumpOnGround(){
		if (Input.GetButtonDown("Jump") && detectGround()){

			//Aniamcion
			anim.SetBool("goJump", true);

			//rb.velocity = Vector2.up * jumpVel;
			rb.AddForce(Vector2.up * jumpVel*100);
		}
		if (rb.velocity.y < 0){
				rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.fixedDeltaTime; 
		}
		else if (rb.velocity.y > 0 && !Input.GetButton("Jump")){
				rb.velocity += Vector2.up * Physics2D.gravity.y * (lowerMultiplier -1) * Time.fixedDeltaTime; 
		}
	}

	//---Salto en el Muro---\\
	void jumpOnWall(int direction){
		if(Input.GetButtonDown("Jump") && horizontalMove != 0){
			isOnWall = false;
			jumpWall.x *= direction;
			rb.velocity = jumpWall * jumpVel;

			anim.SetBool("isOnWall", false);
			anim.SetBool("isJumping", true);
		}
	}

	//---Detecta el Suelo---\\
	bool detectGround(){
		//Debug.DrawRay(transform.position + offset, Vector2.down * 0.2f, Color.white);
		hit = Physics2D.CircleCast(transform.position + offset, 0.2f, Vector2.down, 0.2f, groundLayer);
		if (hit.collider != null) {
			return true;
		}
		return false;
	}

	bool detectEnemiesHit(){
		hit = Physics2D.Raycast(transform.position, Vector2.right);
		Debug.DrawRay(transform.position, Vector2.left, Color.green);
		if (hit.collider.tag == "Enemy"){
			return true;
		}

		return false;
	}

	bool detectRightWall(){
		hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f,  groundLayer);
		if (hit.collider != null){
			//Debug.DrawRay(transform.position, Vector2.right * 0.5f, Color.green);
			return true;
		}

		return false;
	}
	bool detectLefttWall(){
		hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, groundLayer);
		if (hit.collider != null){
			//Debug.DrawRay(transform.position, Vector2.left * 0.5f, Color.blue);
			return true;
		}

		return false;
	}

	//---Voltea en GameObject---\\
	void Flip(){

		/*Al voltear el game object con un rotate
		también se voltea el eje de dirección y
		con ello el vector, así que el ataque 
		solo hace falta programarlo hacia un lado. */

		lookingRight = !lookingRight;
		transform.Rotate(0f, 180f, 0f);
	}
}
