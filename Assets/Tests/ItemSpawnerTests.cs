using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ItemSpawnerTests
{
    [UnityTest]
    public IEnumerator ItemSpawnerSpawnsItemTest()
    {
        yield return SceneManager.LoadSceneAsync("TestScene0");
        yield return new WaitForEndOfFrame();

        ItemSpawner spawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        Assert.AreEqual(spawner.currentItems.Count, 0);
        yield return new WaitForSecondsRealtime(GameConfig.c_ItemSpawnTime + 1.0f);

        Assert.Greater(spawner.currentItems.Count, 0);
    }
}
