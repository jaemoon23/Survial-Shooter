using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public new Rigidbody rigidbody;
    public PlayerInput playerInput;
    public Animator animator;

    public float speed = 10f;
    public float rotateSpeed = 100f;
    public LayerMask groundMask;


    private void FixedUpdate()
    {
        // ȸ��
        var rotation = Quaternion.Euler(0f, rotateSpeed * Time.deltaTime, 0f);
        rigidbody.MoveRotation(rigidbody.rotation * rotation);

        // �̵�
        Vector3 direction = new Vector3(playerInput.Horizontal, 0f, playerInput.Vertical);
        Vector3 movePos = rigidbody.position + direction * speed * Time.deltaTime;
        rigidbody.MovePosition(movePos);
        float moveAmount = direction.magnitude;
        animator.SetFloat(Defines.hashMove, moveAmount);

        // ī�޶�
        Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);  // ���콺�� ����Ű�� ��ġ�� ray�� ��ȯ
        if (Physics.Raycast(mouse, out RaycastHit hit, 100f, groundMask))   // ray�� ���� ��Ҵ��� Ȯ��
        {
            Vector3 lookAt = hit.point; // ���콺�� ���� ��ġ
            lookAt.y = transform.position.y; // y���� ���̴� ����
            transform.LookAt(lookAt);   // �÷��̾ ���콺 ������ �ٶ󺸰�
        }
    }
}
