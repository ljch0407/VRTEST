using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

public class WallOfFlesh : MonoBehaviour
{

    [SerializeField] Transform eyeBone;
    [SerializeField] Transform eyeBone2;
    [SerializeField] Transform target;
    [SerializeField] float headMaxTurnAngle;
    [SerializeField] float headTrackingSpeed;
    
    public GameObject MiniMonsterPrefab;
    public Transform[] SpawnPoint;
    public Transform SpawnTransform;
    public PlayableDirector TimeDirector;
    private SoundManager _soundManager;
    public AudioSource _AudioSource;
    public int _id;
    public ManagerAIScript _Ai;

    public float MobSpawnTime;
    public float MobSpawnDistance;
    
    private float monsterCounter;
    private void Start()
    {
        _Ai = GameObject.FindGameObjectWithTag("AiManager").GetComponent<ManagerAIScript>();
        _id = _Ai.monster_id_Update();
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        _soundManager.Add_Monster_audio(_AudioSource, _id);
        StartCoroutine(IDLESoundPlay());
        monsterCounter = 50;
        StartCoroutine(SpawnMonster());

        if (MobSpawnDistance == 0.0f)
        {
            MobSpawnDistance = 10.0f;
        }

        if (MobSpawnTime == 0.0f)
        {
            MobSpawnTime = 2.0f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        HeadTrackingUpdate();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosive")
        {
            TimeDirector.Play();
            StartCoroutine(DeadSoundPlay());
        }
    }

    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(MobSpawnTime);

        if (Vector3.Distance(SpawnTransform.position, target.position) < MobSpawnDistance)
        {
            monsterCounter--;

            if (monsterCounter > 0)
            {
                Instantiate(MiniMonsterPrefab, SpawnPoint[0].position, SpawnPoint[0].rotation);
            }
        }
        _soundManager.Monster_PlaySFX("SFX_WOF_Spawnning", _id);
        yield return new WaitForSeconds(1f);
        _soundManager.Monster_StopSFX(_id);
        _soundManager.Monster_PlaySFX("SFX_WOF_IDLE", _id);

        StartCoroutine(SpawnMonster());
    }
    
    
    void HeadTrackingUpdate()
    {
        //Head to Target
        Quaternion currentLocalRotation = eyeBone.localRotation;
        Quaternion currentLocalRotation2 = eyeBone.localRotation;
        eyeBone.localRotation = Quaternion.identity;
        
        Vector3 targetWorldLookDir = target.position - eyeBone.position;
        Vector3 targetLocalLookDir = eyeBone.parent.InverseTransformDirection(targetWorldLookDir);
        
        Vector3 targetWorldLookDir2 = target.position - eyeBone2.position;
        Vector3 targetLocalLookDir2 = eyeBone2.parent.InverseTransformDirection(targetWorldLookDir2);
        
        targetLocalLookDir = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir,
            Mathf.Deg2Rad * headMaxTurnAngle,
            0);
        targetLocalLookDir2 = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir2,
            Mathf.Deg2Rad * headMaxTurnAngle,
            0);
        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, -Vector3.up);
        Quaternion targetLocalRotation2 = Quaternion.LookRotation(targetLocalLookDir2, -Vector3.up);
        
        eyeBone.localRotation = Quaternion.Slerp(currentLocalRotation,
            targetLocalRotation,
            1-Mathf.Exp(-headTrackingSpeed * Time.deltaTime)
        );        
        eyeBone2.localRotation = Quaternion.Slerp(currentLocalRotation2,
            targetLocalRotation2,
            1-Mathf.Exp(-headTrackingSpeed * Time.deltaTime)
        );
    }
    
    IEnumerator DeadSoundPlay()
    {
        _soundManager.Monster_PlaySFX("SFX_WOF_Howling", _id);
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
    IEnumerator IDLESoundPlay()
    {
        _soundManager.Monster_PlaySFX("SFX_WOF_IDLE", _id);
        yield return new WaitForSeconds(10f);
    }
}
