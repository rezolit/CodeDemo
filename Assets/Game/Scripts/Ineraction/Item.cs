using System;
using System.Collections;
using System.Collections.Generic;
using Ineraction;
using UnityEngine;

/// <summary>
/// Demonstrates the work of the interaction interface
/// </summary>
public class Item : MonoBehaviour, IInteractable
{
	[SerializeField]
	private ParticleSystem _particleSystem;
	
	private Color defaultColor;

	private void Awake()
	{
		defaultColor = GetComponent<SpriteRenderer>().color;
	}

	public void OnInteract()
    {
        _particleSystem.Play();
    }

    public void ShowHint()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void HideHint()
    {
        GetComponent<SpriteRenderer>().color = defaultColor;
    }
}
