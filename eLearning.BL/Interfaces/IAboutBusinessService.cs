using Common.DTO.DTO;
using Common.ViewModel.Common;
using eLearning.Common.Dto;

namespace eLearning.BL.Interfaces
{
    public interface IAboutBusinessService
    {
        ApiResult<AboutDto> GetFirstOrDefault();
       
        void Update(ApiResult<AboutDto> viewModel);
    }
}