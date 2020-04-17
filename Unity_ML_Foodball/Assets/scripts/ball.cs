using UnityEngine;
using MLAgents;
using MLAgents,Sensors;

public class robot : Agent
{
[Header("速度"), Range(1, 50)]
public float speed = 10;
///<summary>
///機器人剛體
///</summary>
    private Rigidbody rigrobot;
	///<summary>
///足球剛體
///</summary>
	private Rigidbody rigball;
	
	private void Start()
	{
	rigrobot = Getcomponent<Rigidbody>();
	rigball = GameObject.Find("足球").Getcomponent<Rigidbody>();
	}
	
	///<summary>
	///事件開始時：重新設定機器人與足球的位置
	///</summary>
public override void OnEpisodeBegin()
{
//重設缸體加速度與角度加速度
rigrobot.velocity = Vector3.zero;
rigrobot.angularVelocity = Vector3.zero;
rigball.velocity = Vector3.zero;
rigball.angularVelocity = Vector3.zero;

//隨機機器人位置
Vector3 posrobot = new Vector(Random.Range(-2f,2f), 0.1f ,Random.Range(-2f,0f));
transform.postion = posrobot;

//隨機足球位置
Vector3 posball = new Vector(Random.Range(-2f,2f), 0.1f ,Random.Range(1f,2f));
rigball.postion = posball;

//足球尚未進入球門
ball.complete = false;

}

///<summary>
///收集觀測資料
///</summary>
public overrde void CollectObservations(VectorSensor sensor)
{
//加入觀測資料：機器人與足球的座標、機器人加速度x、z
sensor.AddObservation(transform.position);
sensor.AddObservation(rigball.position);
sensor.AddObservation(rigrobot.velocity.x);
sensor.AddObservation(rigrobot.velocity.z);
}
///<summary>
///動作：控制機器人與回饋
///</summary>
public overrde void OnActonReceived(float[] vectorAction)
{
//使用參數控制機器人
Vector3 control = Vector3.zero;
control.x = vectorAction[0];
control.z = vectorAction[1];
rigrobot.AddForce(control * speed);

//成功加一分並結束
if (ball.complete)
{
SetReward(1);
EndEpisode();
}

//失敗扣一分並結束
if (transform.postion.y < 0 || rigball.position.y < 0)
{
SetReward(-1);
EndEpisode();
}
}
}