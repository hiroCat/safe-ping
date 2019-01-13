IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[t_enviroment_config]') AND type in (N'U'))
BEGIN

INSERT INTO [dbo].[t_enviroment_config] (name,ip) values ('localhost','https://127.0.0.0')


INSERT INTO [dbo].[t_endpoint_config] (name,endpoint,body,id_enviroment) values ('Auth api1',':8090/api/login','{"username": "user", "password": "pass"}',(select id from t_enviroment_config where name ='Epp2'))
INSERT INTO [dbo].[t_endpoint_config] (name,endpoint,body,id_enviroment) values ('Auth api2',':8080/api/login','{"username": "user", "password": "pass"}',(select id from t_enviroment_config where name ='Epp2'))

END

	