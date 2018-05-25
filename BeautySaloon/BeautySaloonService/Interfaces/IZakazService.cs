using BeautySaloonService.BindingModel;
using BeautySaloonService.ViewModel;
using System.Collections.Generic;

namespace BeautySaloonService.Interfaces
{
    public interface IZakazService
    {
        List<ZakazViewModel> GetList();

        ZakazViewModel GetElement(int id);

        void AddElement(ZakazBindingModel model);

        void UpdElement(ZakazBindingModel model);

        void DelElement(int id);
    }
}
