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

    private PlayerController _player;
    private bool isPlayerNearby = false; // �÷��̾� ��ó�� �ִ��� Ȯ��
    private bool isTyping = false; // ���� Ÿ���� ������ Ȯ��
    private int currentDialogueIndex = 0; // ���� ��� �ε���

    private void Awake()
    {
        // PlayerController ã��
        _player = GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<PlayerController>();
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

    private void Update()
    {
        if (!isPlayerNearby)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
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
        _player.CantOperate = true; // �÷��̾� ���� ��Ȱ��ȭ
    }

    private void HideDialogueUI()
    {
        dialogueUI.SetActive(false);
        _player.CantOperate = false; // �÷��̾� ���� Ȱ��ȭ
    }

    private void StartDialogue()
    {
        currentDialogueIndex = 0;
        StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue()
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in dialogues[currentDialogueIndex])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);

            // Ÿ���� �߿� e ��ư�� ������ ��� ��ü ���
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                dialogueText.text = dialogues[currentDialogueIndex];
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
            dialogueText.text = dialogues[currentDialogueIndex];
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
}
