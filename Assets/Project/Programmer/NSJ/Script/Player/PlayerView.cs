using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour
{
    public PlayerPanel Panel;
    public enum Parameter
    {
        AttackSpeed,
        Idle,
        Run,
        OnCombo,
        Jump,
        DoubleJump,
        Fall,
        Landing,
        Dash,
        Drain,
        Charge,
        ChargeEnd,
        BasicMelee,
        BasicThrow,
        PowerMelee,
        PowerThrow,
        Size
    }

    private bool _isAnimationFinish;
    public bool IsAnimationFinish
    {
        get
        {
            bool answer = _isAnimationFinish;
            if (_isAnimationFinish == true)
            {
                _isAnimationFinish = false;
            }
            return answer;
        }
        set
        {
            _isAnimationFinish = value;
        }
    }


    [SerializeField] Animator _animator;

    private int[] _animatorHashes = new int[(int)Parameter.Size];

    private void Awake()
    {
        Init();
    }
    // 애니메이션 =========================================================================================================//
    /// <summary>
    /// 플레이어 애니메이션 SetTrigger
    /// </summary>
    public void SetTrigger(Parameter animation)
    {
        _animator.SetTrigger(_animatorHashes[(int)animation]);
    }

    /// <summary>
    /// 플레이어 애니메이션 SetInteger
    /// </summary>
    public void SetInteger(Parameter animation, int value)
    {
        _animator.SetInteger(_animatorHashes[(int)animation], value);
    }

    public int GetInteger(Parameter animation)
    {
        return _animator.GetInteger(_animatorHashes[(int)animation]);
    }

    /// <summary>
    /// 플레이어 애니메이션 SetBool
    /// </summary>
    public void SetBool(Parameter animation, bool value)
    {
        _animator.SetBool(_animatorHashes[(int)animation], value);
    }

    public bool GetBool(Parameter animation)
    {
        return _animator.GetBool(_animatorHashes[(int)animation]);
    }

    /// <summary>
    /// 플레이어 애니메이션 SetFloat
    /// </summary>
    public void SetFloat(Parameter animation, float value)
    {
        _animator.SetFloat(_animatorHashes[(int)animation], value);
    }
    public float GetFloat(Parameter animation)
    {
        return _animator.GetFloat(_animatorHashes[(int)animation]);
    }

    // UI ================================================================================================================//

    public void UpdateText(TMP_Text target, string text)
    {
        target.SetText(text);
    }

    private void Init()
    {
        _animatorHashes[(int)Parameter.AttackSpeed] = Animator.StringToHash("AttackSpeed");
        _animatorHashes[(int)Parameter.Idle] = Animator.StringToHash("Idle");
        _animatorHashes[(int)Parameter.Run] = Animator.StringToHash("Run");
        _animatorHashes[(int)Parameter.OnCombo] = Animator.StringToHash("OnCombo");
        _animatorHashes[(int)Parameter.Jump] = Animator.StringToHash("Jump");
        _animatorHashes[(int)Parameter.DoubleJump] = Animator.StringToHash("DoubleJump");
        _animatorHashes[(int)Parameter.Fall] = Animator.StringToHash("Fall");
        _animatorHashes[(int)Parameter.Landing] = Animator.StringToHash("Landing");
        _animatorHashes[(int)Parameter.Dash] = Animator.StringToHash("Dash");
        _animatorHashes[(int)Parameter.Drain] = Animator.StringToHash("Drain");
        _animatorHashes[(int)Parameter.Charge] = Animator.StringToHash("Charge");
        _animatorHashes[(int)Parameter.ChargeEnd] = Animator.StringToHash("ChargeEnd");
        _animatorHashes[(int)Parameter.BasicMelee] = Animator.StringToHash("BasicMelee");
        _animatorHashes[(int)Parameter.BasicThrow] = Animator.StringToHash("BasicThrow");
        _animatorHashes[(int)Parameter.PowerMelee] = Animator.StringToHash("PowerMelee");
        _animatorHashes[(int)Parameter.PowerThrow] = Animator.StringToHash("PowerThrow");

    }
}
