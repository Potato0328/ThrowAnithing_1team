using System.IO;
using UnityEngine;

/// <summary>
/// CSV 구조를 위한 기본 클래스.
/// 이 클래스는 CSV 파일 경로와 구분 기호를 처리하기 위한 공통 속성과 메서드를 제공한다.
/// - CSV 데이터의 파일 경로와 구분 기호 설정을 처리한다.
/// </summary>
public class Csv
{
    // 파일 위치
    [SerializeField] private string _filePath;
    public string FilePath => Path.Combine(CsvReader.BasePath, _filePath);

    // 구분 기호
    [field: SerializeField] public char SplitSymbol { get; private set; }

    // 기본 CSV
    protected Csv(string path, char splitSymbol)
    {
        _filePath = path;
        SplitSymbol = splitSymbol;
    }
}
