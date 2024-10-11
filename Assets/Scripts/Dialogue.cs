using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
	public string[] speechTxt;
	public string actorName;

	public LayerMask playerLayer;
	public float radious;

	public GameObject interact;
	
	bool onRadious;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.F) && onRadious)
			{
				DialogueControl.Instance.Speech(profile, speechTxt, actorName);
			}
	}

	private void FixedUpdate()
	{
		Interact();
	}

	public void Interact()
	{
		Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);
			if(hit != null)
			{
				interact.SetActive(true);
				onRadious = true;
			}
			else
			{
				interact.SetActive(false);
				onRadious = false;
			}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, radious);
	}
	
	
}
