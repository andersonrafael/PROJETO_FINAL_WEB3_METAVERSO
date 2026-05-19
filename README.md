# 🏛️ MuseuVirtualMeta — Museu Cultural Interativo no Metaverso

> **Web 3.0 | Residência em TIC 29 — Unidade 1, Capítulo 3**  
> Atividade Avaliativa — Fundamentos do Metaverso: Criando sua Primeira Experiência VR Interativa  
> Prof.: Ana Beatriz

---

## 👤 FRANCISCO ANDERSON RAFAEL DA SILVA

**FRANCISCO ANDERSON RAFAEL DA SILVA**  

---

## 🎯 Apresentando o Projeto

O **MuseuVirtualMeta** é um museu cultural interativo desenvolvido em Unity com suporte ao **Meta XR SDK**, projetado para funcionar no **Meta Quest 2/3**. O ambiente recria uma galeria de arte e história onde o visitante pode:

- **Explorar** salas temáticas com obras e artefatos históricos
- **Interagir** com quadros e esculturas para ouvir narrações e ver informações
- **Abrir portas** entre salas por interação física
- **Visualizar painéis** informativos ao se aproximar de cada objeto

A experiência é totalmente navegável no **Unity Editor (PC)** via XR Device Simulator, sem necessidade do headset para testes.

---

## 🌐 Contexto e Objetivos no Metaverso

### Problema / Oportunidade
O acesso a museus e patrimônio cultural ainda é restrito por barreiras geográficas e financeiras. Ao mesmo tempo, a experiência em museus físicos carece de interatividade e personalização.

### Objetivo no Metaverso
O **MuseuVirtualMeta** resolve isso oferecendo:

| Dimensão | Solução |
|---|---|
| **Educação** | Narrações em áudio contextualizam cada obra |
| **Acessibilidade** | Qualquer pessoa com um headset Quest pode visitar |
| **Entretenimento** | Interações gamificadas tornam a visita dinâmica |
| **Preservação Cultural** | Obras e artefatos digitalizados ficam imortalizados |

O contexto se enquadra no pilar de **Metaverso Cultural/Educacional**, alinhado com iniciativas como o Google Arts & Culture e museus virtuais da UNESCO.

---

## 🏗️ Processo de Criação

### Etapa 1 — Planejamento do Tema
Escolhi o tema de **Museu Virtual** pela riqueza de interações possíveis e pelo impacto social claro: democratizar o acesso à cultura.

### Etapa 2 — Configuração do Ambiente Unity
- Instalado o **Unity 6000.0.28f1 LTS** (compatível com Meta XR SDK v74)
- Configurado o **Universal Render Pipeline (URP)** para melhor performance no Quest
- Adicionado o **Meta XR All-in-One SDK** via Package Manager (scoped registry)

### Etapa 3 — Montagem do Ambiente
A cena foi construída com primitivos Unity e organizados em grupos lógicos:

```
Scene: MuseuVirtualScene
├── [--- MANAGEMENT ---]
│   ├── GameManager (MuseuVirtualManager.cs)
│   ├── EventSystem
│   └── XR Interaction Manager
│
├── [--- PLAYER ---]
│   └── XR Origin (Camera Offset)
│       ├── Main Camera
│       ├── LeftHand_Controller
│       └── RightHand_Controller
│
├── [--- ENVIRONMENT ---]
│   ├── Plane_Chao (material mármore)
│   ├── Directional Light (simulação luz natural)
│   ├── Skybox (céu claro/interno com HDRI)
│   ├── Parede_Norte (Cube escalado)
│   ├── Parede_Sul
│   ├── Parede_Leste
│   ├── Parede_Oeste
│   └── Teto
│
├── [--- OBJETOS_MUSEO ---]
│   ├── Quadro_MonaLisa (Plane + textura)
│   │   ├── PainelInfo_MonaLisa (Canvas WorldSpace)
│   │   └── InteracaoObjeto.cs
│   ├── Escultura_Classica (Capsule)
│   │   └── InteracaoObjeto.cs
│   ├── Pedestal_1 (Cylinder)
│   ├── Pedestal_2 (Cylinder)
│   └── Vitrine_Artefato (Cube transparente)
│
├── [--- INTERATIVOS ---]
│   ├── Porta_SalaA (PortaInterativa.cs)
│   ├── Porta_SalaB (PortaInterativa.cs)
│   └── PainelBemVindo (PainelProximidade.cs)
│
└── [--- ILUMINACAO ---]
    ├── PointLight_Sala1
    ├── PointLight_Sala2
    └── SpotLight_Quadro (ilumina obras)
```

