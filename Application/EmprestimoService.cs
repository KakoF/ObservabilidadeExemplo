using Core.Renegociacao.Meters;

namespace Application
{
	public class EmprestimoService
	{
        private readonly MetricsHelper _metrics;

		public EmprestimoService(MetricsHelper metrics)
		{
			_metrics = metrics;
        }



	}
}
