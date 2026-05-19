using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Script de interação principal do Museu Virtual.
/// Quando o usuário aponta o raio do controlador e pressiona Trigger,
/// o objeto (quadro/artefato) muda de cor e emite um som de narração.
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(XRSimpleInteractable))]
public class InteracaoObjeto : MonoBehaviour
{
    [Header("Configurações Visuais")]
    [Tooltip("Cor padrão do objeto antes da interação")]
    public Color corPadrao = Color.white;

    [Tooltip("Cor de destaque ao ser ativado")]
    public Color corDestaque = new Color(0.2f, 0.8f, 1f); // Azul ciano

    [Header("Configurações de Áudio")]
    [Tooltip("Clip de áudio que será tocado ao interagir (ex: narração do quadro)")]
    public AudioClip somInteracao;

    [Header("Painel de Informação")]
    [Tooltip("GameObject do painel/canvas de informação que aparece ao interagir")]
    public GameObject painelInfo;

    [Tooltip("Tempo em segundos que o painel fica visível")]
    public float tempoPainel = 5f;

    // Referências internas
    private Renderer _renderer;
    private AudioSource _audioSource;
    private XRSimpleInteractable _interactable;
    private bool _ativado = false;
    private float _timerPainel = 0f;

    // ---------------------------------------------------------------
    // Inicialização
    // ---------------------------------------------------------------
    void Awake()
    {
        // Captura os componentes necessários
        _renderer    = GetComponent<Renderer>();
        _audioSource = GetComponent<AudioSource>();
        _interactable = GetComponent<XRSimpleInteractable>();

        // Cor padrão ao iniciar
        AplicarCor(corPadrao);

        // Painel começa invisível
        if (painelInfo != null)
            painelInfo.SetActive(false);
    }

    void OnEnable()
    {
        // Registra o evento de seleção (Trigger pressionado)
        _interactable.selectEntered.AddListener(AoSerSelecionado);
    }

    void OnDisable()
    {
        // Remove o listener ao desativar (boas práticas de memória)
        _interactable.selectEntered.RemoveListener(AoSerSelecionado);
    }

    // ---------------------------------------------------------------
    // Update: controla o timer do painel de informação
    // ---------------------------------------------------------------
    void Update()
    {
        if (_ativado && painelInfo != null && painelInfo.activeSelf)
        {
            _timerPainel -= Time.deltaTime;
            if (_timerPainel <= 0f)
            {
                // Oculta o painel após o tempo definido
                painelInfo.SetActive(false);
                _ativado = false;
                AplicarCor(corPadrao); // Retorna à cor original
            }
        }
    }

    // ---------------------------------------------------------------
    // Callback chamado quando o XR Interaction Toolkit detecta seleção
    // ---------------------------------------------------------------
    private void AoSerSelecionado(SelectEnterEventArgs args)
    {
        Ativar();
    }

    // ---------------------------------------------------------------
    // Lógica central de ativação
    // ---------------------------------------------------------------
    public void Ativar()
    {
        // Muda a cor para destaque
        AplicarCor(corDestaque);

        // Toca o som de narração (se configurado)
        if (somInteracao != null)
        {
            _audioSource.clip = somInteracao;
            _audioSource.Play();
        }

        // Exibe painel de informação
        if (painelInfo != null)
        {
            painelInfo.SetActive(true);
            _timerPainel = tempoPainel;
        }

        _ativado = true;

        Debug.Log($"[InteracaoObjeto] Objeto '{gameObject.name}' foi ativado.");
    }

    // ---------------------------------------------------------------
    // Aplica uma cor ao material do objeto
    // ---------------------------------------------------------------
    private void AplicarCor(Color cor)
    {
        if (_renderer != null)
            _renderer.material.color = cor;
    }
}
