using BeautySaloonService.BindingModel;
using BeautySaloonService.ViewModel;
using System.Collections.Generic;

namespace BeautySaloonService.Interfaces
{
    public interface IReportService
    {
        void SaveProcedurePriceDocx(ReportBindingModel model);

        void SaveProcedurePriceExcel(ReportBindingModel model);

        List<KlientRequestsModel> GetKlientRequests(ReportBindingModel model, int id);

        void SaveKlientRequests(ReportBindingModel model, int id);
    }
}
