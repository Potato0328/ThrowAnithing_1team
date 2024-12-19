using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private string savePath; // �ʵ� �ʱ�ȭ �� Application.persistentDataPath ȣ�� ����

    private void Awake()
    {
        // Unity API�� Awake �Ǵ� Start���� ȣ��
        savePath = Application.persistentDataPath + "/save";
    }

    public override void InstallBindings()
    {
        // UserDataManager�� savePath�� ����
        Container.Bind<string>().FromInstance(savePath).AsSingle(); // ��� ����
        Container.Bind<UserDataManager>().FromComponentInNewPrefabResource("UserDataManagerPrefab").AsSingle(); // UserDataManager �ν��Ͻ� ����
        Container.Bind<SlotManager>().FromComponentInHierarchy().AsSingle(); // SlotManager ����
    }
}
