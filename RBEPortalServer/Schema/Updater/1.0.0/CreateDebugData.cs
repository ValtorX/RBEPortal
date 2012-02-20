using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Schema.Updater;
using Migrator.Framework;
using Migrator.Framework.SchemaBuilder;

namespace RBEPortalServer.Schema.Updater {
#if DEBUG
    [Migration(99999999999999999)]
#endif
    public class CreateDebugData : MigrationTask {

        public override void Up() {
            Database.ExecuteNonQuery(System.Reflection.Assembly.GetExecutingAssembly().GetResourceFileContent("CreateDebugData.sql"));
        }

    }
}
