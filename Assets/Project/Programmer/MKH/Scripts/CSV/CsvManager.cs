using System;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    public class CsvManager : MonoBehaviour
    {
        private static CsvManager instance;
        public static CsvManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("CsvManager");
                    instance = obj.AddComponent<CsvManager>();
                    DontDestroyOnLoad(obj);
                }
                return instance;
            }
        }

        private readonly Dictionary<ItemType, CsvDictionary> csvData = new();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                CsvData();
            }
            else
            {
                Destroy(gameObject);
            }

        }

        private void CsvData()
        {
            AddCsv(ItemType.Earring, "Import/DataTable/EquipCSV - Earring.csv");
            AddCsv(ItemType.Glasses, "Import/DataTable/EquipCSV - Glasses.csv");
            AddCsv(ItemType.Gloves, "Import/DataTable/EquipCSV - Gloves.csv");
            AddCsv(ItemType.Helmet, "Import/DataTable/EquipCSV - Helmet.csv");
            AddCsv(ItemType.Necklace, "Import/DataTable/EquipCSV - Necklace.csv");
            AddCsv(ItemType.Pants, "Import/DataTable/EquipCSV - Pants.csv");
            AddCsv(ItemType.Ring, "Import/DataTable/EquipCSV - Ring.csv");
            AddCsv(ItemType.Shirts, "Import/DataTable/EquipCSV - Shirts.csv");
            AddCsv(ItemType.Shoes, "Import/DataTable/EquipCSV - Shoes.csv");
        }

        private void AddCsv(ItemType type, string path)
        {
            CsvDictionary csv = new CsvDictionary(path, ',');
            CsvReader.Read(csv);
            csvData[type] = csv;
        }

        public CsvDictionary GetCsv(ItemType type)
        {
            return csvData.TryGetValue(type, out var csv) ? csv : null;
        }
    }
}
