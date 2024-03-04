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
    public string village;


    [System.Serializable]
    public class ConvertedItem
    {
        //public Recollectable jsonItemSO;
        public string itemSO;
        public int amount;
    }

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
            AddOneItem(new Item
            {
                amount = 1,
                itemSO =
                GameAssets.Instance.apple
            });
        }
    }

    public List<ConvertedItem> SaveInventory(List<Item> listToSave)
    {
        //Debug.Log("SaveInventory / DataManager");
        int i = 0;
        SaveObject saveObejct = new (); //create a new save object to store the inventory 
        if(saveObejct != null)
        {
            foreach (Item itemToSave in listToSave) //per cada item de sa llista, feim un item amb diferent tipus
            {
                ConvertedItem convertedItem = new() { itemSO = itemToSave.itemSO.name, amount = itemToSave.amount };
                if (convertedItem != null)
                {
                    saveObejct.saveItemList.Add(convertedItem); //guardam s'item convertit a sa llista dins es save object
                }
                i++;
            }
            string savedObjectJson = JsonUtility.ToJson(saveObejct, true);

            // Ruta completa del archivo, se concatena con la ruta del directorio de datos persistente y el nombre del archivo
            string filePath = Application.persistentDataPath + DATA_FILE_PATH;

            // Escribe la cadena JSON en el archivo
            File.WriteAllText(filePath, savedObjectJson);

            Debug.Log("Inventory saved to: " + filePath);
            return saveObejct.saveItemList;

        }
        return null;
    }

    public List<Item> LoadInventory()
    {
        List<Item> newList = new();

        //Debug.Log("LoadInventory() / DataManager");
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
                    if(convertedItem != null)
                    {
                        //Debug.Log("item to convert :" + convertedItem.itemSO);
                        Item newItem = new Item
                        {
                            itemSO = GameAssets.Instance.GetRecollectableFromString(convertedItem.itemSO),
                            amount = convertedItem.amount
                        };

                        newList.Add(newItem);
                        i++;
                    }
                }
            }
            else
            {
                //Debug.Log("saveItemList == null");
            }
        }
        else
        {
            // Aquí no tendríamos que caer nunca
            //Debug.LogError("No save file");
        }
        if(newList == null)
        {
            //Debug.Log("newList null");
        }
        else
        {
            foreach (Item item in newList) { //Debug.Log("item of the newList returned " + item.itemSO.name);
                                             }

        }
        return newList;
    }
    private List<ConvertedItem> LoadSavedItemList()
    {
        string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        if (File.Exists(filePath))
        {
            string savedObjectString = File.ReadAllText(filePath);
            SaveObject saveObject = new SaveObject();
            saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);
            if(saveObject != null)
            {
                return saveObject.saveItemList;
            }
            return null;
        }
        else
        {
            return null;
        }
    }

    //removes an item from the saved list
    public void RemoveOneItem(Item itemToRemove)
    {
        localItemList = LoadInventory();
        Debug.Log("removeOneItem(item) / dataManager");
        Debug.Log("localItemList count" + localItemList.Count);

        foreach (Item item in localItemList)
        {
            if(item.itemSO == itemToRemove.itemSO)
            {
                if(item.amount != 1)
                {
                    item.amount--;
                    break; 
                }
                else
                {
                    localItemList.Remove(item); //en teoria és lo mateix posar aquí itemToRemove que item tot sol asi que nice
                    break;  
                }
            }
        }
        SaveInventory(localItemList);
    }

    public void AddOneItem(Item item) //mirar si s'inventory se destrueix després, perquè jo només el vull emplear com una eina per reutilitzar funcions
    {
        localItemList = LoadInventory();

        if (localItemList != null)
        {
            bool itemInInventory = false;
            Debug.Log("addOneItem / inventory");
            foreach (Item inventoryItem in localItemList)
            {
                if (item.itemSO == inventoryItem.itemSO)
                {
                    Debug.Log("The item was already in the list; its amount is increased");
                    inventoryItem.amount += item.amount;
                    itemInInventory = true;
                    break;
                }
            }

            if (!itemInInventory)
            {
                Debug.Log("The item was not in the list; item added");
                localItemList.Add(item);
            }
        }
        else
        {
            Debug.Log("itemList NULL /inventory class");
        }

        SaveInventory(localItemList);
    }

    public void SaveVillage(Village village)
    {
        SaveObject saveObject = new SaveObject {
            saveItemList = LoadSavedItemList(),
            villager = village.name };
        if(saveObject != null)
        {
            string savedObjectJson = JsonUtility.ToJson(saveObject,true);

            string filePath = Application.persistentDataPath + DATA_FILE_PATH;

            //Escribe la cadena JSON en el archivo
            File.WriteAllText(filePath, savedObjectJson);

            //Debug.Log("the villager saved is " + saveObject.villager);

            //Debug.Log("villager saved to: " + filePath);
        }
    }

    public Village LoadVillage()
    {
        string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        if (!File.Exists(filePath))
        {
            //Debug.LogWarning("Save file not found at: " + filePath);
            return null;
        }

        string savedObjectString = File.ReadAllText(filePath);

        SaveObject saveObject = new SaveObject();
        saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);

        return GameAssets.Instance.GetVillageFromString(saveObject.villager);
    }

}
