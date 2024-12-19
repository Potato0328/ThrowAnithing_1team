using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    Coroutine loadingRoutine; //�ε� �ڷ�ƾ
    [SerializeField] Image loadingImage; //�ε��߿��� ��Ÿ�� �̹���
    [SerializeField] Slider loadingBar; //�ε������� �˷��ִ� ���α׷��� ��


    private void Start()
    {
        loadingImage.gameObject.SetActive(false);
    }


    public void ChangeScene(int SceneNum)
    {
        if (loadingRoutine != null)
            return;

        loadingImage.gameObject.SetActive(true);
        loadingRoutine = StartCoroutine(LoadingRoutine(SceneNum));
    }


    IEnumerator LoadingRoutine(int SceneNum)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(SceneNum);

        oper.allowSceneActivation = false;


        while (oper.isDone == false)
        {

            if (oper.progress < 0.9f)
            {
                //�ε���
                Debug.Log($"loading = {oper.progress}");
                loadingBar.value = oper.progress;
            }
            else
            {
                //�ε� �Ϸ� (�ٷ� �Ѿ���� �ڵ�)
                //Debug.Log("loading success");
                //oper.allowSceneActivation = true;
                break;
            }
            yield return null;
        }


        //����ũ �ε� (������ �ε��� ä��� �����ӹ���)
        float time = 0f;
        while (time < 5f)
        {
            time += Time.deltaTime;
            loadingBar.value = time / 5f;
            yield return null;
        }
        Debug.Log("loading success");
        while (time >= 5f)
        {            

            if (Input.GetKeyDown(KeyCode.Return))
            {
                oper.allowSceneActivation = true;

            }

            yield return null;
        }

    }



}
