using MKH;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [HideInInspector] public InventoryController Controller;
    [HideInInspector] public EquipmentInventory EquipInventory;
    [HideInInspector] public InventoryMain InventoryMain;
    [SerializeField] public GameObject BlueChipChoice;
    [HideInInspector] public BlueChipPanel BlueChipPanel;

    [Inject]
    PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        // �ӽ÷� �̱��� ���Ƴ���� �ļ�
        // TODO : ���� ���Ĩ �ʱ�ȭ ��� ������ ���� �ʿ�
        else if (SceneManager.GetActiveScene().name == SceneName.LobbyScene)
        {        
            Destroy(Instance.gameObject);
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitGetComponent();


        playerData.Inventory.Inventory = EquipInventory;
        playerData.Inventory.InventoryMain = InventoryMain;
        playerData.Inventory.BlueChipChoice = BlueChipChoice;
        playerData.Inventory.BlueChipPanel = BlueChipPanel;
    }

    private void InitGetComponent()
    {
        Controller = GetComponentInChildren<InventoryController>();
        EquipInventory = GetComponentInChildren<EquipmentInventory>();
        InventoryMain = GetComponentInChildren<InventoryMain>();
        BlueChipPanel = GetComponentInChildren<BlueChipPanel>();
    }

    private void InitSingleTon()
    {

    }
}
