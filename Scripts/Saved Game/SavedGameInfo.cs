using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;

/**Stores the information required for saved states
 * Contains seperate progress information for singleplayer
 * and multiplayer, for each level stores a current
 * best score that can be continued from
 * 
 * Stewart Collins - Last edit 18/09/16
 */

[Serializable] public class SavedGameInfo {
    //The amout of levels to store saved games for
    public static int AMOUNTOFLEVELS = 9;

    //The scores for each level in singleplayer
    public int[] singlePlayerScores;

    //The scores for each level in multiplayer
    public int[] multiPlayerScores;

    

}
