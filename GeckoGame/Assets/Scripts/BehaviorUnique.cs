using System.Collections.Generic;
using UnityEngine;
using BTAI;

public class BehaviorUnique : MonoBehaviour
{
    private Animator m_Animator;
    private Root m_btRoot = BT.Root();
    // Distance at which the minion will start following player
    public float range = 0.6f;
    public GameObject target;
    private float lastOutOfRange;

    private PlayerMotionController _script;


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        _script = target.GetComponent<PlayerMotionController>();
        if (_script == null)
        {
            Debug.Log("target script is null!");
        }

        Selector rootSelector = BT.Selector();
        m_btRoot.OpenBranch(rootSelector);

        Sequence helpPlayer = BT.Sequence();
        rootSelector.OpenBranch(helpPlayer);

        BTNode checkInRange = BT.RunCoroutine(CheckInRange);
        BTNode flapWing = BT.RunCoroutine(FlapWing);
        BTNode checkScore = BT.RunCoroutine(CheckScore);
        BTNode giveGift = BT.RunCoroutine(GiveGift);
        BTNode idle = BT.RunCoroutine(Idle);

        helpPlayer.OpenBranch(checkInRange);
        helpPlayer.OpenBranch(flapWing);
        helpPlayer.OpenBranch(checkScore);
        helpPlayer.OpenBranch(giveGift);

        rootSelector.OpenBranch(idle);
    }

    // Update is called once per frame
    void Update()
    {
        m_btRoot.Tick();
    }

    IEnumerator<BTState> CheckInRange()
    {
        if ((transform.position - target.transform.position).magnitude > range)
        {
            lastOutOfRange = Time.realtimeSinceStartup;
            yield return BTState.Failure;
        }
        yield return BTState.Success;
    }

    IEnumerator<BTState> FlapWing()
    {
        m_Animator.SetFloat("movementSpeed", 1.0f);
        yield return BTState.Success;
    }

    IEnumerator<BTState> CheckScore()
    {
        if (_script._score < 5) {
            yield return BTState.Success;
        }
        yield return BTState.Failure;
    }

    IEnumerator<BTState> GiveGift()
    {
        if (Time.realtimeSinceStartup - lastOutOfRange > 0.5f)
        {
            lastOutOfRange = Time.realtimeSinceStartup;
            _script.addPoint(1);
            yield return BTState.Success;
        }
        yield return BTState.Continue;
    }
    IEnumerator<BTState> Idle()
    {
        m_Animator.SetFloat("movementSpeed", -1.0f);
        yield return BTState.Success;
    }
}
