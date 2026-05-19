using UnityEngine;

/// <summary>
/// Script de configuração inicial da cena do Museu Virtual.
/// Centraliza parâmetros globais e realiza validação no Editor.
/// NÃO precisa estar em nenhum GameObject específico — attach em um
/// empty GameObject chamado "GameManager".
/// </summary>
public class MuseuVirtualManager : MonoBehaviour
{
    [Header("Informações do Projeto")]
    [Tooltip("Nome do ambiente VR")]
    public string nomeAmbiente = "MuseuVirtualMeta - Metaverso Cultural";

    [Tooltip("Versão do projeto")]
    public string versao = "1.0.0";

    [Header("Configurações de Teleporte")]
    [Tooltip("Pontos de teleporte disponíveis na cena")]
    public Transform[] pontosTeleporte;

    [Header("Objetos Interativos")]
    [Tooltip("Lista de todos os objetos interativos cadastrados")]
    public InteracaoObjeto[] objetosInterativos;

    [Header("Debug")]
    public bool modoDebug = true;

    // ---------------------------------------------------------------
    void Start()
    {
        if (modoDebug)
        {
            Debug.Log($"[MuseuVirtualManager] Iniciando: {nomeAmbiente} v{versao}");
            Debug.Log($"[MuseuVirtualManager] Objetos interativos: {objetosInterativos?.Length ?? 0}");
            Debug.Log($"[MuseuVirtualManager] Pontos de teleporte: {pontosTeleporte?.Length ?? 0}");
        }

        ValidarCena();
    }

    // ---------------------------------------------------------------
    /// <summary>Valida dependências críticas da cena</summary>
    private void ValidarCena()
    {
        // Verifica se o XR Origin está presente
        if (GameObject.FindObjectOfType<Unity.XR.CoreUtils.XROrigin>() == null)
            Debug.LogError("[MuseuVirtualManager] XR Origin não encontrado! Configure o XR Origin na cena.");

        // Verifica iluminação direcional
        if (FindObjectOfType<Light>() == null)
            Debug.LogWarning("[MuseuVirtualManager] Nenhuma luz encontrada na cena.");

        if (modoDebug)
            Debug.Log("[MuseuVirtualManager] Validação concluída.");
    }

    // ---------------------------------------------------------------
    /// <summary>
    /// Exibe no Editor os pontos de teleporte como esferas verdes
    /// </summary>
    void OnDrawGizmos()
    {
        if (pontosTeleporte == null) return;
        Gizmos.color = Color.green;
        foreach (Transform ponto in pontosTeleporte)
        {
            if (ponto != null)
                Gizmos.DrawSphere(ponto.position, 0.3f);
        }
    }
}
