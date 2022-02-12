using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DoorNeed { No, Item, Cant };
public enum DoorType {A, B, C}; // 이름 미정이라서 이렇게 해둠.
public class Door : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField]
    private bool isRotatingDoor = true;
    [SerializeField]
    private float Speed = 1f;
    [Header("Rotation Configs")]
    [SerializeField]
    private float RotationAmount = 90f;
    [SerializeField]
    private float ForwardDirection = 0;

    private Vector3 StartRotation;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;

    [Tooltip("문의 종류")]
    public DoorType DoorType;
    [Tooltip("문의 종류가 B일때만 사용")]
    public GameObject ToWhere;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        //Since "Forward" actually is pointing into the door frame, choose a direction to think about as "forward"
        Forward = transform.right;
    }

    public void Open(Vector3 UserPosition)
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }
        if (isRotatingDoor)
        {
            float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
            Debug.Log($"Dot :  {dot.ToString("N3")})");
            AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRoation;

        if (ForwardAmount >= ForwardDirection)
        {
            endRoation = Quaternion.Euler(new Vector3(0, startRotation.y - RotationAmount, 0));

        }
        else
        {
            endRoation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
        }

        isOpen = true;
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRoation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
    public void Close()
    {
        if (isOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);

            }

            if (isRotatingDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRoation = transform.rotation;
        Quaternion endRoation = Quaternion.Euler(StartRotation);
        isOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRoation, endRoation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }




    [Tooltip("문의 조건")]
    public DoorNeed DoorNeed;

    [Tooltip("DoorNeed이 Item일경우에만 사용함")]
    public string NeedItem;
    Vector3 rayL;
    Vector3 Point0;
    void Start()
    {
        if (DoorType == DoorType.A)
        {
            Point0.x = transform.localRotation.x;
            Point0.y = transform.localRotation.y;
            Point0.z = transform.localRotation.z;
        }else if (DoorType == DoorType.B)
        {
            Point0.x = transform.localPosition.x;
            Point0.y = transform.localPosition.y;
            Point0.z = transform.localPosition.z;
        }
        else
        {

        }
    }
    public void ChangeDoorState(Vector3 rayR)
    {
        rayL = rayR;
      //  open = !open;
    }

    // Update is called once per frame
    //void Update()
   // {
    //    if (DoorType == DoorType.A)
     //   {
        //    if (open)
        //    {

//             Quaternion targetRotation = Quaternion.Euler(0, Point0.z + (rayL.z / Mathf.Abs(rayL.z)) * -90f, 0);
//    
 //               transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, 2 * Time.deltaTime);
//

       //     }
       //     else
       //     {
  //              Quaternion targetRotation2= Quaternion.Euler(0,0,0);
    //            targetRotation2.x = Point0.x;
      //          targetRotation2.y = Point0.y;
        //        targetRotation2.z = Point0.z;

          //      transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, 2 * Time.deltaTime);
       //     }
      //  }
  //      else if (DoorType == DoorType.B)
    //    {
        //    if (open)
        //    {

      ///          Vector3 targetP = ToWhere.gameObject.transform.localPosition;
      //
        //        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetP, 2 * Time.deltaTime);
        //
        //
       //     }
       //     else
        //    {
          ////      Vector3 targetP2 = Point0;
              ////  transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetP2, 2 * Time.deltaTime);
        //    }
     ///   }
     ///   else if(DoorType == DoorType.C)
       /// {
     //
       /// }
   // }
}
