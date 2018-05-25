using BeautySaloonService.BindingModel;
using BeautySaloonService.ViewModel;
using System.Collections.Generic;

namespace BeautySaloonService.Interfaces
{
    public interface IReportService
    {
        void SaveZakazPrice(ReportBindingModel model);

        List<KlientRequestsModel> GetKlientRequests(ReportBindingModel model);

        void SaveKlientRequests(ReportBindingModel model);
    }
}
