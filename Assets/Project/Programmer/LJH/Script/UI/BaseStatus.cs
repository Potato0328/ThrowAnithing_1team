using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseStatus : MonoBehaviour
{
    //string Key�� ���� ������Ʈ Value�� ������ ��ųʸ�

    protected Dictionary<string, Image> _imageDict;

    //string Key�� Component Value�� ������ ��ųʸ�

    private Dictionary<(string, System.Type), Component> _componentDict;

    protected void Bind()
    {

        //��� UI ��ҵ��� ������

        //Transform���� �������� ������ Transform�� ���� ������Ʈ�� ���� ������.

        //��Ȱ��ȭ �Ǿ� �ִ� ������Ʈ�� �������� �ʴ°� �⺻ �����̶� true�� �Ҵ�

        Transform[] _transforms = GetComponentsInChildren<Transform>(true);

        //��ųʸ��� �뷮�� _transforms �迭�� 4��� ����

        _imageDict = new Dictionary<string, Image>(_transforms.Length << 2);

        foreach (Transform child in _transforms)
        {
            //�Ȱ��� �̸��� �ִ� ���� �����ؼ� Try�� �߰�
            _imageDict.TryAdd(child.gameObject.name, child.gameObject.GetComponent<Image>());
        }

        _componentDict = new Dictionary<(string, System.Type), Component>();

    }

    protected void BindAll()
    {
        Transform[] _transforms = GetComponentsInChildren<Transform>(true);

        //��ųʸ��� �뷮�� _transforms �迭�� 4��� ����

        _imageDict = new Dictionary<string, Image>(_transforms.Length << 2);

        foreach (Transform child in _transforms)
        {
            //�Ȱ��� �̸��� �ִ� ���� �����ؼ� Try�� �߰�
            _imageDict.TryAdd(child.gameObject.name, child.gameObject.GetComponent<Image>());

        }

        Component[] components = GetComponentsInChildren<Component>(true);

        _componentDict = new Dictionary<(string, System.Type), Component>(components.Length << 4);

        foreach (Component child in components)
        {
            _componentDict.TryAdd((child.gameObject.name, components.GetType()), child);

        }

    }



    //�̸��� name�� UI ���ӿ�����Ʈ ��������

    //GetUI("Key 01") > Key 01 �� ���ӿ�����Ʈ ��������

    public Image GetImage(in string name)
    {
        _imageDict.TryGetValue(name, out Image obj);

        return obj;
    }

    //�̸��� Name�� UI���� ������Ʈ T ��������

    //GetUI<Image>("Key") : Key�̸��� ���ӿ�����Ʈ���� Image ������Ʈ ������

    public T GetUI<T>(in string name) where T : Component
    {
        //Ŭ������ �̸��� typeOf(T).Name ���� ã�� �� ����

        //������Ʈ ��ųʸ� Ű�� ����� Ʃ��
        (string, System.Type) _key = (name, typeof(T));

        // 1. Component ��ųʸ��� ���� �� (ã�ƺ����� ���� ����) > ã�� �� ��ųʸ��� �߰��ϰ� ��
        _componentDict.TryGetValue(_key, out Component component);

        if (component != null)
            return component as T;

        // 2. Component ��ųʸ��� �̹� ���� �� (ã�ƺ����� �ִ� ����) > ã�Ҵ��� ��

        _imageDict.TryGetValue(name, out Image go);

        if (go != null)
        {
            component = go.GetComponent<T>();

            if (component != null)
            {
                _componentDict.TryAdd(_key, component);
                return component as T;
            }
        }
        return null;
    }
}
