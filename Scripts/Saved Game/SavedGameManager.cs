using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/**This class manages the saved states of the game
 * It loads the saved game information from the specified path
 * for the user based on their userID, if no save exists
 * then it creates a new save with scores set to zero
 */
public class SavedGameManager : MonoBehaviour {

    //The instance of the singleton class
    private static SavedGameManager instance;

    //The saved game information
    private SavedGameInfo info;
    //TODO, get user ID from login information
    private string userID = "userID";

    //Returns the instance of the singleton class
    public static SavedGameManager getSavedGameManager()
    {
        if (instance == null)
        {
            instance = new SavedGameManager();
        }
        return instance;
    }

    //Saves the info object to the application persistent data path, so will automatically
    //chose the correct path based on build settings
    public void save()
    {
        //Serialises the file
        BinaryFormatter bf = new BinaryFormatter();
        //Accesses the file
        FileStream file = File.Open(Application.persistentDataPath + "/" + userID + "Info.dat", FileMode.Create);

        //Serialise
        bf.Serialize(file, getInfo());
        //Close the file
        file.Close();
    }

    //Returns saved game info
    //Initialises it to default values if no info found
    //Should only be called after trying to load saved game
    public SavedGameInfo getInfo()
    {
        //If no info has been loaded
        if (info == null)
        {
            //If saved file exists then load save file
            if(File.Exists(Application.persistentDataPath + "/" + userID + "Info.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + userID + "Info.dat",FileMode.Open);
                info = (SavedGameInfo)bf.Deserialize(file);
                file.Close();
            }//Otherwise create new save file with empty save points
            else
            {
                info = new SavedGameInfo();
                info.singlePlayerScores = new int[SavedGameInfo.AMOUNTOFLEVELS];
                info.multiPlayerScores = new int[SavedGameInfo.AMOUNTOFLEVELS];
                for (int i = 0; i < SavedGameInfo.AMOUNTOFLEVELS; i++)
                {
                    info.singlePlayerScores[i] = 0;
                    info.multiPlayerScores[i] = 0;
                }this.save();
            }
        }
        return info;
    }

    //Updates the saved game information
    public void updateInfo(SavedGameInfo newInfo)
    {
        info = newInfo;
    }
}
