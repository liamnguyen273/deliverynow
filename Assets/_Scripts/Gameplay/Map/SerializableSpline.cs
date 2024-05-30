using Newtonsoft.Json;
using Owlet;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace DeliveryNow
{
    public class SerializableSplineData : SerializableTransformData
    {
        public List<SerializableKnotData> knots;
        public SerializableSplineData() 
        { 
            knots = new List<SerializableKnotData>();
        }
    }

    public class SerializableKnotData
    {
        public SerializableVector3 position; 
        public SerializableVector3 rotation; 
        public SerializableVector3 tangentIn; 
        public SerializableVector3 tangentOut; 

        public SerializableKnotData()
        {

        }

        public SerializableKnotData(BezierKnot knot)
        {
            position = new(knot.Position);
            Quaternion quaternionRotation = knot.Rotation;
            rotation = new(quaternionRotation.eulerAngles);
            tangentIn = new(knot.TangentIn);
            tangentOut = new(knot.TangentOut);
        }

    }

    public class SerializableSpline : SerializableTransform
    {
        public override string GetTag()
        {
            return Keys.SerializableObject.Tags.Spline;
        }


        [Button]
        public override void Load(string json)
        {
            SerializableSplineData data = JsonConvert.DeserializeObject<SerializableSplineData>(json);
            SplineContainer splineContainer = GetComponent<SplineContainer>();
            List<BezierKnot> knots = new();

            foreach (SerializableKnotData knotData in data.knots)
            {
                BezierKnot knot = new(knotData.position.UnityVector, knotData.tangentIn.UnityVector, knotData.tangentOut.UnityVector, Quaternion.Euler(knotData.rotation.UnityVector));
                knots.Add(knot);
            }

            splineContainer.Splines[0].Knots = knots;

            base.Load(json);
        }

        [Button]
        public override string Save()
        {
            SerializableSplineData data = new();

            SplineContainer splineContainer = GetComponent<SplineContainer>();
            var splines = splineContainer.Splines;
            var knots = splines[0].Knots.ToList();

            foreach( var knot in knots )
            {
                SerializableKnotData knotData = new SerializableKnotData(knot);
                data.knots.Add(knotData);
            }

            data.position = new(transform.position);
            data.rotation = new(transform.rotation.eulerAngles);
            data.scale = new(transform.localScale);

            string json = JsonConvert.SerializeObject(data);    

            Debug.Log(json);
            return json;
        }
    }
}
