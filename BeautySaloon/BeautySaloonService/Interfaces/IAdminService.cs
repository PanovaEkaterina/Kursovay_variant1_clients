using BeautySaloonService.BindingModel;
using BeautySaloonService.ViewModel;
using System.Collections.Generic;

namespace BeautySaloonService.Interfaces
{
    public interface IAdminService
    {
        List<AdminViewModel> GetList();

        AdminViewModel GetElement(int id);

        void AddElement(AdminBindingModel model);

        void UpdElement(AdminBindingModel model);

        void DelElement(int id);
    }
}
