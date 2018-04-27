using BeautySaloonService.BindingModel;
using BeautySaloonService.ViewModel;
using System.Collections.Generic;

namespace BeautySaloonService.Interfaces
{
    public interface IKlientService
    {
        List<KlientViewModel> GetList();

        KlientViewModel GetElement(int id);

        void AddElement(KlientBindingModel model);

        void UpdElement(KlientBindingModel model);

        void DelElement(int id);
    }
}
