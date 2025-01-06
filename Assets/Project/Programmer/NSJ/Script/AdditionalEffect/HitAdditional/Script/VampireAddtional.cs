using UnityEngine;

[CreateAssetMenu(fileName = "Vampire", menuName = "AdditionalEffect/Hit/Vampire")]
public class VampireAddtional : HitAdditional
{
    [Header("���� ������(%)")]
    [SerializeField] private float _lifeDrainAmount;
    public override void Enter()
    {
        DrainLife();
        Battle.EndDebuff(this);
    }

    private void DrainLife()
    {
        if (_isCritical == false)
            return;

        int lifeAmount = (int)(_damage * (_lifeDrainAmount / 100));
        // TODO : ü���� �ö󰡴� ����� �ʿ���
        // ���� �ϰ� �ϴ� �������̽��� �� �ʿ��� ��
        Debug.Log($"{lifeAmount} ��ŭ ȸ��. ���� ��� ����");
    }
}
