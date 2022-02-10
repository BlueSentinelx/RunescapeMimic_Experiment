using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour 
{

	private NavMeshAgent playerAgent;

	public GameObject activePlayer;

	private Animator animator;

	public Interactable interactObj;

	private int speedId;
	//private int rotateId;	

	public Vector3 moveTarget;

	public bool isInteractable;
	public bool pathReached;
	public bool canMove;

	public Quaternion rot;

	public GameObject currentInteractable;

	public CameraUIControl camCtrl;

	public GameObject BankUI;

	private GameObject camScript;

	public enum MoveFSM
	{
		findPosition,
		move,
		turnToFace,
		interact
	}

	public MoveFSM moveFSM;
	// Use this for initialization


	public enum TurnFSM
	{
		Empty,
		TriggerTurn,
		WaitForTurnEnd
	}

	public TurnFSM turnFSM;

	private void Start () 
	{

		camScript = GameObject.Find("playerCamera");   //your camera
		camCtrl = camScript.GetComponent<CameraUIControl>(); //access script attached to your camera
		animator = this.GetComponent<Animator> ();	
		speedId = Animator.StringToHash ("speed");
		//rotateId = Animator.StringToHash ("Angle");

		playerAgent = this.GetComponent<NavMeshAgent> ();
		canMove = true;
		pathReached = false;
		activePlayer = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){
			GetInteraction ();
		}	

		MoveState ();
	}

	private void FixedUpdate()
	{	

		if (Input.GetKey(KeyCode.LeftArrow))camCtrl.currentX += 500f * 2f * Time.deltaTime;
		if (Input.GetKey(KeyCode.RightArrow))camCtrl.currentX -= 500f * 2f * Time.deltaTime;
		if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))camCtrl.currentX = 0;
		if (Input.GetKey(KeyCode.UpArrow))if(camCtrl.offset.y <=7f)camCtrl.offset.y += 2f * 2f * Time.deltaTime;
		if (Input.GetKey(KeyCode.DownArrow))if(camCtrl.offset.y >=0f)camCtrl.offset.y -= 2f * 2f * Time.deltaTime;

		if (pathReached == true && isInteractable == true) {
			interactObj.gameObject.GetComponentInParent <Animator> ().SetBool ("InteractSwitch", true);
		} else {

		}
	}

	public void MoveState()
	{
		switch (moveFSM) 
		{
		case MoveFSM.findPosition:
			break;
		case MoveFSM.move:
			BankUI.SetActive (false);
			MoveToEnd ();
			break;
		case MoveFSM.turnToFace:
			TurnToFace ();
			break;
		case MoveFSM.interact:
			
			if(Input.GetMouseButtonUp(0)){
				Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit interactionInfo;
				if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity)) 
				{
					if (interactObj.interactableName == "Chest" && interactionInfo.collider.tag =="Interactable") 
					{
						interactObj.Interact (this.gameObject);
					} 
					else if (interactObj.interactableName == "Bank" && interactionInfo.collider.tag =="Interactable")
					{ 
						BankUI.SetActive (true);
					} 
				}
			}
			break;

		}
	}

	public void MoveToEnd()
	{
		if(!playerAgent.pathPending)
		{
			if(playerAgent.remainingDistance <= playerAgent.stoppingDistance)
			{	
				
				animator.SetFloat (speedId, 0f);

				pathReached = true;

				moveFSM = MoveFSM.turnToFace;
			}
		}
	}

	public void TurnToFace()
	{
		if(currentInteractable != null)
		{
			if(pathReached == true)
			{
				Vector3 dir = currentInteractable.transform.position - transform.position;
				dir.y = 0;
				rot = Quaternion.LookRotation (dir);
				transform.rotation = Quaternion.Lerp (transform.rotation, rot, 5f * Time.deltaTime);

				if((rot.eulerAngles - transform.rotation.eulerAngles).sqrMagnitude < .01)
				{	
					pathReached = false;
					//animator.SetBool ("Turning", false);
					//animator.SetTrigger ("Open");
					turnFSM = TurnFSM.Empty;
					moveFSM = MoveFSM.interact;
				}
					
			}
		}
		else if(currentInteractable == null)
		{
			moveFSM = MoveFSM.findPosition;
		}
	}

	private void GetInteraction()
	{
		if(canMove)
		{
			Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit interactionInfo;
			if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity)) 
			{
				if (interactionInfo.collider.tag == "Interactable") {
					currentInteractable = interactionInfo.collider.gameObject;
					isInteractable = true;
					currentInteractable = interactionInfo.collider.gameObject;

					playerAgent.destination = 
						currentInteractable.GetComponent<Interactable> ().interactionPoint.transform.position;
					currentInteractable.GetComponent<Interactable> ().isClicked = true;
					moveTarget = playerAgent.destination;
					interactObj = interactionInfo.collider.gameObject.GetComponent<Interactable>();
					animator.SetFloat (speedId, 3f);
					pathReached = false;
					moveFSM = MoveFSM.move;
				} 
				else 
				{
					if(currentInteractable != null)
					{
						currentInteractable.GetComponent<Interactable> ().isClicked = false;
						currentInteractable = null;
					}
					isInteractable = false;

					moveTarget = interactionInfo.point;
					playerAgent.destination = interactionInfo.point;

					animator.SetFloat (speedId, 3f);
					moveFSM = MoveFSM.move;
				}
			}
		}
	}


}	
