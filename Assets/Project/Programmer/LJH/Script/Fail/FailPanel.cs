using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class FailPanel : MonoBehaviour
{
    [Inject]
    private PlayerData playerData;
    [SerializeField] Failure failure;
    [SerializeField] Result result;


    private void Start()
    {
        this.UpdateAsObservable()
            .Where(x => playerData.IsDead == true)
            .Subscribe(x => failure.gameObject.SetActive(true));
    }
}
