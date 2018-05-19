using BeautySaloonService.BindingModel;
using BeautySaloonService.ViewModel;
using System.Collections.Generic;

namespace BeautySaloonService.Interfaces
{
    public interface IMaterialService
    {
        List<MaterialViewModel> GetList();

        MaterialViewModel GetElement(int id);

        void AddElement(MaterialBindingModel model);

        void UpdElement(MaterialBindingModel model);

        void DelElement(int id);
    }
}
