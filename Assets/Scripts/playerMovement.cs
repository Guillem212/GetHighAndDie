using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	/*La deteccion por raycast del suelo y los muros no se llaman 
	constantemente, solo cuando son pedidos, así que el coste es 
	muy bajo. */

	[Range(200, 400)]
	public float  movementSpeed = 40f; //Determina la velocidad de movimiento.

	float smoothMove = .05f; //Hace más suave el movimiento.
	private Vector3 velocity = new Vector3();

	float horizontalMove;
	public static bool lookingRight = true;
	int directionLook = 1; // 1 - Mirando hacia la derecha, -1 - Mirando hacia la izquierda.

	private Rigidbody2D rb;


	public LayerMask groundLayer; //Detecta el layerMask del suelo.
	public LayerMask wallLayer; //Detecta el layerMask del muro.

	private Vector3 wallVel; //Determina la velocidad de movimiento en el muro.
	private Vector2 jumpWall = new Vector2(1, 1); //Determina el angulo de salto.

	[Range(1, 10)]
	public int jumpVel;


	float fallMultiplier = 2.5f;
	float lowerMultiplier = 2f;


	private RaycastHit2D hit;
	private Vector3 offset;


	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		velocity = Vector3.zero;
	}

	void FixedUpdate () {

		//Da un valor entre movementSpeed y -movementSpeed por el tiempo.
		horizontalMove = Input.GetAxis("Horizontal") * movementSpeed * Time.fixedDeltaTime;


		//Diferencia entre la situacion del muro y el movimiento normal.
		if(Input.GetButton("GrapWall") && detecttWall()){
			onWall();
		}
		else{
			Movement();
			jumpOnGround();
		}
	}
	void Update(){
		//Rota al jugador
		if (horizontalMove < 0 && lookingRight){
			Flip();
		}
		else if (horizontalMove > 0 && !lookingRight){
			Flip();
		}
	}

	//---Movimiento Vertical---\\
	void Movement() {
		//Crea el vector de velocidad en y.
		Vector3 targetVelocity = new Vector2(horizontalMove, rb.velocity.y);

		//Aplica los calculos al rigidBody del player.
		/* if(horizontalMove == 0){
			rb.velocity = Vector3.zero;

		}else{*/
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothMove);
		//}
		
	}

	//---Salto Normal---\\
	void jumpOnGround(){
		if (Input.GetButtonDown("Jump") && detectGround()){
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
	void jumpOnWall(){
		if(Input.GetButtonDown("Jump")){
			jumpWall.x *= directionLook;
			rb.velocity = jumpWall * jumpVel;
		}
	}

	//---Detecta si Está Tocando un Muro---\\
	void onWall(){
		if (!detectGround()){
			wallVel = new Vector2(0, rb.velocity.y * 0.125f);
			rb.velocity = Vector3.SmoothDamp(rb.velocity, wallVel, ref velocity, smoothMove);

			//Animacion

			jumpOnWall();
		}
	}

	//---Detecta el Suelo---\\
	bool detectGround(){
		offset = new Vector3(transform.position.x, transform.position.y -0.5f);
		//Debug.DrawRay(offset, Vector2.down / 3, Color.green);
		hit = Physics2D.CircleCast(offset, 0.08f, Vector2.down, 0.08f, groundLayer);
		if (hit.collider != null) {
			return true;
		}
		return false;
	}

	//---Detecta si Está Tocando una Pared---\\
	bool detecttWall(){
		offset = new Vector3(transform.position.x,transform.position.y - 0.1f);
		//Debug.DrawRay(offset, Vector2.left / 3, Color.green);
		hit = Physics2D.CircleCast(offset, 0.21f,  Vector2.left, 0.21f, wallLayer);
		if (hit.collider != null) {
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
		directionLook = -directionLook;
		transform.Rotate(0f, 180f, 0f);
	}
}