### Etapa 4 — Scripts de Interação
Foram criados **3 scripts C# originais e comentados**:

| Script | Função |
|---|---|
| `InteracaoObjeto.cs` | Muda cor + toca som ao interagir com quadros |
| `PainelProximidade.cs` | Exibe painel de info ao se aproximar |
| `PortaInterativa.cs` | Abre/fecha porta com animação suave |
| `MuseuVirtualManager.cs` | Gerencia e valida a cena |

### Etapa 5 — Configuração XR e Build

**XR Plugin Management (Android):**
- OpenXR habilitado
- Meta XR Feature Group ativado

**Build Settings:**
- Platform: Android
- Texture Compression: ASTC
- Minimum API Level: 29 (Android 10 — Quest 2/3)
- Scripting Backend: IL2CPP

**Movimentação no PC (Editor):**
- Utilizado o **XR Device Simulator** (XR Interaction Toolkit)
- Mouse simula visão do headset; WASD para locomoção

---

## 💡 Dificuldades e Como Foram Resolvidas

| Dificuldade | Solução Adotada |
|---|---|
| Configurar o scoped registry do Meta XR SDK | Adicionado manualmente ao `manifest.json` antes de abrir o projeto |
| Raio do controlador não detectava objetos | Adicionado `XR Simple Interactable` + `Collider` em todos os objetos interativos |
| Painel Canvas em WorldSpace ficava muito grande | Ajustado o `Canvas Scaler` e `Rect Transform` para escala 0.01 |
| Performance no Quest (muitos Draw Calls) | Habilitado GPU Instancing nos materiais e reduzido polígonos |
| Porta animando de forma incorreta | Refatorado usando `Quaternion.RotateTowards` em Corrotina |

---

## 📁 Estrutura do Repositório

```
MuseuVirtualMeta/
├── Assets/
│   ├── Scripts/
│   │   ├── InteracaoObjeto.cs
│   │   ├── PainelProximidade.cs
│   │   ├── PortaInterativa.cs
│   │   └── MuseuVirtualManager.cs
│   ├── Materials/
│   │   ├── Mat_Marmore.mat
│   │   ├── Mat_Parede_Museu.mat
│   │   └── Mat_Quadro_Destaque.mat
│   ├── Prefabs/
│   │   ├── Quadro_Interativo.prefab
│   │   └── Painel_Info.prefab
│   ├── Audio/
│   │   └── (clips de narração)
│   └── Scenes/
│       └── MuseuVirtualScene.unity
├── ProjectSettings/
│   └── (configurações Unity/XR)
├── Packages/
│   └── manifest.json
├── .gitignore
└── README.md
```

---

## ✅ Checklist dos Requisitos

- [x] Projeto Unity com versão compatível com Meta XR SDK
- [x] Meta XR SDK instalado e configurado (manifest.json)
- [x] Build Settings para Android (Meta Quest)
- [x] XR Plugin Management com OpenXR
- [x] Movimentação funcional no PC (XR Device Simulator)
- [x] Mínimo 5 objetos 3D na cena (quadros, esculturas, pedestais, paredes, portas)
- [x] Plano de chão navegável
- [x] Skybox configurado
- [x] Ambiente temático coerente (Museu Cultural)
- [x] Interação funcional em C# (cor + som + painel de info)
- [x] Hierarquia organizada com grupos lógicos
- [x] Nomenclatura clara e consistente
- [x] Scripts C# comentados
- [x] Repositório público no GitHub
- [x] .gitignore para Unity
- [x] README completo

---

## 🔮 Melhorias Futuras

- Implementar **Multiplayer** com Photon PUN2 para visitas em grupo
- Adicionar **Text-to-Speech** para narrações dinâmicas geradas por IA
- Criar **sistema de pontuação** gamificado para visitantes
- Integrar com **NFTs** para obras digitais únicas (contexto Web 3.0)
- Suporte a **hand tracking** nativo do Quest (sem controles físicos)

---

*Projeto desenvolvido como atividade avaliativa do curso Web 3.0 — Residência em TIC 29.*
TODOS OS DIREITOS RESERVADOS A FRANCISCO ANDERSON RAFAEL DA SILVA
