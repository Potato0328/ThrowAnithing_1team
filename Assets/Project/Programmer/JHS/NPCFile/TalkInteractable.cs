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

    private PlayerInput playerInput; // Player Input ������Ʈ
    private bool isPlayerNearby = false; // �÷��̾� ��ó�� �ִ��� Ȯ��
    private bool isTyping = false; // ���� Ÿ���� ������ Ȯ��
    private int currentDialogueIndex = 0; // ���� ��� �ε���

    private void Awake()
    {
        // PlayerInput ������Ʈ ��������
        playerInput = GameObject.FindGameObjectWithTag("InputKey").GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            ShowPopup();
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            HidePopup();
            HideDialogueUI();
            isPlayerNearby = false;
        }
    }
    private void OnEnable()
    {
        // PlayerInput�� Action Map�� ���� �̺�Ʈ ���
        playerInput.actions["Choice"].performed += OnChoicePerformed;
    }
    private void OnDisable()
    {
        // �̺�Ʈ ��� ����
        playerInput.actions["Choice"].performed -= OnChoicePerformed;
    }

    private void OnChoicePerformed(InputAction.CallbackContext context)
    {
        if (!isPlayerNearby)
            return;

        if (dialogueUI.activeSelf)
        {
            HandleDialogueProgress();
        }
        else
        {
            ShowDialogueUI();
            StartDialogue();
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
        SwitchActionMap("UI"); // �÷��̾� ���� ��Ȱ��ȭ
    }

    private void HideDialogueUI()
    {
        dialogueUI.SetActive(false);
        SwitchActionMap("Gameplay"); // �÷��̾� ���� Ȱ��ȭ
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
        dialogueUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ""; // ��� �ʱ�ȭ

        foreach (char c in currentDialogue)
        {
            dialogueUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text += c;
            yield return new WaitForSeconds(typingSpeed);

            // Ÿ���� �߿� ��� ��� ó��
            if (playerInput.actions["Choice"].triggered)
            {
                dialogueUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentDialogue;
                break;
            }
        }

        isTyping = false;
    }

    private void HandleDialogueProgress()
    {
        if (isTyping)
        {
            // Ÿ���� ���̶�� ��� ��� �Ϸ�
            StopAllCoroutines();
            dialogueUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = dialogues[currentDialogueIndex];
            isTyping = false;
        }
        else
        {
            // ���� ���� �̵�
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogues.Length)
            {
                StartCoroutine(TypeDialogue());
            }
            else
            {
                // ��� ��簡 ������ ��ȭâ �ݱ�
                HideDialogueUI();
            }
        }
    }
    private void SwitchActionMap(string mapName)
    {
        playerInput.SwitchCurrentActionMap(mapName);
    }
}
