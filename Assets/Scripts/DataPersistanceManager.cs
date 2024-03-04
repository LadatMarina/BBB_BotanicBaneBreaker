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
    public List<ConvertedItem> unlockedPotionsList;

    public List<Item> localItemList;
    public string village;

    private string filePath;

    [System.Serializable]
    public class ConvertedItem
    {
        public string itemSO;
        public int amount;
    }

    [System.Serializable]
    public class SaveObject
    {
        public List<ConvertedItem> saveItemList = new List<ConvertedItem>();
        public string villager;
        public bool isFirstGame;
        public List<ConvertedItem> unlockedPotionsList = new List<ConvertedItem>();
    }

    private void Awake()
    {
        filePath = Application.persistentDataPath + DATA_FILE_PATH; 

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

        if (!File.Exists(filePath))
        {
            CreateFirstJsonFile();
        }

    }
    public List<ConvertedItem> SaveInventory(List<Item> listToSave)
    {
        int i = 0;
        SaveObject saveObejct = new(); //create a new save object to store the inventory 
        if (saveObejct != null)
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
            // write data to JSON
            File.WriteAllText(filePath, savedObjectJson);

            return saveObejct.saveItemList;
        }
        return null;
    }

    public List<Item> LoadInventory()
    {
        List<Item> newList = new();

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
                        Item newItem = new Item
                        {
                            itemSO = GameAssets.Instance.GetRecollectableFromString(convertedItem.itemSO),
                            amount = convertedItem.amount
                        };
                        newList.Add(newItem);
                    }
                }
            }
            else
            {
                Debug.LogWarning("SaveObject of the JSON file does not exist. Cannot load inventory.");
            }
        }
        else
        {
            Debug.LogWarning("JSON file doesn't exist. Cannot load inventory.");
        }
        if (newList == null)
        {
            //Debug.Log("newList null");
        }
        return newList;
    }
    private List<ConvertedItem> LoadSavedItemList()
    {
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
        if(localItemList != null)
        {
            foreach (Item item in localItemList)
            {
                if (item.itemSO == itemToRemove.itemSO)
                {
                    if (item.amount != 1)
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
        }
        SaveInventory(localItemList);
    }

    public void AddOneItem(Item item) //mirar si s'inventory se destrueix després, perquè jo només el vull emplear com una eina per reutilitzar funcions
    {
        localItemList = LoadInventory();

        if (localItemList != null)
        {
            bool itemInInventory = false;
            foreach (Item inventoryItem in localItemList)
            {
                if (item.itemSO == inventoryItem.itemSO)
                {
                    inventoryItem.amount += item.amount;
                    itemInInventory = true;
                    break;
                }
            }
            if (!itemInInventory)
            {
                localItemList.Add(item);
            }
        }
        else
        {
            Debug.LogWarning("ItemList null. Cannot add an item.");
        }

        SaveInventory(localItemList);
    }

    public void SaveVillage(Village village)
    {
        SaveObject saveObject = new SaveObject {
            saveItemList = LoadSavedItemList(),
            villager = village.name,
            isFirstGame = LoadIsFirstGame()
        };
        if(saveObject != null)
        {
            string savedObjectJson = JsonUtility.ToJson(saveObject,true);

            //write values in the JSON file 
            File.WriteAllText(filePath, savedObjectJson);
        }
    }

    public Village LoadVillage()
    {
        if (File.Exists(filePath))
        {
            string savedObjectString = File.ReadAllText(filePath);
            SaveObject saveObject = new SaveObject();
            saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);
            if (saveObject != null)
            {
                return GameAssets.Instance.GetVillageFromString(saveObject.villager);
            }
            else
            {
                Debug.LogWarning("The saveObject is null. Couldn't load the village");
                return null;
            }
        }
        Debug.LogWarning("JSON file does not exists. Could not load the village");
        return null;
    }

    public void SaveGame(bool b, List<Item> unlockedPotions)
    {
        //initialze the object we store
        SaveObject saveObject = new SaveObject()
        {
            saveItemList = LoadSavedItemList(),
            villager = "", //not necessary save the village between the games
            isFirstGame = b,
            unlockedPotionsList = new List<ConvertedItem>()
        };

        //meanwhile there's unlocked potions to save, make the conversion
        if(unlockedPotions.Count != 0)
        {
            foreach (Item attackPotion in unlockedPotions) //per cada item de sa llista, feim un item amb diferent tipus
            {
                ConvertedItem convertedItem = new() { itemSO = attackPotion.itemSO.name, amount = 1 };
                if (convertedItem != null)
                {
                    saveObject.unlockedPotionsList.Add(convertedItem);
                }
            };
        }
        
        //save all in the JSON file
        if (saveObject != null)
        {
            string savedObjectJson = JsonUtility.ToJson(saveObject, true);

            //write the JSON file
            File.WriteAllText(filePath, savedObjectJson);
        }
    }
    public bool LoadIsFirstGame()
    {
        if(File.Exists(filePath))
        {
            string savedObjectString = File.ReadAllText(filePath);
            SaveObject saveObject = new SaveObject();
            saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);
            if(saveObject != null)
            {
                return saveObject.isFirstGame;
            }
            else
            {
                Debug.LogWarning("The saveObject is null. Couldn't load the bool isFirstGame");
                return false;
            }
        }
        
        Debug.LogError("the file JSON that stores 'isFirstGame' doesn't exists");
        return false;
        
    }

    public List<Item> LoadUnlockedList()
    {
        List<Item> newList = new();

        if (File.Exists(filePath))
        {
            string savedObjectString = File.ReadAllText(filePath);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);

            if (saveObject.saveItemList != null)
            {
                foreach (ConvertedItem convertedItem in saveObject.unlockedPotionsList)
                {
                    if (convertedItem != null)
                    {
                        Item newItem = new Item
                        {
                            itemSO = PotionManager.Instance.GetPotionFromString(convertedItem.itemSO),
                            amount = 1
                        };
                        newList.Add(newItem);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("the file JSON that stores 'isFirstGame' doesn't exists");
            return null;
        }
        return newList;
    }
    
    private void CreateFirstJsonFile()
    {
        //initialze the object we store
        SaveObject saveObject = new SaveObject()
        {
            saveItemList = new List<ConvertedItem>(),
            villager = "",
            isFirstGame = true,
            unlockedPotionsList = new List<ConvertedItem>()
        };

        //save all in the JSON file
        if (saveObject != null)
        {
            string savedObjectJson = JsonUtility.ToJson(saveObject, true);

            //write the JSON file
            File.WriteAllText(filePath, savedObjectJson);
        }
    }

}
