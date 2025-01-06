using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ApprenticeWizard", menuName = "AdditionalEffect/Player/ApprenticeWizard")]
public class ApprenticeWizardAddtional : PlayerAdditional
{
    [Header("ȸ�� ������(%)")]
    [SerializeField] private float _recoveryAmount;
    public override void Trigger()
    {
        if (Player.CurState != PlayerController.State.SpecialAttack)
            return;
        RecoverMana();
    }

    private void RecoverMana()
    {
        float recoveryMana = Model.MaxMana * (_recoveryAmount / 100);
        Model.CurMana += recoveryMana;
    }
}
