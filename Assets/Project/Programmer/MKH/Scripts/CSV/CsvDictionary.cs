using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CSV 파일을 string 기반 Dictionary 구조로 표현하는 클래스.
/// 이 클래스는 CSV 데이터를 행과 열 키를 통해 접근할 수 있도록 한다.
/// </summary>
[System.Serializable]
public class CsvDictionary : Csv
{
    public Dictionary<string, Dictionary<string, string>> Dict { get; set; }
    public CsvDictionary(string path, char splitSymbol) : base(path, splitSymbol) { }

    /// <summary>
    /// Dictionary에서 특정 셀의 데이터를 가져옴.
    /// </summary>
    /// <param name="row">The row key of the cell. 셀의 행 키.</param>
    /// <param name="column">The column key of the cell. 셀의 열 키.</param>
    public string GetData(string row, string column)
    {
        if (Dict.TryGetValue(row, out var columns) && columns.TryGetValue(column, out var value))
        {
            return value;
        }

        // 지정된 행과 열의 데이터 또는 없으면 null 반환
        Debug.LogError($"Data not found for row '{row}' and column '{column}'.");
        return null;
    }
}