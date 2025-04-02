using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5f;
    public Animator animator;
    private CharacterController controller;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);
        if (move.magnitude > 0.1f) {
            // 面向移動方向
            transform.forward = move.normalized;

            // 移動角色
            controller.Move(move.normalized * moveSpeed * Time.deltaTime);
        }

        // 傳給動畫
        animator.SetFloat("Speed", move.magnitude);
    }
}
