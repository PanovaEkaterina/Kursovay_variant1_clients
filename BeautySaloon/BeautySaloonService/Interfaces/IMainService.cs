using BeautySaloonService.BindingModel;
using BeautySaloonService.ViewModel;
using System.Collections.Generic;

namespace BeautySaloonService.Interfaces
{
    public interface IMainService
    {
        List<RequestViewModel> GetList();

        void CreateRequest(RequestBindingModel model);

        void PayRequest(RequestBindingModel model);
    }
}
