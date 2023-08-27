using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.ViewModel
{
    public class TransactionalInformation
    {
        /// <summary>
        /// Gets or sets a value indicating whether ReturnStatus
        /// </summary>
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

        /// <summary>
        /// Return true if access denied
        /// </summary>
        public Boolean IsAccessDenied { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalInformation"/> class.
        /// </summary>
        public TransactionalInformation()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            ValidationErrors = new Hashtable();
            IsAuthenicated = false;
            IsAccessDenied = false;
        }
    }
}