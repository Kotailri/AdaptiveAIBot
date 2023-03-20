using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerStatTests
{
    [UnityTest]
    public IEnumerator PlayerCollectsStatItemTest()
    {
        yield return SceneManager.LoadSceneAsync("GameScene");
        yield return new WaitForEndOfFrame();
        float prevTimeScale = Time.timeScale;
        Time.timeScale = 1;

        GameObject player = GameObject.Find("Player");
        Assert.IsNotNull(player);

        GameObject speed_stat_item = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/Items/Speed_Stat_Item.prefab");
        GameObject instance = PrefabUtility.InstantiatePrefab(speed_stat_item) as GameObject;
        Assert.IsNotNull(instance);

        float currentSpeedBoost = Global.playerSpeedBoost;
        instance.transform.position = player.transform.position;

        yield return new WaitForSecondsRealtime(1.0f);
        Assert.Greater(Global.playerSpeedBoost, currentSpeedBoost);

        Assert.IsTrue(player.GetComponent<Inventory>().HasItem(ItemName.SpeedStat));
        Assert.Greater(player.GetComponent<Inventory>().GetItemCount(), 0);

        Global.playerSpeedBoost = currentSpeedBoost;
        Time.timeScale = prevTimeScale;
    }
}
