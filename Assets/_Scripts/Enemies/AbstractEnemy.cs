

using TMPro;
using UnityEngine;

public abstract class AbstractEnemy: MonoBehaviour
{
    [SerializeField] protected int maxHP;
    [SerializeField] protected int hp;
    [SerializeField] protected GameObject _DamageNambersText;
    public abstract void TakeDamage(int damage);
    public abstract void Die();
    protected virtual void DamageNumbers(int dmg)
    {
        var dmgNum = Instantiate(_DamageNambersText, transform.position, Quaternion.identity, transform);
        dmgNum.GetComponent<TextMeshPro>().text = dmg.ToString();


    }

}
