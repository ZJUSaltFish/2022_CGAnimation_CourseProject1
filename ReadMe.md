# 计算机动画大作业 - ReadMe

可执行程序：解压BoidGame.zip，运行BoidProject.exe。

Unity 2021.3.11f1c2或更高

URP管线。

# Boid算法实现

对象层：Boid，Flock。控制flock总体运动，boid的运动自己决定。

Boid的行为按优先级排布：碰撞避免（避障>避让同伴）> 速度匹配 > 合群 > 前往目的地。

**Boid Prefab：鸟的实例**

- Mesh - 模型
- animator - 驱动动画
- Script Parameters
  - position 
  - velocity 
  - a-accumulator - 加速度累加器
  - acceleration - 最终加速度
  - neighbor list - 所有可感知的鸟
  - neighbor center - 可感知的群体中心 
  - neighbor velocity - 可感知的群体平均速度
- Script Functions
  - UpdateMovement - 每帧计算运动，由Controller通知何时进行（计算完位置关系表后）
  - ObstacleAvoiding - 紧急避障 - 无障碍/需要躲避。紧急避障时禁用三个原则函数（恐慌鸟群各求生路）。
  - CollisionCheck - 检测碰撞倾向 - 无需躲避/规避/碰撞（杀死两个鸟）。躲避时直接采取最大加速度。（加速度渐变是一个预测的过程，交给速度匹配）
  - VelocityMatch - 检测速度匹配程度 - 在容差内/需要匹配。匹配时偏移越大提供的加速度越大（提供渐变的加速度）。
  - FlockCentering - 检测是否合群 - 可接受/需要合群。匹配时距离越远提供的加速度越大。
  - ApplyMovement - 应用加速度，然后应用速度

**Flock Controller Prefab：群体控制器**（鸟群并没有集群意识，所以flock不能对boid做任何直接控制）

- Script Functions:
  - Spawn - 开局生成给定数量的鸟
  - ReTarget - 设定新的目的地
  - ResetParameters - 重设某个预设参数

**Marker Prefab：群体标记**，用于显示群体的位置和速度（方便观察）

**UI Canvas：用户界面**，用于调节各种参数



**ScriptObject：共享的参数**

*预设而可手动动态修改的参数*

- depth of view - 鸟能看多远
- field of view - 鸟的视角范围
- max velocity - 最大速度
- max physical acceleration - 最大实际加速度
- max mental acceleration - 最大预期加速度（用来模拟鸟群努力加速但力竭不能达到的加速度）
- min seperating distance - 最小的不触发分离行为的距离——一旦需要分离则以最大加速度远离（和避障一样）。
- max velocity tolerance - 最大的可接受的与群体速度的差值
- max a velo diff - 产生最大加速度的临界速度差——低于它则加速度线性变化
- max allowed offset - 最大的可接受的和同伴的间距
- max acceleration distance - 产生最大加速度的临界间距——低于它则加速度线性变化
- command dict - 一个字典，记录所有可能的指令以及对应的权重

*开局生成的静态参数*

- boid number - 鸟的数量
- boid index - 鸟的名单

*动态变化的参数*

- destination - 所有鸟要前往的目的地（会动态修改）

  

