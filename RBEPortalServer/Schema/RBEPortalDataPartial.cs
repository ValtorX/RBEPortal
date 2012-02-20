using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace RBEPortalServer.Schema {
    partial class RBEPortalData {

        /// <summary>
        /// Initializes a new instance of the <see cref="RBEPortalData"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="ownsConnection">if set to <c>true</c> [owns connection].</param>
        public RBEPortalData(DbConnection connection, bool ownsConnection)
            : base(connection, ownsConnection) {
        }
    }
}
