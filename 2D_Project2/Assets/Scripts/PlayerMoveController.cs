using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveController : MonoBehaviour
{
    public float moveSpeed = 5.0f; //�÷��̾� �̵� �ӵ�
    public float jumpPower = 5.0f; //�÷��̾� ���� ��

    public Animator animator; // Animator �Ӽ� ���� ����
    public Rigidbody2D rigid; // Rigidbody 2D �Ӽ� ���� ����
    public SpriteRenderer rend; // SpriteRenderer �Ӽ� ���� ����
    //public SpriteRenderer spriteRenderer;
    public Sprite jump_sprite;

    float horizontal; //����, ������ ���Ⱚ�� �޴� ����
    bool isjumping; // ���� ���������� ��, ���� ���� ������ bool�� ����

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // animator ������ Player�� Animator �Ӽ����� �ʱ�ȭ
        rend = GetComponent<SpriteRenderer>(); // rend ������ Player�� SpriteRenderer �Ӽ����� �ʱ�ȭ
        rigid = GetComponent<Rigidbody2D>(); // rigid ������ Player�� Rigidbody 2D �Ӽ����� �ʱ�ȭ
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() // �÷��̾� �̵��� �׻� �߿��ϰ� �̷�����Ƿ� Fixed Update�� �̿�
    {
        Move(); //�÷��̾� �̵�
        Jump(); //����
        Debug.Log(animator.enabled);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isjumping = false;
            animator.enabled = true;
        }

        if (collision.gameObject.CompareTag("obstacle"))
        {
            SceneManager.LoadScene(0);
        }
    }

    void Jump()
    {
        if (Input.GetButton("Jump")) //���� Ű�� ������ ��
        {
            if (isjumping == false) //���� ������ ���� ��
            {
                animator.enabled = false;
                //spriteRenderer.sprite = jump_sprite;
                //rend.sprite = jump_sprite;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //�������� ���� �ش�.
                isjumping = true;
            }
            else return; //���� ���� ���� �������� �ʰ� �ٷ� return.
        }
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            animator.SetBool("walk", true);

            if (horizontal >= 0)
            {
                rend.flipX = false; // Player�� Sprite�� �¿������Ű�� �Լ� , true�� �� ���� 
                //spriteRenderer.flipX = false;
                //this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                rend.flipX = true;
                //spriteRenderer.flipX = true;
                //this.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            animator.SetBool("walk", false);
        }

        Vector3 dir = horizontal * Vector3.right; // transform.Translate() ������ �ڷ����� ���߱� ���� ������ ���ο� Vector3 ���� ����

        this.transform.Translate(dir * moveSpeed * Time.deltaTime); //������Ʈ �̵� �Լ�
    }
}
