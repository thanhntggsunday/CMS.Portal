using Common.Model.Entities;
using eLearning.Common.Dto;
using eLearning.Model.Entities;

namespace eLearning.Common.Classes
{
    public static class Extensions
    {
        #region Category

        public static void CopyFromModel(this ContentCategoryDto obj, ContentCategory data)
        {
            obj.ID = data.ID;
            obj.CreatedBy = data.CreatedBy;
            obj.CreatedDate = data.CreatedDate;
            obj.DisplayOrder = data.DisplayOrder;
            obj.Language = data.Language;
            obj.MetaDescriptions = data.MetaDescriptions;
            obj.MetaKeywords = data.MetaKeywords;
            obj.MetaTitle = data.MetaTitle;
            obj.ModifiedBy = data.ModifiedBy;
            obj.ModifiedDate = data.ModifiedDate;
            obj.Name = data.Name;
            obj.CreatedDate = data.CreatedDate;
            obj.ParentID = data.ParentID;
            obj.ShowOnHome = data.ShowOnHome;
            obj.Status = data.Status;
        }

        public static void CopyFromDto(this ContentCategory obj, ContentCategoryDto data)
        {
            obj.ID = data.ID;
            obj.CreatedBy = data.CreatedBy;
            obj.CreatedDate = data.CreatedDate;
            obj.DisplayOrder = data.DisplayOrder;
            obj.Language = data.Language;
            obj.MetaDescriptions = data.MetaDescriptions;
            obj.MetaKeywords = data.MetaKeywords;
            obj.MetaTitle = data.MetaTitle;
            obj.ModifiedBy = data.ModifiedBy;
            obj.ModifiedDate = data.ModifiedDate;
            obj.Name = data.Name;
            obj.CreatedDate = data.CreatedDate;
            obj.ParentID = data.ParentID;
            obj.ShowOnHome = data.ShowOnHome;
            obj.Status = data.Status;
        }

        #endregion Category

        #region Content

        public static void CopyFromModel(this ContentDto obj, Content data)
        {
            obj.Id = data.ID;
            obj.CategoryID = data.CategoryID;
            obj.CreatedDate = data.CreatedDate;
            obj.CreatedBy = data.CreatedBy;
            obj.Description = data.Description;
            obj.Detail = data.Detail;
            obj.Image = data.Image;
            obj.MetaCode = data.MetaCode;
            obj.Language = data.Language;
            obj.MetaDescriptions = data.MetaDescriptions;
            obj.MetaKeywords = data.MetaKeywords;
            obj.ContentName = data.Name;
            obj.Tags = data.Tags;
            obj.TopHot = data.TopHot;
            obj.Warranty = data.Warranty;
            obj.ViewCount = data.ViewCount;
        }

        public static void CopyFromDto(this Content obj, ContentDto data)
        {
            obj.ID = data.Id;
            obj.CategoryID = data.CategoryID;
            obj.CreatedDate = data.CreatedDate;
            obj.CreatedBy = data.CreatedBy;
            obj.Description = data.Description;
            obj.Detail = data.Detail;
            obj.Image = data.Image;
            obj.MetaCode = data.MetaCode;
            obj.Language = data.Language;
            obj.MetaDescriptions = data.MetaDescriptions;
            obj.MetaKeywords = data.MetaKeywords;
            obj.Name = data.ContentName;
            obj.Tags = data.Tags;
            obj.TopHot = data.TopHot;
            obj.Warranty = data.Warranty;
            obj.ViewCount = data.ViewCount;
        }

        #endregion Content

        #region Slide

        public static void CopyFromModel(this SlideDto obj, Slide data)
        {
            obj.ID = data.ID;
            obj.CreatedBy = data.CreatedBy;
            obj.CreatedDate = data.CreatedDate;
            obj.DisplayOrder = data.DisplayOrder;
            obj.Image = data.Image;
            obj.ModifiedBy = data.ModifiedBy;
            obj.ModifiedDate = data.ModifiedDate;
            obj.Status = data.Status;
            obj.Link = data.Link;
            obj.Description = data.Description;
        }

