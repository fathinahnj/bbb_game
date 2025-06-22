using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static bool isGameOver = false;

  public static void GameOver()
  {
    isGameOver = true;
    Debug.Log("GAME OVER: Istana hancur!");
    // Di sini kamu bisa munculkan UI Game Over juga
  }
}
