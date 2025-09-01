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



# 📘 Instrumentações OpenTelemetry em .NET

Este documento descreve as principais instrumentações disponíveis no OpenTelemetry para aplicações .NET, explicando o que cada uma coleta e como elas ajudam na observabilidade da aplicação.

---

## 🔧 1. AddAspNetCoreInstrumentation()

Instrumenta o pipeline de requisições HTTP do ASP.NET Core.

### Métricas e Traces coletados:
- Tempo de resposta por rota
- Status code das respostas (200, 404, 500 etc.)
- Método HTTP (`GET`, `POST`, etc.)
- Exceções lançadas durante o processamento

💡 Ideal para monitorar endpoints e identificar gargalos ou erros.

---

## 🌐 2. AddHttpClientInstrumentation()

Instrumenta chamadas feitas com `HttpClient`.

### Métricas e Traces coletados:
- Tempo de execução da requisição externa
- URL de destino
- Status da resposta
- Exceções de rede

💡 Útil para rastrear dependências externas como APIs de terceiros.

---

## ⚙️ 3. AddRuntimeInstrumentation()

Coleta métricas do ambiente de execução do .NET.

### Métricas coletadas:
- Coletas de garbage collector (GC)
- Número de threads no pool
- Exceções não tratadas
- Alocação de memória

💡 Ajuda a entender o comportamento interno da aplicação.

---

## 🧠 4. AddProcessInstrumentation()

Coleta métricas do processo da aplicação no sistema operacional.

### Métricas coletadas:
- Uso de CPU
- Memória física (Working Set)
- Tempo de vida do processo
- Threads ativas

💡 Excelente para monitorar a saúde geral da aplicação.

---

## 📊 5. AddEventCountersInstrumentation()

Coleta métricas expostas via EventCounters de bibliotecas específicas.

### Fontes configuradas:
```csharp
options.AddEventSources("Microsoft.AspNetCore.Hosting", "System.Net.Http");



http://host.docker.internal:3100
http://loki:3100
http://jaeger:16686
http://prometheus:9090

https://grafana.com/grafana/dashboards/19924-asp-net-core/

https://grafana.com/grafana/dashboards/17706-asp-net-otel-metrics/


- http_server_request_duration_seconds_count → número de requisições
- http_server_request_duration_seconds_sum → soma total das durações
- http_server_request_duration_seconds_bucket → distribuição por faixas de tempo

sum(increase(http_server_request_duration_seconds_count{exported_job="Core.Renegociacao"}[$__rate_interval]))
increase(http_server_request_duration_seconds_count{exported_job="BFF", http_route="Emprestimo/{id:int}"}[1m])

histogram_quantile(0.50, sum(rate(http_server_request_duration_seconds_bucket{exported_job="BFF", http_route="Emprestimo/{id:int}"}[1m])) by (le))

histogram_quantile(0.90, sum(rate(http_server_request_duration_seconds_bucket{exported_job="BFF", http_route="Emprestimo/{id:int}"}[1m])) by (le))

histogram_quantile(0.95, sum(rate(http_server_request_duration_seconds_bucket{exported_job="BFF", http_route="Emprestimo/{id:int}"}[1m])) by (le))

histogram_quantile(0.99, sum(rate(http_server_request_duration_seconds_bucket{exported_job="BFF", http_route="Emprestimo/{id:int}"}[1m])) by (le))

histogram_quantile(0.999, sum(rate(http_server_request_duration_seconds_bucket{exported_job="BFF", http_route="Emprestimo/{id:int}"}[1m])) by (le))





choco install k6

winget install k6 --source winget

k6 run index.js

sum(increase(http_server_request_duration_seconds_count{exported_job="Core.Renegociacao"}[1m]))

sum(increase(http_server_request_duration_seconds_count{exported_job="Core.Renegociacao", http_response_status_code="500"}[1m]))