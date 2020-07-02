using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class robot : Agent
{
    [Header("速度"), Range(1, 50)]
    public float speed = 10;

    ///<summary>
    ///機器人剛體
    ///</sumary>
     private Rigidbody rigrobot;
     
     ///<sumary>
     ///足球剛體
     ///</sumary>
     private Rigidbody rigball;

     private void Start()
     {
         rigrobot = GetComponent<Rigidbody>();
         rigball = GameObject.Find("足球").GetComponent<Rigidbody>();
     }

/// <summary>
///事件開始時：重新設定機器人與足球的位置
/// </summary>
     public override void OnEpisodeBegin()
     {
    //重設剛體加速度與角度加速度
       rigrobot.velocity = Vector3.zero;
       rigrobot.angularVelocity = Vector3.zero;
       rigball.velocity = Vector3.zero;
       rigball.angularVelocity = Vector3.zero;

    //機器人隨機移動範圍
       Vector3 posrobot = new Vector3(Random.Range(-2f,2f), 0.1f, Random.Range(-2f,0f));
       transform.position = posrobot;

    //球滾動隨機座標
       Vector3 posball = new Vector3(Random.Range(-2f,2f), 0.1f, Random.Range(1f,2f));
       rigball.position = posball;

    //足球未進入球門
       ball.complete = false;   
     }


///<summary>
///收集觀測資料
///</summary>
     public override void CollectObservations(VectorSensor sensors)
     {
    //加入觀測資料：機器人與足球的座標，機器人，機器人加速度x,z
       sensors.AddObservation(transform.position);
       sensors.AddObservation(rigball.position);
       sensors.AddObservation(rigrobot.velocity.x);
       sensors.AddObservation(rigrobot.velocity.z);
     }

///<summary>
///
///</summary>
     public override void OnActionReceived(float[] vectorAction)
     {
         //使用參數使用參數控制機器人
         Vector3 control = Vector3.zero;
         control.x = vectorAction[0];
         control.z = vectorAction[1];
         rigrobot.AddForce(control * speed);
        
        //成功加一分並結束（球進入球門
         if (ball.complete)
         {
             SetReward(1);
             EndEpisode();
         }
        //失敗扣一分並結束（機器人或是掉至地板下
         if (transform.position.y < 0 || rigball.position.y < 0)
         {
             SetReward(-1);
             EndEpisode();
         }
     }

///<summary>
///讓開發者測試環境
///</summary>

   public override void Heuristic(float[] actionsOut)
{
    actionsOut[0] = Input.GetAxis("Horizontal");
    actionsOut[1] = Input.GetAxis("Vertical");
}
   
   //public override float[] Heuristic()
    //{
      // var action = new float[2];
       //action[0] = Input.GetAxis("Horizontal");
       //action[1] = Input.GetAxis("Vertical");
       //return action;
     //}
}
