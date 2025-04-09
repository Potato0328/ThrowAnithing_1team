using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvManager : MonoBehaviour
{
    private static CsvManager instance;
    public static CsvManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CsvManager();
            }
            return instance;
        }
    }

    [SerializeField] public CsvDictionary earring;
    [SerializeField] public CsvDictionary glasses;
    [SerializeField] public CsvDictionary gloves;
    [SerializeField] public CsvDictionary helmet;
    [SerializeField] public CsvDictionary necklace;
    [SerializeField] public CsvDictionary pants;
    [SerializeField] public CsvDictionary ring;
    [SerializeField] public CsvDictionary shirts;
    [SerializeField] public CsvDictionary shoes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        }

        CsvParse();
        CsvRead();
    }

    private void CsvParse()
    {
        earring = new("Import/DataTable/EquipCSV - Earring.csv", ',');
        glasses = new("Import/DataTable/EquipCSV - Glasses.csv", ',');
        gloves = new("Import/DataTable/EquipCSV - Gloves.csv", ',');
        helmet = new("Import/DataTable/EquipCSV - Helmet.csv", ',');
        necklace = new("Import/DataTable/EquipCSV - Necklace.csv", ',');
        pants = new("Import/DataTable/EquipCSV - Pants.csv", ',');
        ring = new("Import/DataTable/EquipCSV - Ring.csv", ',');
        shirts = new("Import/DataTable/EquipCSV - Shirts.csv", ',');
        shoes = new("Import/DataTable/EquipCSV - Shoes.csv", ',');
    }

    private void CsvRead()
    {
        CsvReader.Read(earring);
        CsvReader.Read(glasses);
        CsvReader.Read(gloves);
        CsvReader.Read(helmet);
        CsvReader.Read(necklace);
        CsvReader.Read(pants);
        CsvReader.Read(ring);
        CsvReader.Read(shirts);
        CsvReader.Read(shoes);
    }
}
