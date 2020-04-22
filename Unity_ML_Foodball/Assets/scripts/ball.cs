using UnityEngine;

public class ball : MonoBehaviour
{
   ///<summary>
   ///足球是否進入球門
   ///</summary>
      public static bool complete;

   ///<summary>
   ///觸發開始事件：碰到勾選 Is Trgger 物件會執行一次
   ///</summary>
   private void OnTriggerEnter(Collider other)
   {
       if (other.name == "球門感應區")
       {
       //進入球門
       complete = true;    
       }
   }

}
