using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowInformation : MonoBehaviour {
	
//	public class Tuple<T, U> : System.IEquatable<Tuple<T,U>>
//	{
//		readonly T first;
//		readonly U second;
//
//		public Tuple(T first, U second) {
//			this.first = first;
//			this.second = second;
//		}
//
//		public T First { get { return first; } }
//		public U Second { get { return second; } }
//
//		public override int GetHashCode() {
//			return first.GetHashCode() ^ second.GetHashCode();
//		}
//
//		public override bool Equals(object obj) {
//			if (obj == null || GetType() != obj.GetType()) {
//				return false;
//			}
//			return Equals((Tuple<T, U>)obj);
//		}
//
//		public bool Equals(Tuple<T, U> other) {
//			return other.first.Equals(first) && other.second.Equals(second);
//		}
//	}

	public class MovementInfo {
		public bool movingRight;
		public bool movingLeft;
		public bool jump;
		public bool crouching;
		public MovementInfo(bool mR, bool mL, bool j, bool c) {
			this.movingRight = mR;
			this.movingLeft = mL;
			this.jump = j;
			this.crouching = c;
		}
	}


//	public Queue infoQueue;



//	public Dictionary<Tuple<int,int>, MovementInfo> infoMap;

	// Use this for initialization
	void Start () {
//		infoMap = new Dictionary<Tuple<int, int>, MovementInfo> ();
//		infoQueue = new Queue();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
