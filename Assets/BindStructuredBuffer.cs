using UnityEngine;

public class BindStructuredBuffer : MonoBehaviour
{
    public GameObject fromQuad;

    private GraphicsBuffer buffer;

    void Start()
    {
        buffer = new GraphicsBuffer (GraphicsBuffer.Target.Structured  | GraphicsBuffer.Target.CopyDestination, GraphicsBuffer.UsageFlags.None, 16*16, 24*4);
        GetComponent<Renderer>().material.SetBuffer("_MyStructuredBuffer", buffer);
    }

    void Update()
    {
        GraphicsBuffer bufferFromOtherQuad = fromQuad.GetComponent<BindRWStructuredBuffer>().buffer;
        Graphics.CopyBuffer(bufferFromOtherQuad, buffer);
    }
}
