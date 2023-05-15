using UnityEngine;

public class BindRWStructuredBuffer : MonoBehaviour
{
    public GraphicsBuffer buffer;

    void Awake()
    {
        buffer = new GraphicsBuffer (GraphicsBuffer.Target.Structured | GraphicsBuffer.Target.CopySource , GraphicsBuffer.UsageFlags.None, 16*16, 24*4);
#if UNITY_EDITOR
        Graphics.SetRandomWriteTarget(1, buffer, true);
#endif
        GetComponent<Renderer>().material.SetBuffer("_MyRWStructuredBuffer", buffer);
    }

#if false // debug output
    struct MyBufferData
    {
        public Vector3 originalColor;
        public Vector3 modifiedColor;
    }
    void OnRenderObject()
    {
        if(buffer!=null)
        {
            float[] myBufferData = new float[16*16*24];
            buffer.GetData(myBufferData);
            Debug.Log("myBufferData[0~5]:"
                + myBufferData[0] + " " + myBufferData[1] + " "  + myBufferData[2] + " "  
                + myBufferData[3] + " " + myBufferData[4] + " "  + myBufferData[5] + " " );
        }
    }
#endif
}
