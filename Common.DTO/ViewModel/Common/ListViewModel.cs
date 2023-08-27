using System;
using System.Collections;
using System.Collections.Generic;
using PagedList;

namespace Common.ViewModel.Common
{
    public class ListViewModel<T>
    {
        public ListViewModel()
        {
            ReturnMessage = new List<string>();
            ValidationErrors = new Hashtable();
        }

        public long TotalRows { get; set; }

        /// <summary>
        /// Gets or sets the PageSize
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the CurrentPageNumber
        /// </summary>
        public int CurrentPageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the MaxPageDisplayNumber
        /// </summary>
        public int MaxPageDisplayNumber { get; set; } = 5;

        /// <summary>
        /// Gets or sets the FistPageNumber
        /// </summary>
        public int FistPageNumber { get; set; } = 1;

        // Properties has to calculate from businesservice:
        /// <summary>
        /// Gets or sets the TotalPages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the StartPageNumber
        /// </summary>
        public int StartPageNumber { get; set; }

        /// <summary>
        /// Gets or sets the EndPageNumber
        /// </summary>
        public int EndPageNumber { get; set; }

        /// <summary>
        /// Gets or sets the LastPageNumber
        /// </summary>
        public int LastPageNumber { get; set; }

        /// <summary>
        /// Gets or sets the PrevPageNumber
        /// </summary>
        public int PrevPageNumber { get; set; }

        /// <summary>
        /// Gets or sets the NextPageNumber
        /// </summary>
        public int NextPageNumber { get; set; }

        public bool ReturnStatus { get; set; }

        /// <summary>
        /// Gets or sets the ReturnMessage
        /// </summary>
        public List<String> ReturnMessage { get; set; }

        /// <summary>
        /// Gets or sets the ValidationErrors
        /// </summary>
        public Hashtable ValidationErrors { get; set; }

        /// <summary>
        /// Gets or sets the IsAuthenicated
        /// </summary>
        public Boolean IsAuthenicated { get; set; }

        public IPagedList<T> Data { get; set; }
    }
}