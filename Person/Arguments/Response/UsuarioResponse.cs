using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Person.Core;

namespace Person.Arguments.Response {
    public class UsuarioResponse {
        public static explicit operator UsuarioResponse(Usuario v) {
            throw new NotImplementedException();
        }
    }
}
