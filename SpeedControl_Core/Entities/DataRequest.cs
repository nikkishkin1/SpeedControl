﻿using System;

namespace SpeedControl_Core.Entities
{
    /// <summary>
    /// Model of a request for data
    /// </summary>
    public class DataRequest
    {
        /// <summary>
        /// Typeof a request
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// Date and time of a request
        /// </summary>
        public DateTime RequestDate { get; set; }
    }
}