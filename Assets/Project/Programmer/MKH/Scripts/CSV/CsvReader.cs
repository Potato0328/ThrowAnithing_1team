using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// CSV 파일을 읽어 Dictionary 구조로 로드하는 정적 클래스.
/// </summary>
public static class CsvReader
{
    /// <summary>
    /// 파일을 로드할 기본 경로를 가져옴.
    /// </summary>
    public static string BasePath
    {
        get
        {
            return Application.dataPath + Path.DirectorySeparatorChar;
        }
    }

    /// <summary>
    /// CSV 파일을 읽고 적절한 데이터 구조로 로드한다.
    /// </summary>
    public static void Read(Csv csv)
    {
        if (!IsValidPath(csv) ||
            !IsValidEmpty(csv, out string[] lines))
            return;

        bool isReadSuccessful;

        switch (csv)
        {
            // 딕셔너리 구조로 로드
            case CsvDictionary dictionary:
                isReadSuccessful = ReadToDictionary(dictionary, lines);
                break;
            default:
                isReadSuccessful = false;
                break;
        }

        PrintResult(csv, isReadSuccessful);
    }

    /// <summary>
    /// CSV 파일을 읽어 CsvDictionary로 로드한다.
    /// </summary>
    /// <param name="csv">데이터를 로드할 CsvDictionary 객체.</param>
    /// <param name="lines">CSV 파일에서 읽은 줄.</param>
    private static bool ReadToDictionary(CsvDictionary csv, string[] lines)
    {
        // 첫번째 줄을 SplitSymbol을 기준으로 나누어서 헤더 배열 생성
        string[] fieldsIndex = lines[0].Split(csv.SplitSymbol);
        // CSV 열 개수
        int columns = fieldsIndex.Length;
        // 딕셔너리 초기화
        csv.Dict = new Dictionary<string, Dictionary<string, string>>();

        // 첫째 줄 제외
        for (int i = 1; i < lines.Length; i++)
        {
            // SplitSymbol을 기준으로 줄 나누기
            string[] fields = lines[i].Split(csv.SplitSymbol);

            // 현재 줄의 열 개수가 columns보다 적으면 false
            if (fields.Length < columns)
            {
                return false;
            }

            // 첫번째 열을 행의 키로 사용
            string rowKey = fields[0];
            // rowKey를 기준으로 내부 딕셔너리 생성
            csv.Dict[rowKey] = new Dictionary<string, string>(columns);

            // 헤더를 키로 하고, 해당 필드 값을 내부 딕셔너리에 저장
            for (int j = 1; j < columns; j++)
            {
                csv.Dict[rowKey][fieldsIndex[j]] = fields[j];
            }
        }

        return true;
    }

    /// <summary>
    /// 지정된 경로에 파일이 존재하는지 검증한다.
    /// </summary>
    /// <param name="csv">확인할 CSV 객체.</param>
    private static bool IsValidPath(Csv csv)
    {
        // 파일 존재 X -> false
        if (!File.Exists(csv.FilePath))
        {
            Debug.LogError($"Error: CSV file not found at path: {csv.FilePath}");
            return false;
        }
        // 파일 존재 -> true
        return true;
    }

    /// <summary>
    /// 파일의 모든 줄을 읽고 비어 있는지 확인한다.
    /// </summary>
    private static bool IsValidEmpty(Csv csv, out string[] lines)
    {
        lines = File.ReadAllLines(csv.FilePath);

        if (lines.Length == 0)
        {
            Debug.LogError($"Error: CSV file at path {csv.FilePath} is empty.");
            return false;
        }
        return true;
    }

    /// <summary>
    /// CSV 파일 로딩 프로세스의 결과를 로그로 출력한다.
    /// </summary>
    private static void PrintResult(Csv csv, bool result)
    {
        // csv 로딩 성공
        if (result)
        {
            Debug.Log($"Successfully loaded CSV file from path: {csv.FilePath}");
        }
        // csv 로딩 실패
        else
        {
            Debug.LogError($"Error: CSV file at path {csv.FilePath} has inconsistent column lengths.");
        }
    }
}
