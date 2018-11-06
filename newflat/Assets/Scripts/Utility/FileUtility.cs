using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public static class FileUtils  {


	public static string GetFileName(string path)
	{
		//string path ="http://www.rr.com/fdff.unity3d";
		int location = path.LastIndexOf ("/");
		path = path.Substring (location+1);
		string[] result = path.Split ('.');

		if (result.Length == 2) {
			return  result [0].Trim();
		} else {
			return string.Empty;
		}
	}

	public enum WriteType
	{
		/// <summary>
		/// 写入
		/// </summary>
		Write,
		/// <summary>
		/// 追加
		/// </summary>
		AddTo
	}





	/// <summary>
	/// 一行一行的读取输出。
	/// </summary>
	/// <param name="path">路径</param>
	public static string[] ReadTextContent(string path)
	{

		try
		{
			StreamReader sr = new StreamReader(path, Encoding.UTF8);
			string line;
			List<string> contentNumber = new List<string>();
			while ((line = sr.ReadLine()) != null)
			{
				contentNumber.Add(line.Trim());
			}
			return  contentNumber.ToArray();

		}
		catch
		{
			Debug.Log("读取路径有误！" + path);
		}
		return new string[0];
	}

	public static  string ReadAllContent(string path)
	{
		return File.ReadAllText (path);
	}

	/// <summary>
	/// 往文本写入数据
	/// \r\n在内容中代表换行 如："123\r\n456"
	/// </summary>
	/// <param name="path">路径</param>
	/// <param name="type">写入类型</param>
	/// <param name="content">需要写入内容  \r\n在内容中输入这个代表换行 如："123\r\n456"</param>
	public static void WriteContent(string path, WriteType type, string content)
	{
		try
		{
			#if UNITY_STANDALONE_WIN
			StreamWriter sw = null;
			string w = "";
			switch (type)
			{
			case WriteType.Write:
				//表示向txt写入文本
				sw = new StreamWriter(path);
				w = content;
				break;
			case WriteType.AddTo:
				//表示追加文本
				sw = File.AppendText(path);
				w = content;
				break;

			}
			sw.Write(w);
			sw.Close();
			#endif
		}
		catch
		{
			Debug.Log("写入路径有误！");
		}
	}
	/// <summary>
	/// 获取路径下的所有的打包文件
	/// </summary>
	/// <returns>The asset bundle scene name.</returns>
	/// <param name="path">Path.</param>
	/// <param name="extendFileName">Extend file name.</param>
	public static string[] ReadAssetBundleSceneName(string path,string extendFileName)
	{

		List<string> sceneName = new List<string>();
		try
		{
			#if UNITY_STANDALONE_WIN
			string[] arrStrAudioPath = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
			for (int i = 0; i < arrStrAudioPath.Length; i++)
			{
				if (System.IO.Path.GetExtension(arrStrAudioPath[i]) == extendFileName)
				{
					arrStrAudioPath[i] = arrStrAudioPath[i].Replace("\\", "/");
					string[] _filePath = arrStrAudioPath[i].Split('/');
					string[] _fileName = _filePath[_filePath.Length - 1].Split('.');
					//Debug.Log("遍历文件获取文件名：" + _fileName[0]);
					//Debug.Log("遍历文件获取文件路径：" + arrStrAudioPath[i]);
					sceneName.Add(_fileName[0]);
				}
			}
			#endif

			return sceneName.ToArray();
		}
		catch
		{
			Debug.Log("路径不对：" + path);
		}
		return sceneName.ToArray();
	}


	/// 获取文件大小—字节
	/// </summary>
	/// <param name="filePath">文件路径</param>
	/// <returns>文件大小</returns>
	public static long GetSize(string filePath)
	{
		long _size = 0;
		try
		{
			if (File.Exists(filePath))
			{
				FileStream _stream = new FileStream(filePath, FileMode.Open);
				_size = _stream.Length;
				_stream.Close();
				_stream.Dispose();
			}
		}
		catch (Exception ex)
		{
			_size = 0;
		}
		return _size;
	}

	/// <summary>
	/// 获取文件大小—kb
	/// </summary>
	/// <param name="filePath">文件路径</param>
	/// <returns>文件大小_kb</returns>
	public static double GetKBSize(string filePath)
	{
		double _kb = 0;
		long _size = GetSize(filePath);
		if (_size != 0)
		{
			_kb = _size / 1024d;
		}
		return _kb;
	}

	/// <summary>
	/// 获取文件大小—mb
	/// </summary>
	/// <param name="filePath">文件路径</param>
	/// <returns>文件大小_mb</returns>
	public static double GetMBSize(string filePath)
	{
		double _mb = 0;
		long _size = GetSize(filePath);
		if (_size != 0)
		{
			_mb = _size / 1048576d;//1024*1024==1048576;
		}
		return _mb;
	}

	/// <summary>
	/// 获取目录下所有的文件
	/// </summary>
	/// <param name="path">文件夹目录</param>
	public static List<string> GetFilesByDirectory(string path)
	{
		#if UNITY_STANDALONE_WIN
		List<string> result = new List<string>();
		string[] arrStrAudioPath = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
		for (int i = 0; i < arrStrAudioPath.Length; i++)
		{
			result.Add(arrStrAudioPath[i]);
		}
		return result;
		#else

		Application.ExternalCall("GetFiles", path);
		return null;

		#endif
	}

	/// <summary>
	/// 通过路径截取文件名字
	/// </summary>
	/// <param name="filePath">文件的完整路径</param>
	public static string GetFileNameByPath(string filePath)
	{
		return System.IO.Path.GetFileName(filePath);
	}
	/// <summary>
	/// 通过路径获取文件的全名
	/// </summary>
	/// <returns>The file name by full path.</returns>
	/// <param name="filePath">File path.</param>
	public static string GetFileNameByFullPath(string filePath)
	{

		return   filePath.Substring(filePath.LastIndexOf("\\") + 1); 
	}


	/// <summary>
	/// c#对象的克隆 
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static object Clone(object obj)
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(memoryStream, obj);
		memoryStream.Position = 0;
		return formatter.Deserialize(memoryStream);
	}
}
