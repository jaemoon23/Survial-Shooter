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
        // 회전
        var rotation = Quaternion.Euler(0f, rotateSpeed * Time.deltaTime, 0f);
        rigidbody.MoveRotation(rigidbody.rotation * rotation);

        // 이동
        Vector3 direction = new Vector3(playerInput.Horizontal, 0f, playerInput.Vertical);
        Vector3 movePos = rigidbody.position + direction * speed * Time.deltaTime;
        rigidbody.MovePosition(movePos);
        float moveAmount = direction.magnitude;
        animator.SetFloat(Defines.hashMove, moveAmount);

        // 카메라
        Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);  // 마우스가 가르키는 위치를 ray로 변환
        if (Physics.Raycast(mouse, out RaycastHit hit, 100f, groundMask))   // ray가 땅에 닿았는지 확인
        {
            Vector3 lookAt = hit.point; // 마우스가 닿은 위치
            lookAt.y = transform.position.y; // y축의 차이는 무시
            transform.LookAt(lookAt);   // 플레이어가 마우스 방향을 바라보게
        }
    }
}
