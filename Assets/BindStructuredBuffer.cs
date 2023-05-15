using UnityEngine;

public class BindStructuredBuffer : MonoBehaviour
{
    public GameObject fromQuad;

#if UNITY_EDITOR
    private GraphicsBuffer bufferCopy;
#endif

    void Start()
    {
    #if UNITY_EDITOR
        bufferCopy = new GraphicsBuffer (GraphicsBuffer.Target.Structured  | GraphicsBuffer.Target.CopyDestination, GraphicsBuffer.UsageFlags.None, 16*16, 24*4);
        GetComponent<Renderer>().material.SetBuffer("_MyStructuredBuffer", bufferCopy);
    #else
        GraphicsBuffer bufferFromOtherQuad = fromQuad.GetComponent<BindRWStructuredBuffer>().buffer;
        GetComponent<Renderer>().material.SetBuffer("_MyStructuredBuffer", bufferFromOtherQuad);
    #endif
    }

#if UNITY_EDITOR
    void Update()
    {
        GraphicsBuffer bufferFromOtherQuad = fromQuad.GetComponent<BindRWStructuredBuffer>().buffer;
        Graphics.CopyBuffer(bufferFromOtherQuad, bufferCopy);
    }
#endif
}
