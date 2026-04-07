using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class DataSaveLoader : MonoBehaviour
{
    public static DataSaveLoader instance {  get; private set; }
    private readonly string _key = "12345678901234567890123456789012";
    private readonly string _iv = "1234567890123456";
    private string _savePath;
    private Dictionary<string, object> _saveData = new Dictionary<string, object>();

    void Awake()
    {
        instance = this;
        _savePath = Path.Combine(Application.persistentDataPath, "Ingatan.mind");
        LoadAllFromFile();
    }

    public void SetData(string key, object value)
    {
        if (_saveData.ContainsKey(key)) _saveData[key] = value;
        else _saveData.Add(key, value);

        SaveAllToFile();
    }

    public T GetData<T>(string key)
    {
        if (!_saveData.ContainsKey(key)) return default;

        try
        {
            string json = JsonConvert.SerializeObject(_saveData[key]);
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return default;
        }
    }

    private void SaveAllToFile()
    {
        try
        {
            string json = JsonConvert.SerializeObject(_saveData);
            string encrypted = Encrypt(json);
            File.WriteAllText(_savePath, encrypted);
        }
        catch (Exception e) { Debug.LogError(e.Message); }
    }

    private void LoadAllFromFile()
    {
        if (!File.Exists(_savePath)) return;

        try
        {
            string encrypted = File.ReadAllText(_savePath);
            string decrypted = Decrypt(encrypted);
            _saveData = JsonConvert.DeserializeObject<Dictionary<string, object>>(decrypted)
                        ?? new Dictionary<string, object>();
        }
        catch (Exception e) { Debug.LogError(e.Message); }
    }

    private string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = Encoding.UTF8.GetBytes(_iv);
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs)) sw.Write(plainText);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }

    private string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = Encoding.UTF8.GetBytes(_iv);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs)) return sr.ReadToEnd();
                }
            }
        }
    }
}