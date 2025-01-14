using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class TalkInteractable : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject upPopup; // ������ ���� ������ ����â UI
    [SerializeField] private GameObject dialogueUI; // ��ȭâ UI
    [SerializeField] private TextMeshProUGUI dialogueText; // ��� ��� �ؽ�Ʈ
    [SerializeField] private string[] dialogues; // ��� ���
    [SerializeField] private float typingSpeed = 0.05f; // Ÿ���� �ӵ�

    [SerializeField] PlayerInput playerInput; // Player Input ������Ʈ
    private bool isPlayerNearby = false; // �÷��̾� ��ó�� �ִ��� Ȯ��
    private bool isTyping = false; // ���� Ÿ���� ������ Ȯ��
    private int currentDialogueIndex = 0; // ���� ��� �ε���

    private void Awake()
    {
        playerInput = GameObject.Find("InputManager").GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.Log("PlayerInput �ȵ�");
        }
    }
    // �浹������ �ȳ�ui ���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            ShowPopup();
            isPlayerNearby = true;
        }
    }
    // �������� �ȳ� ui ��Ȱ��ȭ
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            HidePopup();
            isPlayerNearby = false;
        }
    }
    // gameplay�ʿ��� Interaction�׼��� ����ɶ�
    private void Update()
    {
        if(playerInput.actions["Interaction"].WasPressedThisFrame() || playerInput.actions["Choice"].WasPressedThisFrame())
        {
            Debug.Log("1���õ�");
            if (isPlayerNearby)
            {
                Debug.Log("2���õ�");
                if (dialogueUI.activeSelf)
                {
                    // ��ȭâ�� ���������� ��ȭ ��ŵ
                    HandleDialogueProgress();
                }
                else
                {
                    // ��ȭâ�� ���������� ��ȭâ���� ���� ��ȭ ����
                    Debug.Log("3���õ�");
                    ShowDialogueUI();
                    StartDialogue();
                }
            }
        }        
    }

    private void ShowPopup()
    {
        upPopup.SetActive(true);
    }

    private void HidePopup()
    {
        upPopup.SetActive(false);
    }

    private void ShowDialogueUI()
    {
        dialogueUI.SetActive(true);
        playerInput.SwitchCurrentActionMap(ActionMap.UI); // �÷��̾� ���� ��Ȱ��ȭ
        Debug.Log(playerInput.currentActionMap);
    }

    private void HideDialogueUI()
    {
        dialogueUI.SetActive(false);
        playerInput.SwitchCurrentActionMap(ActionMap.GamePlay); // �÷��̾� ���� Ȱ��ȭ
        Debug.Log(playerInput.currentActionMap);
    }

    private void StartDialogue()
    {
        currentDialogueIndex = 0;
        StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue()
    {
        isTyping = true;
        string currentDialogue = dialogues[currentDialogueIndex];
        dialogueText.text = ""; // ��� �ʱ�ȭ

        foreach (char c in currentDialogue)
        {
            dialogueText.text += c; // �ϳ��� ���
            yield return new WaitForSeconds(typingSpeed);

            // Ÿ���� �߿� 'Choice' �׼��� Ʈ���ŵǸ� ��� ���
            if (playerInput.actions["Choice"].triggered)
            {
                dialogueText.text = currentDialogue; // ��ü ��� ���
                break;
            }
        }

        isTyping = false;
    }

    private void HandleDialogueProgress()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogues[currentDialogueIndex];
            isTyping = false;
        }
        else
        {
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogues.Length)
            {
                StartCoroutine(TypeDialogue());
            }
            else
            {
                HideDialogueUI();
            }
        }
    }
}
