using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Person.Core.Base {
    public class EntityBase {
        #region - attributes -

        [Key]
        public string Id { get; set; }

        public DateTimeOffset? CriadoEm { get; set; }
        public DateTimeOffset? AtualizadoEm { get; set; }
        public bool Deletado { get; set; }

        #endregion

        #region - ctor -

        protected EntityBase() {
            Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            CriadoEm = DateTimeOffset.UtcNow;
        }

        #endregion

        #region - methods -

        /// <summary>
        /// Return a new custom guid with base in the size length attribute
        /// </summary>
        /// <param name="length">Use to substring length</param>
        /// <returns></returns>
        public static string CustomHash(int length = 0) {
            return Guid.NewGuid().ToString().ToUpper().Replace("-", "").Substring(0, length);
        }

        /// <summary>
        /// Return a new custom random latter hash with base in the size length attribute
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string LetterHash(int length) {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion
    }
}