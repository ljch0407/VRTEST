using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WOF_varients : MonoBehaviour
{
    [SerializeField] Transform eyeBone;
    [SerializeField] Transform target;
    [SerializeField] float headMaxTurnAngle;
    [SerializeField] float headTrackingSpeed;
    public SoundManager _soundManager;
    public AudioSource _AudioSource;
    public int _id;
    public ManagerAIScript _Ai;
    public PlayableDirector TimeDirector;
    public GameObject Eyeball;
    public ParticleSystem explode;

    private bool isDead;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        _Ai = GameObject.FindGameObjectWithTag("AiManager").GetComponent<ManagerAIScript>();
        _id = _Ai.monster_id_Update();
        explode.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);        
        isDead = false;
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        _soundManager.Add_Monster_audio(_AudioSource, _id);
        StartCoroutine(IDLESoundPlay());
    }
    
    void Update()
    {
        HeadTrackingUpdate();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosive" && !isDead)
        {
            TimeDirector.Play();
            
            StartCoroutine(DeadSoundPlay());
            StartCoroutine(DeadMotionPlay());
            
        }
    }
    void HeadTrackingUpdate()
    {
        //Head to Target
        Quaternion currentLocalRotation = eyeBone.localRotation;
        eyeBone.localRotation = Quaternion.identity;
        
        Vector3 targetWorldLookDir = target.position - eyeBone.position;
        Vector3 targetLocalLookDir = eyeBone.parent.InverseTransformDirection(targetWorldLookDir);
        
        
        targetLocalLookDir = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir,
            Mathf.Deg2Rad * headMaxTurnAngle,
            0);
        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, -Vector3.up);
        
        eyeBone.localRotation = Quaternion.Slerp(currentLocalRotation,
            targetLocalRotation,
            1-Mathf.Exp(-headTrackingSpeed * Time.deltaTime)
        );        
    }
    IEnumerator DeadSoundPlay()
    {
        _soundManager.Monster_StopSFX(_id);
        _soundManager.Monster_PlaySFX("SFX_WOF_Howling", _id);
       
        yield return new WaitForSeconds(0.4f);
    }IEnumerator DeadMotionPlay()
    {
        explode.Play();
        Eyeball.SetActive(false);
        yield return new WaitForSeconds(1f);
        explode.Stop();
        isDead = true;
    }
    IEnumerator IDLESoundPlay()
    {
        _soundManager.Monster_PlaySFX("SFX_WOF_IDLE", _id);
        yield return new WaitForSeconds(0.4f);
    }
}
