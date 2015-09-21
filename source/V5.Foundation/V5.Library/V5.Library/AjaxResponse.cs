// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AjaxResponse.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The ajax response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library
{
    /// <summary>
    /// The ajax response.
    /// </summary>
    public class AjaxResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AjaxResponse"/> class.
        /// </summary>
        public AjaxResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AjaxResponse"/> class.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        public AjaxResponse(int state)
        {
            this.State = state;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AjaxResponse"/> class.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public AjaxResponse(int state, string message)
        {
            this.State = state;
            this.Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AjaxResponse"/> class.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public AjaxResponse(int state, object data)
        {
            this.State = state;
            this.Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AjaxResponse"/> class.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public AjaxResponse(int state, string message, object data)
        {
            this.State = state;
            this.Message = message;
            this.Data = data;
        }

        /// <summary>
        /// 返回状态：-401 会话失效，-403 无访问权限
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public object Data { get; set; }
    }
}