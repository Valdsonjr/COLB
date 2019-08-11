using System;

namespace Services.Util
{
    /// <summary>
    /// Email
    /// </summary>
    public class Email
    {
        /// <summary>
        /// String do email
        /// </summary>
        public String Raw { get; }
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="value"></param>
        public Email(String value)
        {
            Raw = value;
        }
        /// <summary>
        /// Validações de e-mail
        /// </summary>
        /// <returns></returns>
        public Boolean IsValid()
        {
            return true;
        }
    }
}
