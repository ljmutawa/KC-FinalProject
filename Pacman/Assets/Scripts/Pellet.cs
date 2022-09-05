using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int pelletEatenPoints = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }

    //private void -> only this class has access
    //protected void -> this class & subclasses have access
    //adding "virtual" -> allows an override

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this); 
    }
}
