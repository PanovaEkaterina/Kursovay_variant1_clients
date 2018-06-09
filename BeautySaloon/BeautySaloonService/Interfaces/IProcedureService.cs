using BeautySaloonService.BindingModel;
using BeautySaloonService.ViewModel;
using System.Collections.Generic;

namespace BeautySaloonService.Interfaces
{
    public interface IProcedureService
    {
        List<ProcedureViewModel> GetList();

        ProcedureViewModel GetElement(int id);
    }
}
