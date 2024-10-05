using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;
    //[SerializeField] private float minX = -1, maxX = 1;  // Vị trí tối thiểu
    //[SerializeField] private float initialY = 1;  // Vị trí tối đa
    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        this.FXOnDead();
        this.DropOnDead();
        this.entity.gameObject.SetActive(false);
    }

    public virtual void DropOnDead()
    {
        ItemDropSpawner.Instance.Drop(this.stateData.dropList, this.entity.aliveGo.transform.position, this.entity.aliveGo.transform.rotation                                  );
    }
    public virtual void FXOnDead()
    {
        AudioManager.Instance.PlayAudio(AudioManager.Instance.dead);
        GameObject.Instantiate(stateData.deathBloodParticle, this.entity.aliveGo.transform.position, this.stateData.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(stateData.deathChunkParticle, this.entity.aliveGo.transform.position, this.stateData.deathChunkParticle.transform.rotation);
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(new Vector2(minX, initialY), new Vector2(maxX, initialY)); // Vẽ một đường ngang tại Y
    //    Gizmos.DrawWireSphere(new Vector2((minX + maxX) / 2, initialY), 0.5f); // Điểm spawn
    //}
}
