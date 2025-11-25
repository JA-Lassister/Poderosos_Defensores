using UnityEngine;

public class ParedesInvisiveis
{
    public static float MaximoX = 60f;
    public static float MinimoX = -60f;
    public static float MaximoZ = 55f;
    public static float MinimoZ = -15f;

    public static Vector3 PosicaoAjustada(float x, float y, float z) =>
        new Vector3(Mathf.Clamp(x, MinimoX, MaximoX), y, Mathf.Clamp(z, MinimoZ, MaximoZ));

    public static Vector3 PosicaoAjustada(Vector3 v) =>
        new Vector3(Mathf.Clamp(v.x, MinimoX, MaximoX), v.y, Mathf.Clamp(v.z, MinimoZ, MaximoZ));
}
