using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";

    private bool encryptData = false;
    private string codeword = "sacabambaspis"; 
    public FileDataHandler(string _dataDirPath, string _dataFileName, bool _encryptData)
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
        this.encryptData = _encryptData;
    }
    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try{
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(_data, true);

            if(encryptData){
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using(FileStream fs = new FileStream(fullPath, FileMode.Create)){
                using(StreamWriter writer = new StreamWriter(fs)){
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e){
            Debug.LogError("Error saving file: " + e.Message);
        }
    }
    public GameData Load(){
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;
        if(File.Exists(fullPath)){
            try{
                string dataToLoad = "";
                using(FileStream fs = new FileStream(fullPath, FileMode.Open)){
                    using(StreamReader reader = new StreamReader(fs)){
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                if(encryptData){
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e){
                Debug.LogError("Error loading file: " + e.Message);
            }
        }
        return loadData;
    }
    public void Delete(){
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if(File.Exists(fullPath)){
            File.Delete(fullPath);
        }
    }
    private string EncryptDecrypt(string _data){
        string modifiedData = "";

        for(int i = 0; i< _data.Length; i++){
            modifiedData += (char)(_data[i] ^ codeword[i % codeword.Length]);
        }
        return modifiedData;
    }
}
