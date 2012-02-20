using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Schema.Updater;
using Migrator.Framework;
using Migrator.Framework.SchemaBuilder;

namespace RBEPortalServer.Schema.Updater {
    // The format of the Migration attribute is this.
    // E.g.:   30105201106171456
    //         3                  Major Version (can be 2 numbers long if required)
    //          01                Minor Version
    //            05              Build Version
    //              2011          Current Year
    //                  06        Current Month
    //                    17      Current Day
    //                      14    Current Hour (24)
    //                        56  Current Minute
    [Migration(10000000000000000)]
    public class CreateTables1_0_0 : MigrationTask {

        public override void Up() {
            Database.ExecuteNonQuery(System.Reflection.Assembly.GetExecutingAssembly().GetResourceFileContent("CreateTables1_0_0.sql"));
        }

    }
}
