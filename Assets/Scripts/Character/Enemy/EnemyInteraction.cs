using UnityEngine;

[RequireComponent(typeof(Animator), typeof(EnemyMovement))]
public class EnemyInteraction : Interaction
{
    [SerializeField] private ParticleSystem _vfx_Emoji;
    [SerializeField] private ParticleSystem _vfx_Slap;
    [SerializeField] private Animations _startAnimation;

    private Animator _animator;
    private PlayerInteraction _player;
    private EnemyMovement _movement;
    private float _startRunDelay = 1.2f;
    private float _vfxSlapDelay = 0.2f;
    private Vector3 _vfxSlapPosition = new Vector3(0f, 1f, -.25f);
    private Vector3 _vfxEmojiPosition = new Vector3(0f, 2f, -.05f);
    private Vector3 _offset = new Vector3(0f, 0f, -6.5f);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(_startAnimation.ToString(), true);
        _movement = GetComponent<EnemyMovement>();
    }

    protected override void VFXDamage()
    {
        ShowVFX();
    }

    public void Slap(PlayerInteraction player)
    {
        _player = player;
        Invoke(nameof(ShowVFX), _vfxSlapDelay);
        _ragDoll.Activate();
        Invoke(nameof(StartRun), _startRunDelay);
    }

    private void StartRun()
    {
        _ragDoll.Activate();
        Vector3 startPosition = _player.transform.position + _offset;
        transform.rotation = Quaternion.identity;
        transform.position = startPosition;
        _startAnimation = Animations.Run;
        _animator.SetBool(_startAnimation.ToString(), true);
        _movement.Init(_player, this);
    }

    private void ShowVFX()
    {
        if (_vfx_Emoji != null)
            VfxViewer(_vfx_Emoji, _vfxEmojiPosition);

        VfxViewer(_vfx_Slap, _vfxSlapPosition);
    }

    private void VfxViewer(ParticleSystem vfx, Vector3 offset)
    {
        Vector3 position = transform.position + offset;
        Instantiate(vfx, position, Quaternion.identity, null);
    }

}

public enum Animations
{
    Stay,
    Hiphop,
    Talk,
    Walk,
    Rumba,
    Run
}