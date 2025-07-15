using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보스 애니메이션에 필요한 스트링값을 해쉬 데이터로 전환하는 클래스
/// </summary>
public class BossAnimationDB
{
    [SerializeField] private string spawnParameter = "Spawn";
    [SerializeField] private string idleParameter = "Idle";
    [SerializeField] private string walkParameter = "Walk";
    [SerializeField] private string deathParameter = "Death";
    [SerializeField] private string phaseShiftParameter = "PhaseShift";
    [SerializeField] private string groggyParameter = "Groggy";



    public int SpawnParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int DeathParameterHash { get; private set; }
    public int PhaseShiftParameterHash { get; private set; }
    public int GroggyParameterHash { get; private set; }


    public void Initailize()
    {
        SpawnParameterHash = Animator.StringToHash(spawnParameter);
        IdleParameterHash = Animator.StringToHash(idleParameter);
        WalkParameterHash = Animator.StringToHash(walkParameter);
        DeathParameterHash = Animator.StringToHash(deathParameter);
        PhaseShiftParameterHash = Animator.StringToHash(phaseShiftParameter);
        GroggyParameterHash = Animator.StringToHash(groggyParameter);
    }
}
