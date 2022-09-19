using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerObserver : MonoBehaviour
{
    public event Action<Collider2D> TriggerEnter;
    public event Action<Collider2D> TriggerExit;

    private void OnTriggerEnter2D(Collider2D collision) => 
        TriggerEnter.Invoke(collision);

    private void OnTriggerExit2D(Collider2D collision) =>
       TriggerExit.Invoke(collision);
}