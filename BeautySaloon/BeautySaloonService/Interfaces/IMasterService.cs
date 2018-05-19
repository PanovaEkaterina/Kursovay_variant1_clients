using BeautySaloonService.BindingModel;
using BeautySaloonService.ViewModel;
using System.Collections.Generic;

namespace BeautySaloonService.Interfaces
{
    public interface IMasterService
    {
        List<MasterViewModel> GetList();

        MasterViewModel GetElement(int id);

        void AddElement(MasterBindingModel model);

        void UpdElement(MasterBindingModel model);

        void DelElement(int id);
    }
}
