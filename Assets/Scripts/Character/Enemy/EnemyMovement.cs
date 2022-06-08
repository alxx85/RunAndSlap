using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _runOffset;
    [SerializeField] private float _startBoostRunOffset;
    [SerializeField] private float _speed;

    private NavMeshAgent _agent;
    private PlayerInteraction _player;
    private EnemyInteraction _interaction;
    private float _boostSpeed = 3f;
    private bool _isChase = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!_isChase)
        {
            return;
        }

        if (_interaction.IsAlive == false)
        {
            _agent.velocity = Vector3.zero;
            _agent.isStopped = true;
        }

        if (_player != null)
        {
            _agent.destination = _player.transform.position;
            float distance = _player.transform.position.z - transform.position.z;

            if (distance > _startBoostRunOffset)
            {
                _agent.speed = _speed + _boostSpeed;
            }
            
            if (distance <= _runOffset)
            {
                _agent.speed = _speed;
            }
        }
    }

    private void OnDisable()
    {
        if (_player != null)
            _player.Dying -= OnDyingPlayer;
    }

    public void Init(PlayerInteraction player, EnemyInteraction interaction)
    {
        _player = player;
        _interaction = interaction;
        _agent.speed = _speed;
        _player.Dying += OnDyingPlayer;
        _isChase = true;
    }

    private void OnDyingPlayer()
    {
        _player.Dying -= OnDyingPlayer;
        _player = null;
    }
}
