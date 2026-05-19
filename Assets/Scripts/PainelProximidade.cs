using UnityEngine;

/// <summary>
/// Exibe um painel de informações quando o jogador se aproxima do objeto.
/// Ideal para placas de museu que descrevem obras ou artefatos.
/// </summary>
public class PainelProximidade : MonoBehaviour
{
    [Header("Configuração de Proximidade")]
    [Tooltip("Distância em metros para ativar o painel")]
    public float raioDeteccao = 2.5f;

    [Tooltip("Tag do objeto do jogador (XR Origin)")]
    public string tagJogador = "Player";

    [Header("UI")]
    [Tooltip("Painel Canvas que será exibido ao se aproximar")]
    public GameObject painelCanvas;

    private Transform _jogador;
    private bool _painelVisivel = false;

    void Start()
    {
        // Localiza o jogador pela tag
        GameObject jogadorObj = GameObject.FindGameObjectWithTag(tagJogador);
        if (jogadorObj != null)
            _jogador = jogadorObj.transform;
        else
            Debug.LogWarning("[PainelProximidade] Jogador com tag '" + tagJogador + "' não encontrado.");

        // Garante que o painel começa oculto
        if (painelCanvas != null)
            painelCanvas.SetActive(false);
    }

    void Update()
    {
        if (_jogador == null || painelCanvas == null) return;

        // Calcula distância entre o jogador e este objeto
        float distancia = Vector3.Distance(transform.position, _jogador.position);

        if (distancia <= raioDeteccao && !_painelVisivel)
        {
            // Entra na zona: exibe o painel
            painelCanvas.SetActive(true);
            _painelVisivel = true;
            Debug.Log($"[PainelProximidade] Painel '{painelCanvas.name}' ativado por proximidade.");
        }
        else if (distancia > raioDeteccao && _painelVisivel)
        {
            // Sai da zona: oculta o painel
            painelCanvas.SetActive(false);
            _painelVisivel = false;
        }
    }

    // Exibe o raio de detecção no Editor para facilitar o design
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
        Gizmos.DrawSphere(transform.position, raioDeteccao);
    }
}
