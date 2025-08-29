using System;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rigidbody;
    public PlayerInput playerInput;
    public float speed = 10f;
    public float rotateSpeed = 100f;


    private void FixedUpdate()
    {
        // ȸ��
        var rotation = Quaternion.Euler(0f, playerInput.Roate * rotateSpeed * Time.deltaTime, 0f);
        rigidbody.MoveRotation(rigidbody.rotation * rotation);

        // �̵�
        var distance = playerInput.Move * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + distance * transform.forward);

        
    }
}
