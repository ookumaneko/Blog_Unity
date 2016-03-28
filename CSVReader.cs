using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// CSVファイルを読み込み、データを保持するクラス
/// </summary>
public class CSVReader
{
    /// <summary>
    /// CSVファイルの拡張子
    /// </summary>
	public const string _EXTENSION = ".csv";

    /// <summary>
    /// セルを区切る為にキャラ
    /// </summary>
    public const char _SPLIT_CHAR = ',';

    /// <summary>
    /// 行がコメントだと言う事を表す文字列
    /// </summary>
	string m_commentString = "//";

    // 読み込んだデータ
    List<List<string>> m_data = new List<List<string>>();
    TextAsset m_textAsset = null;
    bool m_isReadFromDisc;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="comment">コメントの文字列</param>
    /// <param name="isReadFromDisc">Resourcesでなくディスク(フルパス)から読み込むか</param>
	public CSVReader(string comment = "//", bool isReadFromDisc = false)
	{
        m_textAsset = null;
		m_data = new List<List<string>>(10);
		m_commentString = comment;
        m_isReadFromDisc = isReadFromDisc;
	}
		
	public bool Load(string fileName) 
	{			
		m_data.Clear();
        TextReader reader = CreateTextReader(fileName);

		int counter = 0;
		string line = "";
		while ( ( line = reader.ReadLine()) != null ) 
		{
            // コメントが入っている時はスキップする
			if ( line.Contains( m_commentString ) )
			{
				continue;
			}
			
            // 今の列をマス毎に区切る
			string[] fields = line.Split( _SPLIT_CHAR );
			m_data.Add( new List<string>() );

			foreach ( var field in fields )
			{
                if (field.Contains(m_commentString) || field == "")
				{
					continue;	
				}
				m_data[ counter ].Add( field );
			}
			counter++;
		}

        // 読み込んだリソースを開放する
        Resources.UnloadAsset(m_textAsset);
        m_textAsset = null;
		Resources.UnloadUnusedAssets();
		return true;
	}

    private TextReader CreateTextReader(string fileName)
    {
        if (m_isReadFromDisc)
        {
            return new StreamReader(fileName);
        }

        m_textAsset = Resources.Load<TextAsset>(fileName);
        return new StringReader(m_textAsset.text);
    }

	public void Clear()
	{
		m_data.Clear();	
	}

	public int RowCount
	{
        get { return m_data.Count; }
	}
		
	public int GetColumnCount(int row)
	{
        if (RowCount == 0)
		{
			return 0;
		}
			
		return m_data[ row ].Count;
	}

    public List<string> GetData(int row)
    {
        return m_data[row];
    }

	public string GetString(int row, int col)
	{
		return m_data[ row ][ col ];	
	}

    public bool GetBool(int row, int col)
    {
        string data = GetString(row, col);
        return bool.Parse( data );
    }

	public int GetInt(int row, int col)
	{
		string data = GetString( row, col );
		return int.Parse( data );
	}
		
	public float GetFloat(int row, int col)
	{
		string data = GetString( row, col );
		return float.Parse( data );
	}

    public void SetData(string[,] data)
    {
        int height = data.GetLength(0);
        int width = data.GetLength(1);

        for (int y = 0; y < height; ++y)
        {
            m_data.Add(new List<string>());
            for (int x = 0; x < width; ++x)
            {
                m_data[y].Add(data[y, x]);   
            }
        }
    }
}

