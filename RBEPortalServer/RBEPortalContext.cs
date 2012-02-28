using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using Core;
using Core.Schema;
using Core.Tools;

namespace RBEPortalServer {
    /// <summary>
    /// The Core.CoreContext class is the main context class that maintains the states of all operations that are stateful.
    /// </summary>
    /// <remarks>
    /// This class should always be used within a using statement.
    /// </remarks>
    public class RBEPortalContext : CoreContext {
        /// <summary>
        /// Initializes a new instance of the <see cref="RBEPortalContext"/> class.
        /// </summary>
        public RBEPortalContext() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RBEPortalContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public RBEPortalContext(CoreContextSettings settings)
            : base(settings) {
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        protected override CoreData CoreData {
            get {
                if (_RBEPortalData == null) {
                    InitData();
                }
                return base.CoreData;
            }
            set {
                base.CoreData = value;
            }
        }

        private System.Data.SqlClient.SqlConnection _SharedConnection;
        /// <summary>
        /// Inits the data.
        /// </summary>
        protected void InitData() {
            var core =
                new System.Data.EntityClient.EntityConnection(
                    @"metadata=res://Core/Schema.Core.csdl|res://Core/Schema.Core.msl|res://Core/Schema.Core.ssdl;provider=System.Data.SqlClient;provider connection string=""""");
            var portal =
                new System.Data.EntityClient.EntityConnection(
                    @"metadata=res://*/Schema.RBEPortal.csdl|res://*/Schema.RBEPortal.ssdl|res://*/Schema.RBEPortal.msl;provider=System.Data.SqlClient;provider connection string=""""");

            _SharedConnection = new System.Data.SqlClient.SqlConnection(Settings.ConnectionString);

            base.CoreData = new CoreData(new System.Data.EntityClient.EntityConnection(core.GetMetadataWorkspace(), _SharedConnection), false);
            base.CoreData.ObjectContext.ContextOptions.ProxyCreationEnabled = false;
            _RBEPortalData = new Schema.RBEPortalData(new System.Data.EntityClient.EntityConnection(portal.GetMetadataWorkspace(), _SharedConnection), false);
            _RBEPortalData.ObjectContext.ContextOptions.ProxyCreationEnabled = false;
        }

        private Schema.RBEPortalData _RBEPortalData;
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public virtual Schema.RBEPortalData RBEPortalData {
            get {
                if (_RBEPortalData == null) {
                    InitData();
                }
                return _RBEPortalData;
            }
            set {
                _RBEPortalData = value;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose() {
            if (_RBEPortalData != null) {
                _RBEPortalData.Dispose();
                _RBEPortalData = null;
            }
            if (_SharedConnection != null) {
                _SharedConnection.Dispose();
                _SharedConnection = null;
            }
            base.Dispose();
        }
    }
}
