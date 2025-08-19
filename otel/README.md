# 📡 OpenTelemetry - Observabilidade Moderna para Sistemas Distribuídos

Este projeto utiliza **OpenTelemetry (OTel)** para coletar, processar e exportar dados de telemetria — como *logs*, *métricas* e *traces* — com o objetivo de melhorar a observabilidade de aplicações distribuídas.

## 🚀 O que é o OpenTelemetry?

OpenTelemetry é uma **framework de código aberto** mantida pela [Cloud Native Computing Foundation (CNCF)](https://opentelemetry.io/), que padroniza a coleta de dados de desempenho e comportamento de sistemas modernos. Ele surgiu da fusão dos projetos OpenTracing e OpenCensus.

## 🔍 Por que usar?

- **Observabilidade completa**: Entenda o que está acontecendo em tempo real com sua aplicação.
- **Independência de backend**: Envie os dados para Prometheus, Jaeger, Elastic, Grafana, entre outros.
- **Instrumentação fácil**: Suporte para várias linguagens (Java, Python, Go, .NET, etc.).
- **Padrão da indústria**: Cada vez mais adotado por empresas que operam em ambientes nativos da nuvem.

## 📊 Tipos de dados coletados

| Tipo       | Descrição                                                                 |
|------------|---------------------------------------------------------------------------|
| **Logs**   | Registros de eventos que ocorreram em pontos específicos do sistema.      |
| **Métricas** | Valores numéricos medidos ao longo do tempo (ex: uso de CPU, taxa de erro). |
| **Traces** | Caminho de uma requisição por vários serviços, útil para identificar gargalos. |

## 🛠️ Como funciona

1. **Instrumentação**: O código da aplicação é instrumentado com bibliotecas do OpenTelemetry.
2. **Coleta**: Os dados são coletados automaticamente ou manualmente.
3. **Processamento**: Os dados podem ser enriquecidos, filtrados ou agregados.
4. **Exportação**: Os dados são enviados para um backend de observabilidade.

## 📦 Exemplos de backends compatíveis

- Jaeger
- Prometheus
- Zipkin
- Elastic Stack
- Grafana Tempo
- AppSignal

## 📚 Recursos adicionais

- [Documentação oficial](https://opentelemetry.io/docs/)
- [Guia de instrumentação por linguagem](https://opentelemetry.io/docs/instrumentation/)
- [Repositório GitHub](https://github.com/open-telemetry)

---

> Este projeto é ideal para desenvolvedores, DevOps e engenheiros de confiabilidade que desejam ter visibilidade profunda sobre o comportamento de suas aplicações.


# 🛠️ Observabilidade com OpenTelemetry Collector

Este projeto demonstra como utilizar o **OpenTelemetry Collector** para centralizar, processar e exportar dados de telemetria (traces, métricas e logs) gerados por aplicações instrumentadas com o SDK do OpenTelemetry.

## 📦 Componentes principais

### 1. SDKs de Instrumentação

Bibliotecas específicas por linguagem (Java, Python, Go, etc.) que são integradas ao código da aplicação.

- **Função**: Gerar dados de telemetria diretamente da aplicação.
- **Exemplo**: Uma API em Python instrumentada com o SDK do OpenTelemetry que envia traces para o Collector.

### 2. OpenTelemetry Collector

Processo independente que atua como **agente ou gateway** para receber, transformar e exportar dados de telemetria.

- **Função**: 
  - Receber dados de múltiplas fontes (aplicações, agentes, etc.)
  - Aplicar processamento intermediário (filtros, agregações, enriquecimento)
  - Exportar para backends como Jaeger, Prometheus, Grafana, Elastic, etc.

- **Vantagens**:
  - Desacopla sua aplicação do backend de observabilidade
  - Permite configuração centralizada
  - Facilita escalabilidade e manutenção

## 🔄 Fluxo de dados

```text
[Aplicação com SDK OTel] → envia dados → [OpenTelemetry Collector] → exporta para → [Backend de observabilidade]



http://host.docker.internal:3100
http://loki:3100
http://jaeger:16686
http://prometheus:9090
