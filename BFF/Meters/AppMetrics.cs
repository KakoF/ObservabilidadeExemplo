using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace BFF.Meters
{
	public class AppMetrics : IDisposable
	{
		private readonly Meter _meter;

		// 1. COUNTER - Contador de requisições por tipo
		private readonly Counter<int> _requisicoesCounter;

		// 2. HISTOGRAM - Tempo de processamento
		private readonly Histogram<double> _tempoProcessamentoHistogram;

		// 3. UP DOWN COUNTER - Número de usuários ativos
		private readonly UpDownCounter<int> _usuariosAtivosCounter;

		// 4. OBSERVABLE GAUGE - Uso de memória (exemplo adicional)
		private readonly ObservableGauge<double> _usoMemoriaGauge;

		public AppMetrics()
		{
			_meter = new Meter("BFF.Custom.Metrics", "1.0.0");

			// 1. Counter - conta eventos
			_requisicoesCounter = _meter.CreateCounter<int>(
				"requisicoes.total",
				unit: "requisicoes",
				description: "Total de requisições processadas por tipo");

			// 2. Histogram - mede distribuição de valores
			_tempoProcessamentoHistogram = _meter.CreateHistogram<double>(
				"tempo.processamento",
				unit: "ms",
				description: "Tempo de processamento das requisições");

			// 3. UpDownCounter - valor que sobe e desce
			_usuariosAtivosCounter = _meter.CreateUpDownCounter<int>(
				"usuarios.ativos",
				unit: "usuarios",
				description: "Número de usuários ativos no sistema");

			// 4. ObservableGauge - valor observado quando solicitado
			_usoMemoriaGauge = _meter.CreateObservableGauge<double>(
				"memoria.uso",
				observeValue: () => GetMemoryUsage(),
				unit: "MB",
				description: "Uso de memória da aplicação");
		}

		// Métodos para registrar métricas
		public void RegistrarRequisicao(string tipoRequisicao, string endpoint, int quantidade = 1)
		{
			_requisicoesCounter.Add(quantidade, 
				new KeyValuePair<string, object?>("tipo", tipoRequisicao),
				new KeyValuePair<string, object?>("endpoint", endpoint));
		}

		public void RegistrarTempoProcessamento(double tempoMs, string endpoint)
		{
			_tempoProcessamentoHistogram.Record(
				tempoMs,
				new KeyValuePair<string, object?>("endpoint", endpoint));
		}

		public void IncrementarUsuariosAtivos(int quantidade = 1)
		{
			_usuariosAtivosCounter.Add(quantidade);
		}

		public void DecrementarUsuariosAtivos(int quantidade = 1)
		{
			_usuariosAtivosCounter.Add(-quantidade);
		}

		private static double GetMemoryUsage()
		{
			// Exemplo simples de uso de memória
			return Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0;
		}

		public void Dispose()
		{
			_meter?.Dispose();
		}
	}
}
