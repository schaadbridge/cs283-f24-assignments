using System.Collections.Generic;
using BTAI;
using UnityEngine;
using UnityEngine.AI;

public class BehaviorMinion : MonoBehaviour
{
    private Root m_btRoot = BT.Root();
    // Distance at which the minion will start following player
    public float range = 0.3f;
    public GameObject target;
    public Vector3 minionHome;
    private PlayerMotionController _script;
    private float lastOutOfRange = 0;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (!target.TryGetComponent<PlayerMotionController>(out _script))
        {
            Debug.Log("target script is null!");
        }
        _audioSource = GetComponent<AudioSource>();

        // minion will return to its starting point when retreating -- MUST START OUTSIDE TARGETHOME
        minionHome = transform.position;

        Selector rootSelector = BT.Selector();
        m_btRoot.OpenBranch(rootSelector);

        Sequence checkToAttack = BT.Sequence();
        Sequence checkToFollow = BT.Sequence();
        BTNode orRetreat = BT.RunCoroutine(Retreat);

        rootSelector.OpenBranch(checkToAttack);
        rootSelector.OpenBranch(checkToFollow);
        rootSelector.OpenBranch(orRetreat);

        BTNode inRange = BT.RunCoroutine(CheckInRange);
        BTNode attack = BT.RunCoroutine(Attack);

        checkToAttack.OpenBranch(inRange);
        checkToAttack.OpenBranch(attack);

        BTNode notAtHome = BT.RunCoroutine(CheckNotAtHome);
        BTNode follow = BT.RunCoroutine(Follow);

        checkToFollow.OpenBranch(notAtHome);
        checkToFollow.OpenBranch(follow);
    }

    // Update is called once per frame
    void Update()
    {
        m_btRoot.Tick();   
    }

    IEnumerator<BTState> CheckInRange()
    {
        if ((transform.position - target.transform.position).sqrMagnitude > range * range)
        {
            lastOutOfRange += Time.deltaTime;
            yield return BTState.Failure;
        }
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.ResetPath();
        yield return BTState.Success;
    }

    IEnumerator<BTState> Attack()
    {
        // take 1 mushroom each second
        if (lastOutOfRange > 0.5f)
        {
            lastOutOfRange = 0;
            _audioSource.Play();
            _script.OnCollisionEnter();
            yield return BTState.Continue;
        }
        lastOutOfRange += Time.deltaTime;
        yield return BTState.Success;
    }

    IEnumerator<BTState> CheckNotAtHome()
    {
        // left side of home area
        float leftHome = -26.9f;
        float rightHome = -21.13f;
        float backHome = -65.46f;
        float frontHome = -59.66f;
        
        if (target.transform.position.z > leftHome && target.transform.position.z < rightHome
            && target.transform.position.x > backHome && target.transform.position.x < frontHome)
        {
            yield return BTState.Failure;
        }
        yield return BTState.Success;
    }
    IEnumerator<BTState> Follow()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(target.transform.position);
        agent.SetDestination(target.transform.position);
        yield return BTState.Success;
    }

    IEnumerator<BTState> Retreat()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(minionHome);
        yield return BTState.Success;
    }
}
