using UnityEngine;

public class Interactable : MonoBehaviour
{	

	public string interactableName;
	public GameObject interactionPoint;
	public Animator playerAnimator;
	public GameObject currentPlayer;

	public bool isClicked = false;


	public virtual void FindPlayer(GameObject player)
	{
		player = currentPlayer;
		playerAnimator = currentPlayer.GetComponent<Animator> ();
	}

	public virtual void Interact(GameObject player)

	{
	}
}

