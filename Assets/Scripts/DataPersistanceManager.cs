using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance;
    private const string DATA_FILE_PATH = "/data.json";

    public bool inventoryUploaded = false;

    public List<JsonItem> jsonItemList;
    //public List<string> stringList;
    public List<Item> localItemList;

    public SaveObject saveObject;

    public class JsonItem
    {
        //public Recollectable jsonItemSO;
        public string itemSO;
        public int amount;
    }
    public string village;

    public class SaveObject
    {
        public List<JsonItem> saveItemList;
        //public List<string> stringList;
        public string villager;
    }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        jsonItemList = new List<JsonItem>();
        localItemList = new List<Item>();

        saveObject = new SaveObject { saveItemList = new List<JsonItem>() };


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (JsonItem jsonItem in saveObject.saveItemList)
            {
                Debug.Log("object of the saveItemList " + "Item: " + jsonItem.itemSO + ", Amount: " + jsonItem.amount);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (JsonItem jsonItem in saveObject.saveItemList)
            {
                Debug.Log("object of the saveItemList " + "Item: " + jsonItem.itemSO + ", Amount: " + jsonItem.amount);
            }
        }
    }

    public void SaveInventory(List<Item> listToSave)
    {
        Debug.Log("SaveInventory / DataManager");


        //jsonItemList.Clear(); //reset the list before saving
        //stringList.Clear(); //reset the list before saving
        int i = 0;

        foreach (Item item in listToSave)
        {
            JsonItem jsonItem = new JsonItem { itemSO = item.itemSO.name, amount = item.amount };

            saveObject.saveItemList.Add(jsonItem);
            //Debug.Log(jsonItemList[i].itemSO);
            i++;
        }


        //foreach (Item item in listToSave)
        //{
            
        //    //stringList.Add(item.itemSO.name);
        //    //Debug.Log(jsonItemList[i].itemSO);
        //    //i++;
        //}



        //SaveObject saveObject = new SaveObject { stringList = stringList };
        //foreach (JsonItem jsonItem in saveObject.saveItemList)
        //{
        //    Debug.Log("object of the saveObject.saveItemList " + "Item: " + jsonItem.itemSO + ", Amount: " + jsonItem.amount);
        //}

        string savedObjectJson = JsonUtility.ToJson(saveObject);

        // Ruta completa del archivo, se concatena con la ruta del directorio de datos persistente y el nombre del archivo
        string filePath = Application.persistentDataPath + DATA_FILE_PATH;
        //File.WriteAllText(Application.dataPath + DATA_FILE_PATH, savedObjectJson);

        // Escribe la cadena JSON en el archivo
        File.WriteAllText(filePath, savedObjectJson);

        Debug.Log("Inventory saved to: " + filePath);
        //Debug.Log(savedObjectJson);
    }

    public List<Item> LoadInventory()
    {
        Debug.Log("LoadInventory() / DataManager");
        //we make a new inventory for create a new list with all loaded elements
        Inventory loadedInventory = new Inventory();

        string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        if (File.Exists(filePath))
        {
            string savedObjectString = File.ReadAllText(filePath);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);
            //stringList.Clear(); //delete all the components in the list

            //if (saveObject.stringList != null)
            //{
            //    foreach (string stringItem in saveObject.stringList)
            //    {
            //        Item newItem = new Item
            //        {
            //            itemSO = GameAssets.Instance.GetRecollectableFromString(stringItem),
            //            amount = 1
            //        };

            //        localItemList.Add(newItem); //re feim sa llista a partir de es items que aconsegueix fent sa conversió de string a recollectables
            //    }
            //}
            //else
            //{
            //    Debug.Log("save object. saveItemList == null");
            //}

            if (saveObject.saveItemList != null)
            {
                foreach (JsonItem jsonItem in saveObject.saveItemList)
                {
                    Item newItem = new Item
                    {
                        itemSO = GameAssets.Instance.GetRecollectableFromString(jsonItem.itemSO),
                        amount = jsonItem.amount
                    };

                    loadedInventory.GetItemList().Add(newItem);
                }
            }
            else
            {
                Debug.Log("save object. saveItemList == null");
            }
        }
        else
        {
            // Aquí no tendríamos que caer nunca
            Debug.LogError("No save file");
        }

        //foreach(Item item in loadedInventory.itemList)
        //{
        //    Debug.Log(item.itemSO.name + " " + item.amount);
        //}
        //inventoryUploaded = true; // this bool is for when the inventory is saved to the json file, per quant jo hagui d'accedir des de s'UI invntory, perquè així sàpiga si puc fer load, si nohi ha
        
        return localItemList;

    }

    public void RemoveOneItem(Item itemToRemove)
    {
        List<Item> list = LoadInventory();
        foreach(Item item in list)
        {
            if(item == itemToRemove)
            {
                if(item.amount != 1)
                {
                    item.amount--;
                }
                else
                {
                    list.Remove(itemToRemove); //en teoria és lo mateix posar aquí itemToRemove que item tot sol asi que nice
                }
            }
        }
    }

    public void SaveVillage(Village village)
    {
        //SaveObject saveObject = new SaveObject { villager = village.name };
        //string savedObjectJson = JsonUtility.ToJson(saveObject);

        //string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        // Escribe la cadena JSON en el archivo
        //File.WriteAllText(filePath, savedObjectJson);

        //Debug.Log("the villager saved is " + saveObject.villager);

        //Debug.Log("villager saved to: " + filePath);
    }

    public Village LoadVillage()
    {
        //string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        //if (!File.Exists(filePath))
        //{
        //    Debug.LogWarning("Save file not found at: " + filePath);
        //    return null;
        //}

        //string savedObjectString = File.ReadAllText(filePath);

        //SaveObject saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);

        //Debug.Log(GameAssets.Instance.GetVillageFromString(saveObject.villager));

        //return GameAssets.Instance.GetVillageFromString(saveObject.villager);
        return null;
    }

}
