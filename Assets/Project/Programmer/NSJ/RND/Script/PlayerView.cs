using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public enum Parameter { MoveSpeed, MeleeAttack, Size }

    public bool IsAnimationFinish;

    [SerializeField] PlayerPanel _panel;

    [SerializeField] Animator _animator;

    private int[] _animatorHashes = new int[(int)Parameter.Size];

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// �÷��̾� �ִϸ��̼� SetTrigger
    /// </summary>
    public void SetTrigger(Parameter animation)
    {
        _animator.SetTrigger(_animatorHashes[(int)animation]);
    }

    /// <summary>
    /// �÷��̾� �ִϸ��̼� SetInteger
    /// </summary>
    public void SetInteger(Parameter animation, int value)
    {
        _animator.SetInteger(_animatorHashes[(int)animation], value);
    }

    /// <summary>
    /// �÷��̾� �ִϸ��̼� SetBool
    /// </summary>
    public void SetBool(Parameter animation, bool value)
    {
        _animator.SetBool(_animatorHashes[(int)animation], value);
    }

    /// <summary>
    /// �÷��̾� �ִϸ��̼� SetFloat
    /// </summary>
    public void SetFloat(Parameter animation, float value)
    {
        _animator.SetFloat(_animatorHashes[(int)animation], value);
    }

    public void SetIsAnimationFinish()
    {
        IsAnimationFinish = true;
    }

    private void Init()
    {
        _animatorHashes[(int)Parameter.MoveSpeed] = Animator.StringToHash("IsMove");
        _animatorHashes[(int)Parameter.MeleeAttack] = Animator.StringToHash("MeleeAttack");
    }
}
