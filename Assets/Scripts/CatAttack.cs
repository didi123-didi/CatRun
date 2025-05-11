using UnityEngine;

public class CatAttack : MonoBehaviour
{
    public Transform attackPoint;
    public Vector2 attackSize = new Vector2(0.5f, 0.5f);
    public LayerMask enemyLayers;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Attack()
    {
        Debug.Log("ATTACK!");
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0, enemyLayers);
        Debug.Log("HITS: " + hits);
        foreach (Collider2D hit in hits)
        {
            Debug.Log("命中：" + hit.name);

            if (hit.TryGetComponent<BinManagement>(out BinManagement obj))
            {
                Debug.Log("有");
                obj.Hit();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackSize);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("AttackTrigger");
            Attack();
        }
    }
}