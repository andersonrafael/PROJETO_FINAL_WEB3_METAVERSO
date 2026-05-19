using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Abre e fecha uma porta por interação XR (Trigger) ou por proximidade.
/// Ideal para portas internas do museu virtual.
/// </summary>
public class PortaInterativa : MonoBehaviour
{
    [Header("Configurações da Animação")]
    [Tooltip("Ângulo de abertura da porta em graus (ex: 90 = abre para a direita)")]
    public float anguloAbertura = 90f;

    [Tooltip("Velocidade de abertura/fechamento em graus por segundo")]
    public float velocidadeRotacao = 80f;

    [Header("Feedback Sonoro")]
    [Tooltip("Som de abertura da porta")]
    public AudioClip somAbrir;

    [Tooltip("Som de fechamento da porta")]
    public AudioClip somFechar;

    // Estado interno
    private bool _estaAberta = false;
    private bool _animando = false;
    private Quaternion _rotacaoFechada;
    private Quaternion _rotacaoAberta;
    private AudioSource _audioSource;

    void Awake()
    {
        // Salva rotação inicial (porta fechada)
        _rotacaoFechada = transform.localRotation;

        // Calcula rotação da porta aberta (em torno do eixo Y local)
        _rotacaoAberta = Quaternion.Euler(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y + anguloAbertura,
            transform.localEulerAngles.z
        );

        // Obtém ou adiciona AudioSource
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();

        // Tenta registrar interação XR
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
            interactable.selectEntered.AddListener(AoInteragir);
    }

    // ---------------------------------------------------------------
    // Pode ser chamado por botão UI ou por XR Interaction
    // ---------------------------------------------------------------
    public void AoInteragir(SelectEnterEventArgs args) => AlternarPorta();
    public void AlternarPorta()
    {
        if (_animando) return; // Impede cliques múltiplos durante animação
        StartCoroutine(AnimarPorta());
    }

    // ---------------------------------------------------------------
    // Corrotina que anima suavemente a porta
    // ---------------------------------------------------------------
    private IEnumerator AnimarPorta()
    {
        _animando = true;

        // Define destino
        Quaternion destino = _estaAberta ? _rotacaoFechada : _rotacaoAberta;

        // Toca som correspondente
        AudioClip som = _estaAberta ? somFechar : somAbrir;
        if (som != null) _audioSource.PlayOneShot(som);

        // Rotaciona suavemente
        while (Quaternion.Angle(transform.localRotation, destino) > 0.5f)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                destino,
                velocidadeRotacao * Time.deltaTime
            );
            yield return null;
        }

        // Garante posição final exata
        transform.localRotation = destino;

        // Inverte estado
        _estaAberta = !_estaAberta;
        _animando = false;

        Debug.Log($"[PortaInterativa] Porta '{gameObject.name}' {(_estaAberta ? "aberta" : "fechada")}.");
    }
}
