using Core.Renegociacao.Meters;
using Core.Renegociacao.Records;

namespace Core.Renegociacao.Services
{
	public class EmprestimoService
	{
        private readonly MetricsHelper _metrics;
		public EmprestimoService(MetricsHelper metrics)
		{
			_metrics = metrics;
        }

		public EmprestimoRequest Processar(EmprestimoRequest request)
		{
            _metrics.RecordCounter(
                    "emprestimos.criados",
                    value: 1,
                    new KeyValuePair<string, object>("solicitante", request.Name),
                    new KeyValuePair<string, object>("valor", request.Ammount)
                );

            return request;

        }
    }
}
