using Assets.Scripts.Player;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveToPlayer : MonoBehaviour
{
    [Inject]
    private PlayerMove _player;
    
    private float _speed;

    public void Construct(int speed) => 
        _speed = speed;

    private void Update() =>
        transform.position = Vector2.MoveTowards
            (transform.position, _player.transform.position, _speed * Time.deltaTime);

}
 