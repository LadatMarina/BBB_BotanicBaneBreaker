using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance;
    private const string DATA_FILE_PATH = "/data.json";

    public bool inventoryUploaded = false;

    public List<ConvertedItem> convertedItemList;
    //public List<string> stringList;
    public List<Item> localItemList;


    Player player;

    [System.Serializable]
    public class ConvertedItem
    {
        //public Recollectable jsonItemSO;
        public string itemSO;
        public int amount;
    }
    public string village;

    [System.Serializable]

    public class SaveObject
    {
        public List<ConvertedItem> saveItemList = new ();
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadInventory();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(GameAssets.Instance.GetRecollectableFromString("paco"));
            
        }
    }

    public List<ConvertedItem> SaveInventory(List<Item> listToSave)
    {
        Debug.Log("SaveInventory / DataManager");
        int i = 0;
        SaveObject saveObejct = new (); //create a new save object to store the inventory 

        foreach (Item itemToSave in listToSave) //per cada item de sa llista, feim un item amb diferent tipus
        {
            ConvertedItem convertedItem = new () { itemSO = itemToSave.itemSO.name, amount = itemToSave.amount };

            saveObejct.saveItemList.Add(convertedItem); //guardam s'item convertit a sa llista dins es save object
            Debug.Log("item" + i + saveObejct.saveItemList[i]);
            i++;
        }

        string savedObjectJson = JsonUtility.ToJson(saveObejct);

        // Ruta completa del archivo, se concatena con la ruta del directorio de datos persistente y el nombre del archivo
        string filePath = Application.persistentDataPath + DATA_FILE_PATH;
       
        // Escribe la cadena JSON en el archivo
        File.WriteAllText(filePath, savedObjectJson);

        Debug.Log("Inventory saved to: " + filePath);

        return saveObejct.saveItemList;
    }

    public List<Item> LoadInventory()
    {
        List<Item> newList = new();

        Debug.Log("LoadInventory() / DataManager");
        //we make a new inventory for create a new list with all loaded elements

        string filePath = Application.persistentDataPath + DATA_FILE_PATH;
        int i = 0;
        if (File.Exists(filePath))
        {
            string savedObjectString = File.ReadAllText(filePath);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);

            if (saveObject.saveItemList != null)
            {
                foreach (ConvertedItem convertedItem in saveObject.saveItemList)
                {
                    Item newItem = new Item
                    {
                        itemSO = GameAssets.Instance.GetRecollectableFromString(convertedItem.itemSO),
                        amount = convertedItem.amount
                    };

                    newList.Add(newItem);
                    Debug.Log(newList[i].itemSO.name);
                    i++;
                }
            }
            else
            {
                Debug.Log("saveItemList == null");
            }
        }
        else
        {
            // Aqu� no tendr�amos que caer nunca
            Debug.LogError("No save file");
        }
        if(newList == null)
        {
            Debug.Log("newList null");
        }
        else
        {
            foreach (Item item in newList) { Debug.Log("item of the newList returned " + item.itemSO.name); }

        }
        return newList;
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
                    list.Remove(itemToRemove); //en teoria �s lo mateix posar aqu� itemToRemove que item tot sol asi que nice
                }
            }
        }
    }

    public void SaveVillage(Village village)
    {
        SaveObject saveObject = new SaveObject { villager = village.name };
        string savedObjectJson = JsonUtility.ToJson(saveObject);

        string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        //Escribe la cadena JSON en el archivo
        File.WriteAllText(filePath, savedObjectJson);

        Debug.Log("the villager saved is " + saveObject.villager);

        Debug.Log("villager saved to: " + filePath);
    }

    public Village LoadVillage()
    {
        string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found at: " + filePath);
            return null;
        }

        string savedObjectString = File.ReadAllText(filePath);

        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);

        Debug.Log(GameAssets.Instance.GetVillageFromString(saveObject.villager));

        return GameAssets.Instance.paco;
    }

}