        public static void CopyFromDto(this Slide obj, SlideDto data)
        {
            obj.ID = data.ID;
            obj.CreatedBy = data.CreatedBy;
            obj.CreatedDate = data.CreatedDate;
            obj.DisplayOrder = data.DisplayOrder;
            obj.Image = data.Image;
            obj.ModifiedBy = data.ModifiedBy;
            obj.ModifiedDate = data.ModifiedDate;
            obj.Status = data.Status;
            obj.Link = data.Link;
            obj.Description = data.Description;
        }

        #endregion


        #region Course

        public static void CopyFromDto(this Courses obj, CourseDto objDto)
        {
            obj.Id = objDto.Id;
            obj.CreatedDate = objDto.CreatedDate;
            // obj.CategoryId = objDto.CategoryId;
            obj.Content = objDto.Content;
            obj.CreatedBy = objDto.CreatedBy;
            obj.Description = objDto.Description;
            obj.ModifiedDate = objDto.ModifiedDate;
            obj.ModifiedBy = objDto.ModifiedBy;
            obj.Image = objDto.Image;
            obj.Name = objDto.CourseName;
            obj.Price = objDto.Price;
            obj.PromotionPrice = objDto.PromotionPrice;
            obj.Status = objDto.Status;
            obj.TrainerId = objDto.TrainerId;

            if (objDto.CategoryId.HasValue && objDto.CategoryId.Value > 0)
            {
                obj.CategoryId = objDto.CategoryId;
            }
        }

        public static void CopyFromModel(this CourseDto objDto, Courses obj)
        {
            objDto.Id = obj.Id;
            objDto.CreatedDate = obj.CreatedDate;
            objDto.CategoryId = obj.CategoryId;
            objDto.Content = obj.Content;
            objDto.CreatedBy = obj.CreatedBy;
            objDto.Description = obj.Description;
            objDto.ModifiedDate = obj.ModifiedDate;
            objDto.ModifiedBy = obj.ModifiedBy;
            objDto.Image = obj.Image;
            objDto.CourseName = obj.Name;
            objDto.Price = obj.Price;
            objDto.PromotionPrice = obj.PromotionPrice;
            objDto.Status = obj.Status;
            objDto.TrainerId = obj.TrainerId;
        }

        #endregion Course

        #region CourseCategory

        public static void CopyFromDto(this CourseCategory category, CourseCategoryDto dto)
        {
            category.CreatedDate = dto.CreatedDate;
            category.CreatedBy = dto.CreatedBy;
            category.Name = dto.Name;
            category.Status = dto.Status;
            category.SeoAlias = dto.SeoAlias;
            category.SeoMetaDescription = dto.SeoMetaDescription;
            category.SeoMetaKeywords = dto.SeoMetaKeywords;
            category.SeoTitle = dto.SeoTitle;
        }

        public static void CopyFromMoel(this CourseCategoryDto dto, CourseCategory category)
        {
            dto.ID = category.Id;
            dto.CreatedDate = category.CreatedDate;
            dto.CreatedBy = category.CreatedBy;
            dto.Name = category.Name;
            dto.Status = category.Status;
            dto.SeoAlias = category.SeoAlias;
            dto.SeoMetaDescription = category.SeoMetaDescription;
            dto.SeoMetaKeywords = category.SeoMetaKeywords;
            dto.SeoTitle = category.SeoTitle;
        }

        #endregion CourseCategory

        #region CourseStudent
        public static void CopyFromModel(this CourseStudentDto dto, CoursesStudents obj)
        {
            dto.CreatedDate = obj.CreatedDate;
            dto.CourseId = obj.CourseId;
            dto.CreatedBy = obj.CreatedBy;
            dto.ModifiedDate = obj.ModifiedDate;
            dto.ModifiedBy = obj.ModifiedBy;
            dto.Id = obj.Id;
            dto.Price = obj.Price;
            dto.AppUserId = obj.AppUserId;
        }

        public static void CopyFromDto(this CoursesStudents obj, CourseStudentDto dto)
        {
            obj.CreatedDate = dto.CreatedDate;
            obj.CourseId = dto.CourseId;
            obj.CreatedBy = dto.CreatedBy;
            obj.ModifiedDate = dto.ModifiedDate;
            obj.ModifiedBy = dto.ModifiedBy;
            obj.Id = dto.Id;
            obj.Price = dto.Price;
            obj.AppUserId = dto.AppUserId;
        }
        #endregion

