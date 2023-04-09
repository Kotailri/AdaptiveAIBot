using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HealthTests
{
    [UnityTest]
    public IEnumerator HealthUpdateTest()
    {
        yield return SceneManager.LoadSceneAsync("TestScene0");
        yield return new WaitForEndOfFrame();
        
        Health playerHealth = GameObject.Find("Player").GetComponent<Health>();

        playerHealth.UpdateHealth(-10);
        Assert.AreEqual(90, playerHealth.health);
        
        playerHealth.UpdateHealth(110);
        Assert.AreEqual(100, playerHealth.health);

        Assert.IsFalse(playerHealth.CheckDead());
        playerHealth.UpdateHealth(-1000);
        Assert.IsTrue(playerHealth.CheckDead());

        playerHealth.UpdateHealth(100);
    }

    public IEnumerator ScoreUpdateOnDeath()
    {
        yield return SceneManager.LoadSceneAsync("GameScene");
        yield return new WaitForEndOfFrame();

        Health playerHealth = GameObject.Find("Player").GetComponent<Health>();
        
        int currentScore = Global.gamemanager.BotScore;
        playerHealth.UpdateHealth(-100);

        yield return new WaitForSecondsRealtime(0.5f);
        Assert.Greater(Global.gamemanager.BotScore, currentScore);
    }

    public IEnumerator TakingDamageTest()
    {
        yield return SceneManager.LoadSceneAsync("GameScene");
        yield return new WaitForEndOfFrame();

        float prevTimeScale = Time.timeScale;
        Time.timeScale = 1;

        GameObject player = GameObject.Find("Player");
        Assert.IsNotNull(player);

        GameObject bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/Player/Bullet_bad.prefab");
        GameObject bulletInstance = PrefabUtility.InstantiatePrefab(bulletPrefab) as GameObject;
        Assert.IsNotNull(bulletInstance);

        bulletInstance.transform.position = new Vector3(player.transform.position.x, 
            player.transform.position.y + 3.0f, player.transform.position.z);
        bulletInstance.GetComponent<BulletCollision>().playerType = PlayerType.Bot;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);

        yield return new WaitForSeconds(1.5f);

        Health playerHealth = GameObject.Find("Player").GetComponent<Health>();
        Assert.Less(playerHealth.health, 100);

        playerHealth.UpdateHealth(100);

        GameObject poisonPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Level/Prefabs/Poison_Bad.prefab");
        GameObject poisonInstance = PrefabUtility.InstantiatePrefab(poisonPrefab) as GameObject;
        Assert.IsNotNull(bulletInstance);

        poisonInstance.transform.position = player.transform.position;
        yield return new WaitForSeconds(1.5f);
        Assert.Less(playerHealth.health, 100);

        Time.timeScale = prevTimeScale;
    }
}
