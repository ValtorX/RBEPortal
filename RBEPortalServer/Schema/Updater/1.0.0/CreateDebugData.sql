DELETE FROM SchemaInfo.RBEPortal WHERE Version = 99999999999999999

IF (SELECT COUNT(*) FROM Core.ApplicationParameter WHERE PermanentKeyName = 'DummyClient.CompanyName.Global') < 1
BEGIN
    INSERT INTO Core.ApplicationParameter (PermanentKeyName, Version) VALUES ('DummyClient.CompanyName.Global', 1);
    INSERT INTO Core.ApplicationParameterValue (ApplicationParameterID, StartingDate, SVal01, DVal01, NVal01, Version) VALUES (@@IDENTITY, null, 'Hello World Company', null, null, 1);
END

--IF (SELECT COUNT(*) FROM [RBEPortal].[User] WHERE [UserName] = 'Valtor') < 1
--BEGIN
--    INSERT INTO [RBEPortal].[User] ([UserName], [UserPassword], [CreationDate], [CreatedBy]) VALUES
--        ('Valtor', '123', GetDate(), 'panadeau');
--END
