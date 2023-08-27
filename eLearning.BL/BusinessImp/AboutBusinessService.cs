using Common.DTO.DTO;
using Common.Model;
using Common.ViewModel.Common;
using eLearning.BL.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace eLearning.BL.BusinessImp
{
    public class AboutBusinessService : IAboutBusinessService
    {
        public ApiResult<AboutDto> GetFirstOrDefault()
        {
            var viewModel = new ApiResult<AboutDto>();
            var aboutDto = new AboutDto();

            using (var context = new CommonDbContext())
            {
                var item = context.Abouts.FirstOrDefault();

                if (item != null)
                {
                    aboutDto.ID = item.ID;
                    aboutDto.Address = item.Address;
                    aboutDto.Email = item.Email;
                    aboutDto.Introduce = item.Detail;
                    aboutDto.OpenTime = item.OpenTime;
                    aboutDto.PhoneNumber = item.PhoneNumber;
                    aboutDto.Contact = item.Contact;
                }

                viewModel.ResultObj = aboutDto;
                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Get Item Successfully!");
            }

            return viewModel;
        }

        public void Update(ApiResult<AboutDto> viewModel)
        {
            using (var context = new CommonDbContext())
            {
                var item = context.Abouts.FirstOrDefault();

                if (item == null)
                {
                    return;
                }

                item.ID = viewModel.ResultObj.ID;
                item.Address = viewModel.ResultObj.Address;
                item.Email = viewModel.ResultObj.Email;
                item.Detail = viewModel.ResultObj.Introduce;
                item.OpenTime = viewModel.ResultObj.OpenTime;
                item.PhoneNumber = viewModel.ResultObj.PhoneNumber;
                item.Contact = viewModel.ResultObj.Contact;

                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Update Item Successfully!");
            }
        }
    }
}