using BeautySaloonService.BindingModel;
using BeautySaloonService.ViewModel;

namespace BeautySaloonService.Interfaces
{
    public interface IKlientService
    {
        KlientViewModel GetElement(int id);

        void AddElement(KlientBindingModel model);

        void UpdElement(KlientBindingModel model);
    }
}