        #region CourseLesson
        public static void CopyFromModel(this CourseLessonDto dto, CourseLessons courseLesson)
        {
            dto.Attachment = courseLesson.Attachment;
            dto.CourseId = courseLesson.CourseId;
            dto.CreatedDate = courseLesson.CreatedDate;
            dto.CreatedBy = courseLesson.CreatedBy;
            dto.ModifiedBy = courseLesson.ModifiedBy;
            dto.Id = courseLesson.Id;
            dto.SlidePath = courseLesson.SlidePath;
            dto.SortOrder = courseLesson.SortOrder;
            dto.Status = courseLesson.Status;
            dto.VideoPath = courseLesson.VideoPath;
            dto.ModifiedDate = courseLesson.ModifiedDate;

        }

        public static void CopyFromDto(this CourseLessons obj, CourseLessonDto dto)
        {
            obj.Attachment = dto.Attachment;
            obj.CourseId = dto.CourseId;
            obj.CreatedDate = dto.CreatedDate;
            obj.CreatedBy = dto.CreatedBy;
            obj.ModifiedBy = dto.ModifiedBy;
            obj.Id = dto.Id;
            obj.SlidePath = dto.SlidePath;
            obj.SortOrder = dto.SortOrder;
            obj.Status = dto.Status;
            obj.VideoPath = dto.VideoPath;
            obj.ModifiedDate = dto.ModifiedDate;
            obj.Name = dto.LessonName;
        }
        #endregion

        #region Order
        public static void CopyFromModel(this OrderDto dto, Order order)
        {
            dto.CreatedDate = order.CreatedDate;
            dto.CreatedBy = order.CreatedBy;
            dto.CustomerAddress = order.CustomerAddress;
            dto.CustomerEmail = order.CustomerEmail;
            dto.CustomerMessage = order.CustomerMessage;
            dto.CustomerMobile = order.CustomerMobile;
            dto.CustomerName = order.CustomerName;
            dto.CreatedDate = order.CreatedDate;
            dto.ModifiedBy = order.ModifiedBy;
            dto.Id = order.Id;
            dto.PaymentMethod = order.PaymentMethod;
            dto.PaymentStatus = order.PaymentStatus;
            dto.Total = order.Total;
        }

        public static void CopyFromDto(this Order obj, OrderDto dto)
        {
            obj.CreatedDate = dto.CreatedDate;
            obj.CreatedBy = dto.CreatedBy;
            obj.CustomerAddress = dto.CustomerAddress;
            obj.CustomerEmail = dto.CustomerEmail;
            obj.CustomerMessage = dto.CustomerMessage;
            obj.CustomerMobile = dto.CustomerMobile;
            obj.CustomerName = dto.CustomerName;
            obj.ModifiedDate = dto.ModifiedDate;
            obj.ModifiedBy = dto.ModifiedBy;
            obj.Id = dto.Id;
            obj.PaymentMethod = dto.PaymentMethod;
            obj.PaymentStatus = dto.PaymentStatus;
            obj.Total = dto.Total;
        }
        #endregion

        #region Trainner
        public static void CopyFromDto(this Trainners obj, TrainnerDto dto)
        {
            obj.Avatar = dto.Avatar;
            obj.Bio = dto.Bio;
            obj.CreatedDate = dto.CreatedDate;
            obj.CreatedBy = dto.CreatedBy;
            obj.ModifiedDate = dto.ModifiedDate;
            obj.ModifiedBy = dto.ModifiedBy;
            obj.Id = dto.Id;
            obj.Name = dto.Name;

        }

        public static void CopyFromModel(this TrainnerDto obj, Trainners data)
        {
            obj.Avatar = data.Avatar;
            obj.Bio = data.Bio;
            obj.CreatedDate = data.CreatedDate;
            obj.CreatedBy = data.CreatedBy;
            obj.ModifiedDate = data.ModifiedDate;
            obj.ModifiedBy = data.ModifiedBy;
            obj.Id = data.Id;
            obj.Name = data.Name;

        }
        #endregion
    }
}