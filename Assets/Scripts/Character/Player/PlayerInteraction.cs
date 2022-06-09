using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class PlayerInteraction : Interaction
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Vector3 _enemySearchOffset;

    private Animator _animator;
    private SlapDirection _slapDirection;

    public event UnityAction Slaped;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void VFXDamage()
    {
        Slaped?.Invoke();
    }

    private void FindTarget()
    {
        List<EnemyInteraction> enemys = new List<EnemyInteraction>();
        Collider[] targets = Physics.OverlapBox(transform.position + _enemySearchOffset, Vector3.one , Quaternion.identity, _enemyLayer);

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].TryGetComponent(out EnemyInteraction enemy))
            {
                enemys.Add(enemy);
                enemy.Slap(this);
                Slaped?.Invoke();
            }
        }

        if (enemys.Count > 0)
        {
            _slapDirection = GetEnemyDirection(enemys);
            _animator.SetTrigger(_slapDirection.ToString());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAlive)
            FindTarget();
    }

    private SlapDirection GetEnemyDirection(List<EnemyInteraction> enemys)
    {
        if (enemys.Count > 1)
            return SlapDirection.All;

        float deltaX = enemys[0].transform.position.x - transform.position.x;

        if (deltaX < -0.25f)
            return SlapDirection.Left;
        else if (deltaX > 0.25f)
            return SlapDirection.Right;
        else
            return SlapDirection.All;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + _enemySearchOffset, Vector3.one);
    }
}

public enum SlapDirection
{
    Left,
    Right,
    All
}